using DiskCardGame;
using System.Collections.Generic;
using InscryptionAPI.Card;
using System.Collections;
using System;
using System.Linq;

namespace LifeCost.Utility
{
    public static class ext
    {
        public static int LifeCost(this CardInfo info)
        {
            int? extendedPropertyAsInt = CardExtensions.GetExtendedPropertyAsInt(info, "LifeCost");
            return (extendedPropertyAsInt != null) ? extendedPropertyAsInt.Value : 0;
        }

        public static int MoneyCost(this CardInfo info)
        {
            int? extendedPropertyAsInt = CardExtensions.GetExtendedPropertyAsInt(info, "MoneyCost");
            return (extendedPropertyAsInt != null) ? extendedPropertyAsInt.Value : 0;
        }

        public static int LifeMoneyCost(this CardInfo info)
        {
            int? extendedPropertyAsInt = CardExtensions.GetExtendedPropertyAsInt(info, "LifeMoneyCost");
            return (extendedPropertyAsInt != null) ? extendedPropertyAsInt.Value : 0;
        }
    }


    public static class BackwardsCompatibility
    {
        public static void ChangeCardsToLifecost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.EnergyCost < 0
                                              select c)
                {
                    int num = cardInfo.EnergyCost * -1;
                    CardExtensions.SetExtendedProperty(cardInfo, "LifeMoneyCost", num);
                    cardInfo.SetEnergyCost(0);
                }
                return cards;
            };
        }

        public static void SetCardsToLifeMoneycost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.GetCustomCost("LifeMoneyCost") != 0
                                              select c)
                {
                    int num = cardInfo.GetCustomCost("LifeMoneyCost");
                    CardExtensions.SetExtendedProperty(cardInfo, "LifeMoneyCost", num);
                }
                return cards;
            };
        }

        public static void SetCardsToMoneycost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.GetCustomCost("MoneyCost") != 0
                                              select c)
                {
                    int num = cardInfo.GetCustomCost("MoneyCost");
                    CardExtensions.SetExtendedProperty(cardInfo, "MoneyCost", num);
                }
                return cards;
            };
        }

        public static void SetCardsToLifecost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.GetCustomCost("LifeCost") != 0
                                              select c)
                {
                    int num = cardInfo.GetCustomCost("LifeCost");
                    CardExtensions.SetExtendedProperty(cardInfo, "LifeCost", num);
                }
                return cards;
            };
        }

        public static void FixCardsToLifeMoneycost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.LifeMoneyCost() != 0
                                              select c)
                {
                    int num = cardInfo.LifeMoneyCost();
                    cardInfo.SetCustomCost("LifeMoneyCost", num);
                }
                return cards;
            };
        }

        public static void FixCardsToMoneycost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.MoneyCost() != 0
                                              select c)
                {
                    int num = cardInfo.MoneyCost();
                    cardInfo.SetCustomCost("MoneyCost", num);
                }
                return cards;
            };
        }

        public static void FixCardsToLifecost()
        {
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo cardInfo in from c in cards
                                              where c.LifeCost() != 0
                                              select c)
                {
                    int num = cardInfo.LifeCost();
                    cardInfo.SetCustomCost("LifeCost", num);
                }
                return cards;
            };
        }
    }
}
