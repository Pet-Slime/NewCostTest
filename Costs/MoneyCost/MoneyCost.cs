using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.CardCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LifeCost.Costs.MCost
{
    public class MoneyCost : CustomCardCost
    {
        // this is a required field, and should be equal to the name you pass into the API when registering your cost
        public override string CostName => "MoneyCost";

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
            if (cardCost > currency)
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
                Hint = $"You do not have enough [c:gray]Foils[c:] to play that. You gain [c:gray]Foils[c:] by dealing overkill damage.";
            }
            else
            {
                var choice1 = $"You lack the foils to play that [c:gray]{card.Info.DisplayedNameLocalized}[c:].";
                var choice2 = $"[c:gray]{card.Info.DisplayedNameLocalized}[c:] requires you pay it's cost in [c:gray]foils[c:], that which you have [c:gray]{RunState.Run.currency}[c:].";
                var choice3 = $"That [c:gray]{card.Info.DisplayedNameLocalized}[c:] has the cost of [c:gray]{cardCost}[c:] foils, which you do not have.";

                List<String> strings = new List<String>();
                strings.Add(choice1);
                strings.Add(choice2);
                strings.Add(choice3);

                Hint = strings[UnityEngine.Random.Range(0, strings.Count)];
            }

            return Hint;
        }

        // this is called after a card with this cost resolves on the board
        // if your cost spends a resource, this is where you'd put that logic
        public override IEnumerator OnPlayed(int cardCost, PlayableCard card)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                yield return PayCost.extractCostPart2_MoneyOnly(cardCost);
            }
            else
            {
                yield return PayCost.extractCostPart1_MoneyOnly(cardCost, RunState.Run.currency);
            }
        }
    }
}
