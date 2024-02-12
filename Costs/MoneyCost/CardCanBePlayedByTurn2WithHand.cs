using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.MCost
{
    internal class CardCanBePlayedByTurn2WithHand
    {
        public static bool CanBePlayed(int amount, CardInfo card, List<CardInfo> hand)
        {
            // TestCost is just a copy of Energy, so any card that costs 2 or less will be playable by turn 2
            return amount <= Plugin.configFairHandCostM.Value;
        }
    }
}
