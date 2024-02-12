using DiskCardGame;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using UnityEngine;
using static InscryptionAPI.CardCosts.CardCostManager;


namespace LifeCost.Costs
{
    internal class CostsToAdd
    {

        public static void AddCost()
        {

            Texture2D rewardBack = TextureHelper.GetImageAsTexture("CostChoiceBack.png", typeof(CostsToAdd).Assembly, 0);
            // when registering your card, you need to provide 2 Func's: one for grabbing the cost texture in the 3D Acts, and one for grabbing the pixel texture in Act 2
            // if your cost is exclusive to one part of the game, you can pass in null for the appropriate Func.
            FullCardCost lifeMoneyCost = Register(Plugin.PluginGuid, "LifeMoneyCost", typeof(HCost.LifeMoneyCost), HCost.Textures.Texture_3D, HCost.Textures.Texture_Pixel);
            lifeMoneyCost.SetCostTier(HCost.CostTier.CostTierH);
            lifeMoneyCost.ResourceType = (ResourceType)42;
            lifeMoneyCost.SetFoundAtChoiceNodes(true, rewardBack);

            FullCardCost lifeCost = Register(Plugin.PluginGuid, "LifeCost", typeof(LCost.LifeCost), LCost.Textures.Texture_3D, LCost.Textures.Texture_Pixel);
            lifeCost.SetCostTier(LCost.CostTier.CostTierL);
            lifeCost.ResourceType = lifeMoneyCost.ResourceType;

            FullCardCost moneyCost = Register(Plugin.PluginGuid, "MoneyCost", typeof(MCost.MoneyCost), MCost.Textures.Texture_3D, MCost.Textures.Texture_Pixel);
            moneyCost.SetCostTier(MCost.CostTier.CostTierM);
            moneyCost.ResourceType = lifeMoneyCost.ResourceType;

            if (Plugin.configFairHandActive.Value)
            {
                lifeMoneyCost.SetCanBePlayedByTurn2WithHand(HCost.CardCanBePlayedByTurn2WithHand.CanBePlayed);
                lifeCost.SetCanBePlayedByTurn2WithHand(LCost.CardCanBePlayedByTurn2WithHand.CanBePlayed);
                moneyCost.SetCanBePlayedByTurn2WithHand(MCost.CardCanBePlayedByTurn2WithHand.CanBePlayed);
            }
        }
    }
}
