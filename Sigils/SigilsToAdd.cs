using DiskCardGame;
using UnityEngine;
using InscryptionAPI.Helpers;
using NewCostTest.Utility;

namespace NewCostTest.Sigils
{
    internal class SigilsToAdd
    {
        public static void AddSigils()
        {
            AddActivateEnergyGamble();
            AddActivateLifeConverter();
            AddActivateLifeRandomStatsUp();
            AddActiveCashConverter();
            AddActiveStatsUpLife();
            AddActiveStatsUpMoney();
        }


        private static void AddActivateEnergyGamble()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecost_ActivateEnergyGamble.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecost_ActivateEnergyGamble_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 0;
            bool leshyUsable = true;
            bool part1Modular = false;
            bool stack = false;
            lifecost_ActivateEnergyGamble.ability = CardUtils.CreateAbilityWithDefaultSettings("Max Energy Gamble", "Pay 6 energy to put 0 to 3 damage on someone's side of the scale", typeof(lifecost_ActivateEnergyGamble), text_a, text_a2, "Money for Blood", true, powerLevel, leshyUsable, part1Modular, stack).ability;
        }

        private static void AddActivateLifeRandomStatsUp()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecost_ActivateLifeRandomStatsUp.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecost_ActivateLifeRandomStatsUp_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 2;
            bool leshyUsable = true;
            bool part1Modular = false;
            bool stack = false;
            AbilityInfo abilityInfo = CardUtils.CreateAbilityWithDefaultSettings("Die Roll", "Pay 5 Life/Foils to gain between 0 and 6 increase in stats, distributed randomly", typeof(lifecost_ActivateLifeRandomStatsUp), text_a, text_a2, "Sing it once, Sing it twice, take a chance and roll the dice!", true, powerLevel, leshyUsable, part1Modular, stack);
            abilityInfo.activated = true;
            lifecost_ActivateLifeRandomStatsUp.ability = abilityInfo.ability;
        }

        private static void AddActiveCashConverter()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecost_CashConverter.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecost_CashConverter_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 2;
            bool leshyUsable = true;
            bool part1Modular = false;
            bool stack = false;
            lifecost_ActivateCashConverter.ability = CardUtils.CreateAbilityWithDefaultSettings("Cash Converter", "Pay 4 foils to put 1 damage on your opponent's side of the scale", typeof(lifecost_ActivateCashConverter), text_a, text_a2, "Money for Blood", true, powerLevel, leshyUsable, part1Modular, stack).ability;
        }

        private static void AddActivateLifeConverter()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecost_LifeConverter.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecost_LifeConverter_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 3;
            bool leshyUsable = true;
            bool part1Modular = false;
            bool stack = false;
            lifecost_ActivateLifeConverter.ability = CardUtils.CreateAbilityWithDefaultSettings("Life Converter", "Pay 2 life to gain 1 foils", typeof(lifecost_ActivateLifeConverter), text_a, text_a2, "Blood for money", true, powerLevel, leshyUsable, part1Modular, stack).ability;
        }

        private static void AddActiveStatsUpLife()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecost_ActivateStatsUpLife.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecost_ActivateStatsUpLife_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 3;
            bool leshyUsable = true;
            bool part1Modular = true;
            bool stack = false;
            AbilityInfo abilityInfo = CardUtils.CreateAbilityWithDefaultSettings("Vamperic Strength", "Pay 3 life to increase the power and health of this card by 1", typeof(lifecost_ActivateStatsUpLife), text_a, text_a2, "Hurting oneself can lead to an increase in strength.", true, powerLevel, leshyUsable, part1Modular, stack);
            abilityInfo.activated = true;
            lifecost_ActivateStatsUpLife.ability = abilityInfo.ability;
        }

        private static void AddActiveStatsUpMoney()
        {
            Texture2D text_a = TextureHelper.GetImageAsTexture("lifecostActivateStatsUpMoney.png", typeof(Plugin).Assembly);
            Sprite text_a2 = TextureHelper.GetImageAsSprite("lifecostActivateStatsUpMoney_a2.png", typeof(Plugin).Assembly, TextureHelper.SpriteType.PixelActivatedAbilityIcon);
            int powerLevel = 3;
            bool leshyUsable = true;
            bool part1Modular = false;
            bool stack = false;
            lifecost_ActiveStatsUpMoney.ability = CardUtils.CreateAbilityWithDefaultSettings("Greedy Strength", "Pay 5 currency to increase the power and health of this card by 1", typeof(lifecost_ActiveStatsUpMoney), text_a, text_a2, "One can be hired to do many tasks", true, powerLevel, leshyUsable, part1Modular, stack).ability;
        }
    }
}
