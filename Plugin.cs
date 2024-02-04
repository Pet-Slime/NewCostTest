using BepInEx;
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


        private void Awake () {


            Plugin.Log = base.Logger;
            Plugin.Directory = base.Info.Location.Replace("NewCostTestCost.dll", "");
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll();


            NewCostTest.Costs.CostsToAdd.AddCost();
            NewCostTest.Cards.Teck.AddCard();
            AddStartingDeck();


        }


        public static void AddStartingDeck()
        {


            Texture2D tex_a1 = TextureHelper.GetImageAsTexture("Dev_Test", typeof(Plugin).Assembly, 0);

            StarterDeckInfo NewCostDevTest = ScriptableObject.CreateInstance<StarterDeckInfo>();
            NewCostDevTest.title = "Pure Bone";
            NewCostDevTest.iconSprite = tex_a1.ConvertTexture(TextureHelper.SpriteType.StarterDeckIcon);
            NewCostDevTest.cards = new() { CardLoader.GetCardByName("lifecost_Teck"), CardLoader.GetCardByName("lifecost_Teck"), CardLoader.GetCardByName("lifecost_Teck") };

            StarterDeckManager.Add(Plugin.PluginGuid, NewCostDevTest);

        }
    }
}
