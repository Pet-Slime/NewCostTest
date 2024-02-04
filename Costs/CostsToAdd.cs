using DiskCardGame;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using NewCostTest;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NewCostTest.Costs
{
    internal class CostsToAdd
    {

        public static void AddCost()
        {
            // when registering your card, you need to provide 2 Func's: one for grabbing the cost texture in the 3D Acts, and one for grabbing the pixel texture in Act 2
            // if your cost is exclusive to one part of the game, you can pass in null for the appropriate Func.
            CardCostManager.Register(Plugin.PluginGuid, "LifeMoneyCost", typeof(NewCostTest.Costs.LifeMoneyCost.LifeMoneyCost), TextureMethod, PixelTextureMethod);
        }


        public static Texture2D TextureMethod(int cardCost, CardInfo info, PlayableCard card)
        {
            return TextureHelper.GetImageAsTexture(string.Format("LifeMoneyCost_{0}.png", cardCost), typeof(Plugin).Assembly, 0);
        }

        public static Texture2D PixelTextureMethod(int cardCost, CardInfo info, PlayableCard card)
        {
            // if you want the API to handle adding stack numbers, you can instead provide a 7x8 texture like so:
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture("LifeMoneyCost_pixel.png", typeof(Plugin).Assembly, 0));
        }
    }
}
