using DiskCardGame;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using UnityEngine;

namespace LifeCost.Costs
{
    internal class CostsToAdd
    {

        public static void AddCost()
        {
            // when registering your card, you need to provide 2 Func's: one for grabbing the cost texture in the 3D Acts, and one for grabbing the pixel texture in Act 2
            // if your cost is exclusive to one part of the game, you can pass in null for the appropriate Func.
            CardCostManager.Register(Plugin.PluginGuid, "LifeMoneyCost", typeof(LifeCost.Costs.HCost.LifeMoneyCost), TextureMethod_LifeMoney, PixelTextureMethod_LifeMoney);
            CardCostManager.Register(Plugin.PluginGuid, "LifeCost", typeof(LifeCost.Costs.LCost.LifeCost), TextureMethod_Life, PixelTextureMethod_Life);
            CardCostManager.Register(Plugin.PluginGuid, "MoneyCost", typeof(LifeCost.Costs.MCost.MoneyCost), TextureMethod_Money, PixelTextureMethod_Money);
        }

        public static Texture2D TextureMethod_Life(int cardCost, CardInfo info, PlayableCard card)
        {
            return TextureHelper.GetImageAsTexture(string.Format("LifeCost_{0}.png", cardCost), typeof(Plugin).Assembly);
        }

        public static Texture2D PixelTextureMethod_Life(int cardCost, CardInfo info, PlayableCard card)
        {
            // if you want the API to handle adding stack numbers, you can instead provide a 7x8 texture like so:
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture("LifeCost_pixel.png", typeof(Plugin).Assembly));
        }

        public static Texture2D TextureMethod_Money(int cardCost, CardInfo info, PlayableCard card)
        {
            return TextureHelper.GetImageAsTexture(string.Format("MoneyCost_{0}.png", cardCost), typeof(Plugin).Assembly);
        }

        public static Texture2D PixelTextureMethod_Money(int cardCost, CardInfo info, PlayableCard card)
        {
            // if you want the API to handle adding stack numbers, you can instead provide a 7x8 texture like so:
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture("MoneyCost_pixel.png", typeof(Plugin).Assembly));
        }


        public static Texture2D TextureMethod_LifeMoney(int cardCost, CardInfo info, PlayableCard card)
        {
            return TextureHelper.GetImageAsTexture(string.Format("LifeMoneyCost_{0}.png", cardCost), typeof(Plugin).Assembly);
        }

        public static Texture2D PixelTextureMethod_LifeMoney(int cardCost, CardInfo info, PlayableCard card)
        {
            // if you want the API to handle adding stack numbers, you can instead provide a 7x8 texture like so:
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture("LifeMoneyCost_pixel.png", typeof(Plugin).Assembly));
        }
    }
}
