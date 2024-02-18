using System.Collections;
using GBC;
using DiskCardGame;
using UnityEngine;
using HarmonyLib;
using LifeCost.Costs;

namespace LifeCost.Sigils
{
    [HarmonyPatch]
    public abstract class LifeActiveAbilityCost : ActivatedAbilityBehaviour
    {
        public virtual int LifeMoneyCost { get; }

        public virtual int LifeCost { get; }

        public virtual int MoneyCost { get; }

        public override int EnergyCost { get; }

        public override int BonesCost { get; }

        private bool CanAffordLife()
        {
            int num = Singleton<LifeManager>.Instance.Balance + 4;
            return num >= this.LifeCost;
        }

        private bool CanAffordMoney()
        {
            int currency;
            if (SaveManager.SaveFile.IsPart2)
            {
                currency = SaveData.Data.currency;
            }
            else
            {
                currency = RunState.Run.currency;
            }
            return currency >= this.MoneyCost;
        }

        private bool CanAffordHybrid()
        {
            Plugin.Log.LogMessage("Lifecost active ability patch firing 3");
            int currency;
            if (SaveManager.SaveFile.IsPart2)
            {
                currency = SaveData.Data.currency;
            }
            else
            {
                currency = RunState.Run.currency;
            }
            int num = Singleton<LifeManager>.Instance.Balance + 4;
            int num2 = currency + num;
            return num2 >= this.LifeMoneyCost;
        }

        public new IEnumerator OnActivatedAbility()
        {
            if (this.CanAfford() && this.CanAffordLife() && this.CanAffordMoney() && this.CanAffordHybrid() &&  this.CanActivate())
            {
                if (this.EnergyCost > 0)
                {
                    yield return Singleton<ResourcesManager>.Instance.SpendEnergy(this.EnergyCost);
                    if (Singleton<ConduitCircuitManager>.Instance != null)
                    {
                        CardSlot cardSlot = Singleton<BoardManager>.Instance.GetSlots(true).Find((CardSlot x) => x.Card != null && x.Card.HasAbility(Ability.ConduitEnergy));
                        if (cardSlot != null)
                        {
                            ConduitEnergy component = cardSlot.Card.GetComponent<ConduitEnergy>();
                            if (component != null && component.CompletesCircuit())
                            {
                                yield return Singleton<ResourcesManager>.Instance.AddEnergy(this.EnergyCost);
                            }
                        }
                    }
                }
                if (this.BonesCost > 0)
                {
                    yield return Singleton<ResourcesManager>.Instance.SpendBones(this.BonesCost);
                }
                if (this.LifeCost > 0)
                {
                    if (SaveManager.SaveFile.IsPart2)
                    {
                        yield return PayCost.extractCostPart2_lifeOnly(this.LifeCost);
                    }
                    else
                    {
                        yield return PayCost.extractCostPart1_lifeOnly(this.LifeCost);
                    }
                }
                if (this.MoneyCost > 0)
                {
                    if (SaveManager.SaveFile.IsPart2)
                    {
                        yield return PayCost.extractCostPart2_MoneyOnly(this.MoneyCost);
                    }
                    else
                    {
                        int currentCurrency = RunState.Run.currency;
                        yield return PayCost.extractCostPart1_MoneyOnly(this.MoneyCost, currentCurrency);
                    }
                }
                if (this.LifeMoneyCost > 0)
                {
                    if (SaveManager.SaveFile.IsPart2)
                    {
                        int currentCurrency3 = SaveData.Data.currency;
                        yield return PayCost.extractCostPart2_hybrid(this.LifeMoneyCost, currentCurrency3);
                    }
                    else
                    {
                        int currentCurrency2 = RunState.Run.currency;
                        yield return PayCost.extractCostPart1_hybrid(this.LifeMoneyCost, currentCurrency2);
                    }
                }
                yield return new WaitForSeconds(0.1f);
                yield return base.PreSuccessfulTriggerSequence();
                yield return this.Activate();
                ProgressionData.SetMechanicLearned(MechanicsConcept.GBCActivatedAbilities);
            }
            else
            {
                base.Card.Anim.LightNegationEffect();
                AudioController.Instance.PlaySound2D("toneless_negate", MixerGroup.GBCSFX, 0.2f, 0f, null, null, null, null, false);
                yield return new WaitForSeconds(0.25f);
            }
            yield break;
        }
    }
}