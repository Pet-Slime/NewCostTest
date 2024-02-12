using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.CardCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.LCost
{
    public class LifeCost : CustomCardCost
    {
        // this is a required field, and should be equal to the name you pass into the API when registering your cost
        public override string CostName => "LifeCost";

        public ResourceType ResourceType = (ResourceType)42;

        // whether or not this cost's price has been satisfied by the card
        public override bool CostSatisfied(int cardCost, PlayableCard card)
        {
            //Life is tracked from a scale of -5 to 5. So we add 4 to it to correct it to be from -1 to 9
            int correctedLife = Singleton<LifeManager>.Instance.Balance + 4;
            if (cardCost > correctedLife)
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
                Hint = $"You do not have enough [c:bG]Life[c:] on your scales to play that. Damage your opponent to gain more [c:bG]Life[c:].";
            }
            else
            {
                var choice1 = $"Your [c:bG]Scales[c:] are too tipped to play [c:bG]{card.Info.DisplayedNameLocalized}[c:].";
                var choice2 = $"[c:bG]{card.Info.DisplayedNameLocalized}[c:] requires you to pay in life, in which you can not fullfill without killing yourself.";
                var choice3 = $"You would kill yourself if you played [c:bG]{card.Info.DisplayedNameLocalized}[c:]. Your [c:bG]Scales[c:] are too tipped";

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
                yield return PayCost.extractCostPart2_lifeOnly(cardCost);
            }
            else
            {
                yield return PayCost.extractCostPart1_lifeOnly(cardCost);
            }
        }
    }
}
