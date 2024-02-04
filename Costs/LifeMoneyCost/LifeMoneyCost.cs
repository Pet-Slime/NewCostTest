using DiskCardGame;
using InscryptionAPI.CardCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NewCostTest.Costs.LifeMoneyCost
{
    public class LifeMoneyCost : CustomCardCost
    {
        // this is a required field, and should be equal to the name you pass into the API when registering your cost
        public override string CostName => "LifeMoneyCost";

        // whether or not this cost's price has been satisfied by the card
        public override bool CostSatisfied(int cardCost, PlayableCard card)
        {
            // if the player has enough energy to pay the cost
            // takes the vanilla energy cost into account
            return cardCost <= (ResourcesManager.Instance.PlayerEnergy - card.EnergyCost);
        }

        // the dialogue that's played when you try to play a card with this cost, and CostSatisfied is false
        public override string CostUnsatisfiedHint(int cardCost, PlayableCard card)
        {
            return $"Eat your greens aby. {card.Info.DisplayedNameLocalized}";
        }

        // this is called after a card with this cost resolves on the board
        // if your cost spends a resource, this is where you'd put that logic
        public override IEnumerator OnPlayed(int cardCost, PlayableCard card)
        {
            // reduce the player's current energy by the card's cost
            yield return ResourcesManager.Instance.SpendEnergy(cardCost);
        }
    }
}
