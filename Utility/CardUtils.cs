using System;
using System.Collections.Generic;
using System.Reflection;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using UnityEngine;
using static UnityEngine.GridBrushBase;

namespace LifeCost.Utility
{
    public static class CardUtils
    {

        private static Assembly _assembly;

        public static Assembly CurrentAssembly
        {
            get
            {
                Assembly result;
                if ((result = CardUtils._assembly) == null)
                {
                    result = (CardUtils._assembly = Assembly.GetExecutingAssembly());
                }
                return result;
            }
        }

        public static Texture2D getImage(string path)
        {
            return TextureHelper.GetImageAsTexture(path, CardUtils.CurrentAssembly, 0);
        }

        public static Sprite getSprite(string path)
        {
            Sprite sprite = new Sprite();
            Texture2D image = CardUtils.getImage(path);
            return Sprite.Create(image, new Rect(0f, 0f, (float)image.width, (float)image.height), new Vector2(0.5f, 0.5f));
        }


        public static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue)
        {
            return new DialogueEvent.LineSet(new List<DialogueEvent.Line>
            {
                new DialogueEvent.Line
                {
                    text = dialogue
                }
            });
        }

        public static AbilityInfo CreateAbilityWithDefaultSettings(string rulebookName, string rulebookDescription, Type behavior, Texture2D text_a1, Sprite text_a2, string LearnDialogue, bool withDialogue = false, int powerLevel = 0, bool leshyUsable = false, bool part1Modular = true, bool stack = false)
        {
            AbilityInfo abilityInfo = AbilityManager.New("extraVoid.inscryption.LifeCost", rulebookName, rulebookDescription, behavior, text_a1);
            if (withDialogue)
            {
                abilityInfo.abilityLearnedDialogue = SetAbilityInfoDialogue(LearnDialogue);
            }
            abilityInfo.powerLevel = powerLevel;
            abilityInfo.activated = true;
            if (part1Modular)
            {
                abilityInfo.metaCategories = new List<AbilityMetaCategory>
                {
                    AbilityMetaCategory.Part1Modular,
                    0
                };
            }
            else
            {
                abilityInfo.metaCategories = new List<AbilityMetaCategory>
                {
                    0
                };
            }
            abilityInfo.pixelIcon = text_a2;
            abilityInfo.canStack = stack;
            return abilityInfo;
        }

        

        public static CardInfo CreateCardWithDefaultSettings(string InternalName, string DisplayName, int attack, int health, Texture2D texture_base, Texture2D texture_emission, List<CardMetaCategory> cardMetaCategories, List<Tribe> tribes, List<Trait> traits, List<Ability> abilities, Texture2D texture_pixel = null, int bloodCost = 0, int boneCost = 0, int energyCost = 0)
        {
            CardInfo cardInfo = CardManager.New("lifecost", InternalName, DisplayName, attack, health, "A puddle that errods all that touches it.");
            cardInfo.SetPortrait(texture_base, texture_emission, null);
            bool flag = texture_pixel != null;
            if (flag)
            {
                cardInfo.SetPixelPortrait(texture_pixel, null);
            }
            cardInfo.metaCategories = cardMetaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;
            for (int i = 0; i < abilities.Count; i++)
            {
                cardInfo.AddAbilities(new Ability[]
                {
                    abilities[i]
                });
            }
            cardInfo.temple = CardTemple.Nature;
            cardInfo.cardComplexity = CardComplexity.Intermediate;
            cardInfo.SetBloodCost(bloodCost);
            cardInfo.SetBonesCost(boneCost);
            cardInfo.SetEnergyCost(energyCost);
            return cardInfo;
        }
    }
}
