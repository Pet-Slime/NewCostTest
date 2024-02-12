using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.CardCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LifeCost.Costs.HCost
{
    public class LifeMoneyCost : CustomCardCost
    {
        // this is a required field, and should be equal to the name you pass into the API when registering your cost
        public override string CostName => "LifeMoneyCost";
        public ResourceType ResourceType = (ResourceType)42;

        // whether or not this cost's price has been satisfied by the card
        public override bool CostSatisfied(int cardCost, PlayableCard card)
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
            int hybridCost = currency +  Singleton<LifeManager>.Instance.Balance + 4;
            if (cardCost > hybridCost)
            {
                return false;
            }
            return true;
        }

        // the dialogue that's played when you try to play a card with this cost, and CostSatisfied is false
        public override string CostUnsatisfiedHint(int cardCost, PlayableCard card)
        {
            string Hint;
            if (SaveManager.SaveFile.IsPart2)
            {
                Hint = $"You do not have enough [c:bG]Life[c:] or [c:gray]Foils[c:] to play that. Damage your opponent to gain more [c:bG]Life[c:].";
            }
            else
            {
                Hint = $"Your [c:bG]Scales[c:] are too tipped and you lack the [c:gray]Foils[c:] to play {card.Info.DisplayedNameLocalized}.";
            }
            return Hint;
        }

        // this is called after a card with this cost resolves on the board
        // if your cost spends a resource, this is where you'd put that logic
        public override IEnumerator OnPlayed(int cardCost, PlayableCard card)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                yield return PayCost.extractCostPart2_hybrid(cardCost, SaveData.Data.currency);
            }
            else
            {
                yield return PayCost.extractCostPart1_hybrid(cardCost, RunState.Run.currency);
            }
        }
    }
}
