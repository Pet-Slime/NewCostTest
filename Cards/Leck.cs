using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using NewCostTest;
using NewCostTest.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NewCostTest.Cards
{
    public static class Leck
    {
        public static void AddCard()
        {
            string internalName = "lifecost_Leck";
            string displayName = "Leck";
            string description = "The Lost Beast, in the shape of a tooth, showing up only in error.";
            int attack = 5;
            int health = 1;
            int bloodCost = 0;
            int boneCost = 0;
            int energyCost = 0;
            List<CardMetaCategory> cardMetaCategories = new List<CardMetaCategory>();
            List<Tribe> tribes = new List<Tribe>();
            List<Ability> list = new List<Ability>();
            List<Trait> traits = new List<Trait>();
            Texture2D texture2D = TextureHelper.GetImageAsTexture("teck.png", typeof(Plugin).Assembly, 0);
            Texture2D texture2D2 = TextureHelper.GetImageAsTexture("pixel_teck.png", typeof(Plugin).Assembly, 0);
            Texture2D texture2D3 = TextureHelper.GetImageAsTexture("teck_e.png", typeof(Plugin).Assembly, 0);
            Texture2D texture_base = texture2D;
            Texture2D texture_emission = texture2D3;
            Texture2D texture_pixel = texture2D2;
            CardInfo cardInfo = CardUtils.CreateCardWithDefaultSettings(internalName, displayName, attack, health, texture_base, texture_emission, cardMetaCategories, tribes, traits, list, texture_pixel, bloodCost, boneCost, energyCost);
            cardInfo.description = description;
            cardInfo.SetCustomCost("LifeCost", 4);
            CardManager.Add("lifecost", cardInfo);
        }
    }
}

