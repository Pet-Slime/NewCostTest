using DiskCardGame;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LifeCost.Costs.MCost
{
    internal class Textures
    {
        public static Texture2D Texture_3D(int cardCost, CardInfo info, PlayableCard card)
        {
            return TextureHelper.GetImageAsTexture($"MoneyCost_{cardCost}.png", typeof(Plugin).Assembly);
        }

        public static Texture2D Texture_Pixel(int cardCost, CardInfo info, PlayableCard card)
        {
            // if you want the API to handle adding stack numbers, you can instead provide a 7x8 texture like so:
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture("MoneyCost_pixel.png", typeof(Plugin).Assembly));
        }
    }
}
