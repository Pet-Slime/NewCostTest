using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NewCostTest
{

    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(APIGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(ZGUID, BepInDependency.DependencyFlags.SoftDependency)]


    public partial class Plugin : BaseUnityPlugin
    {
        public const string APIGUID = "cyantist.inscryption.api";
        public const string PluginGuid = "extraVoid.inscryption.NewCostTest";
        public const string ZGUID = "community.inscryption.patch";
        private const string PluginName = "New Cost Test";
        private const string PluginVersion = "1.0.0";
        public static string Directory;
        internal static ManualLogSource Log;
        internal static ConfigEntry<bool> configFairHandActive;
        internal static ConfigEntry<int> configFairHandCostL;
        internal static ConfigEntry<int> configFairHandCostM;
        internal static ConfigEntry<int> configFairHandCostH;


        private void Awake () {

            
            Plugin.Log = base.Logger;
            Plugin.Directory = base.Info.Location.Replace("NewCostTest.dll", "");
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();
            Plugin.configFairHandActive = base.Config.Bind<bool>("Fair Hand", "Active", true, "Should this mod post-fix patch fair hand to include the Money, Life, and Hybrid Costs?");
            Plugin.configFairHandCostL = base.Config.Bind<int>("Fair Hand", "Life Cost", 4, "The value in which the card should not show up in fair hand.");
            Plugin.configFairHandCostM = base.Config.Bind<int>("Fair Hand", "Money Cost", 2, "The value in which the card should not show up in fair hand.");
            Plugin.configFairHandCostH = base.Config.Bind<int>("Fair Hand", "Hybrid Cost", 2, "The value in which the card should not show up in fair hand.");


            NewCostTest.Costs.CostsToAdd.AddCost();
            NewCostTest.Cards.Teck.AddCard();
            NewCostTest.Cards.Meck.AddCard();
            NewCostTest.Cards.Leck.AddCard();
            AddStartingDeck();


        }


        public static void AddStartingDeck()
        {

            
            Texture2D tex_a1 = TextureHelper.GetImageAsTexture("Dev_Test.png", typeof(Plugin).Assembly, 0);

            StarterDeckInfo NewCostDevTest = ScriptableObject.CreateInstance<StarterDeckInfo>();
            NewCostDevTest.title = "NewCost Dev Test";
            NewCostDevTest.iconSprite = tex_a1.ConvertTexture(TextureHelper.SpriteType.StarterDeckIcon);
            NewCostDevTest.cards = new() { CardLoader.GetCardByName("lifecost_Teck"), CardLoader.GetCardByName("lifecost_Meck"), CardLoader.GetCardByName("lifecost_Leck") };

            StarterDeckManager.Add(Plugin.PluginGuid, NewCostDevTest);

        }

        [HarmonyPatch(typeof(PlayableCard), "CanPlay")]
        public class CanPlay_patch
        {
            [HarmonyPostfix]
            public static void Postfix(ref PlayableCard __instance, ref bool __result)
            {
                Plugin.Log.LogMessage(Singleton<LifeManager>.Instance.Balance.ToString());
            }
        }
    }
}
