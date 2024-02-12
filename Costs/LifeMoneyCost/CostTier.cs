using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.HCost
{
    internal class CostTier
    {
        public static int CostTierH(int amount)
        {
            return Mathf.FloorToInt(amount / 3f);
        }

    }
}
