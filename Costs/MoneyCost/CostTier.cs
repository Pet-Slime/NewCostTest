using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.MCost
{
    internal class CostTier
    {
        public static int CostTierM(int amount)
        {
            return Mathf.FloorToInt(amount / 5f);
        }
    }
}
