using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs
{
    internal class CostTierPatch
    {
        [HarmonyPatch(typeof(CardInfo), "CostTier", MethodType.Getter)]
        public class CardInfo_CostTier_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(ref int __result, ref CardInfo __instance)
            {
                __result += Mathf.RoundToInt((float)__instance.GetCustomCost("LifeMoneyCost") / 3f) 
                    + Mathf.RoundToInt((float)__instance.GetCustomCost("LifeCost")) 
                    + Mathf.RoundToInt((float)__instance.GetCustomCost("MoneyCost") / 5f);
            }
        }

        [HarmonyPatch(typeof(Deck), "CardCanBePlayedByTurn2WithHand", 0)]
        public class Deck_CardCanBePlayedByTurn2WithHand
        {
            [HarmonyPostfix]
            public static void Postfix(ref CardInfo card, List<CardInfo> hand, ref bool __result, ref Deck __instance)
            {
                if (Plugin.configFairHandActive.Value) {
                    bool flag1 = card.GetCustomCost("LifeCost") <= Plugin.configFairHandCostL.Value;

                    bool flag2 = card.GetCustomCost("LifeMoneyCost") <= Plugin.configFairHandCostH.Value;

                    bool flag3 = card.GetCustomCost("MoneyCost") <= Plugin.configFairHandCostM.Value;
                    __result = (__result && flag1 && flag2 && flag3);
                }
            }
        }
    }
}
