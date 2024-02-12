using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.LCost
{
    internal class CostTier
    {
        public static int CostTierL (int amount)
        {
            return Mathf.FloorToInt(amount);
        }
    }
}
