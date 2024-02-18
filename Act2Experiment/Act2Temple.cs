using GBC;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.GridBrushBase;
using UnityEngine;
using System;
using System.Collections.Generic;
using DiskCardGame;
using GBC;
using HarmonyLib;
using InscryptionAPI.Card;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using LifeCost.Utility;
using InscryptionAPI.Guid;

namespace LifeCost.Act2Experiment
{
    internal class Act2Temple
    {
        [HarmonyPatch(typeof(CollectionUI), "Start")]
        public class AddLifeTab
        {
            // Token: 0x0600005C RID: 92 RVA: 0x0000891C File Offset: 0x00006B1C
            public static void Postfix(ref CollectionUI __instance)
            {
                if (!BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("mrfantastik.inscryption.infact2"))
                {
                    AddTab(__instance);
                }

            }
            public static void AddTab(CollectionUI instance)
            {
                GameObject gameObject = Object.Instantiate<GameObject>(instance.gameObject.transform.Find("MainPanel").Find("Tabs").Find("Tab_4").gameObject);
                gameObject.name = "Tab_5";
                gameObject.transform.parent = instance.gameObject.transform.Find("MainPanel").Find("Tabs");
                gameObject.transform.localPosition = new Vector3(-0.718f, 0.175f, 0);
                instance.tabButtons.Add(gameObject.GetComponent<GenericUIButton>());
                gameObject.GetComponent<GenericUIButton>().inputKey = KeyCode.Alpha5;
                gameObject.GetComponent<GenericUIButton>().OnButtonDown = instance.gameObject.transform.Find("MainPanel").Find("Tabs").Find("Tab_4").gameObject.GetComponent<GenericUIButton>().OnButtonDown;
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.55f, 0.44f);
                gameObject.gameObject.transform.Find("Icon").gameObject.GetComponent<SpriteRenderer>().sprite = Utility.CardUtils.getSprite("life_temple_tab.png");
            }
        }


        [HarmonyPatch(typeof(CollectionUI), "CreatePages")]
        public class SortLifeCards
        {
            // Token: 0x0600006E RID: 110 RVA: 0x00009488 File Offset: 0x00007688
            public static void Postfix(ref CollectionUI __instance, ref List<List<CardInfo>> __result, ref List<CardInfo> cards)
            {

                if (!BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("mrfantastik.inscryption.infact2"))
                {
                    __result = Test(__instance, __result, cards);
                }
            }



            public static List<List<CardInfo>> Test(CollectionUI Instance, List<List<CardInfo>> Result, List<CardInfo> cards)
            {
                cards.Sort(delegate (CardInfo a, CardInfo b)
                {
                    int num7 = a.temple - b.temple;
                    bool flag12 = num7 != 0;
                    int result;
                    if (flag12)
                    {
                        result = num7;
                    }
                    else
                    {
                        int num8 = a.metaCategories.Contains(CardMetaCategory.Rare) ? 1 : 0;
                        int num9 = (b.metaCategories.Contains(CardMetaCategory.Rare) ? 1 : 0) - num8;
                        bool flag13 = num9 != 0;
                        if (flag13)
                        {
                            result = num9;
                        }
                        else
                        {
                            int costTier = a.CostTier - b.CostTier;
                            bool flag14 = costTier != 0;
                            if (flag14)
                            {
                                result = costTier;
                            }
                            else
                            {
                                int boneCost = a.BonesCost - b.BonesCost;
                                bool flag15 = boneCost != 0;
                                if (flag15)
                                {
                                    result = boneCost;
                                }
                                else
                                {
                                    int gemsCost = ((a.GemsCost.Count == 1) ? a.GemsCost[0] : GemType.Green) - ((b.GemsCost.Count == 1) ? b.GemsCost[0] : GemType.Green);
                                    bool flag16 = gemsCost != 0;
                                    if (flag16)
                                    {
                                        result = gemsCost;
                                    }
                                    else
                                    {
                                        int name = a.DisplayedNameEnglish.CompareTo(b.DisplayedNameEnglish);
                                        bool flag17 = name == 0;
                                        if (flag17)
                                        {
                                            result = a.name.CompareTo(b.name);
                                        }
                                        else
                                        {
                                            result = name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return result;
                });
                cards = new List<CardInfo>(cards);
                List<CardInfo> toRemove = new List<CardInfo>();
                for (int i = 1; i < cards.Count; i++)
                {
                    bool flag = cards[i].name == cards[i - 1].name;
                    if (flag)
                    {
                        toRemove.Add(cards[i]);
                    }
                }
                cards.RemoveAll((CardInfo x) => toRemove.Contains(x));
                List<List<CardInfo>> list = new List<List<CardInfo>>();
                List<CardInfo> AllBoons = new List<CardInfo>();
                int num = 0;
                for (int j = 0; j < cards.Count; j++)
                {
                    int num2 = num / 8;
                    bool flag2 = num2 >= list.Count;
                    if (flag2)
                    {
                        list.Add(new List<CardInfo>());
                    }

                    bool flag3 = false;
                    if (cards[j] != null && (cards[j].LifeCost() != 0 || cards[j].LifeMoneyCost() != 0 || cards[j].MoneyCost() != 0))
                    {
                        flag3 = true;
                    }
                    if (flag3)
                    {
                        num++;
                        AllBoons.Add(cards[j]);
                        list[num2].Add(cards[j]);
                    }
                }
                cards.RemoveAll((CardInfo x) => AllBoons.Contains(x));
                List<List<CardInfo>> list2 = new List<List<CardInfo>>();
                list2.Add(new List<CardInfo>());
                Instance.tabPageIndices = new int[5];
                for (int k = 0; k < Instance.tabPageIndices.Length; k++)
                {
                    Instance.tabPageIndices[k] = 0;
                }
                for (int l = 0; l < cards.Count; l++)
                {
                    List<CardInfo> list3 = list2[list2.Count - 1];
                    bool flag4 = l == 0;
                    if (flag4)
                    {
                        int temple = (int)cards[l].temple;
                        for (int m = 0; m < temple; m++)
                        {
                            list2.Add(new List<CardInfo>());
                            Instance.tabPageIndices[m + 1] = list2.IndexOf(list3) + 1;
                            list3 = list2[list2.Count - 1];
                        }
                    }
                    list3.Add(cards[l]);
                    bool flag5 = l == cards.Count - 1;
                    bool flag6 = !flag5;
                    if (flag6)
                    {
                        int temple2 = (int)cards[l].temple;
                        int temple3 = (int)cards[l + 1].temple;
                        int num3 = temple3 - temple2 - 1;
                        for (int n = 0; n < num3; n++)
                        {
                            list2.Add(new List<CardInfo>());
                            Instance.tabPageIndices[temple2 + 1 + n] = list2.IndexOf(list3) + 1;
                            list3 = list2[list2.Count - 1];
                        }
                        bool flag7 = !flag5 && temple2 != temple3;
                        bool flag8 = list3.Count >= 8 || flag7;
                        if (flag8)
                        {
                            list2.Add(new List<CardInfo>());
                            bool flag9 = flag7;
                            if (flag9)
                            {
                                Instance.tabPageIndices[temple3] = list2.IndexOf(list3) + 1;
                            }
                        }
                    }
                }
                for (int num4 = 0; num4 < Instance.tabPageIndices.Length; num4++)
                {
                    bool flag10 = num4 > 0 && Instance.tabPageIndices[num4] == 0;
                    if (flag10)
                    {
                        list2.Add(new List<CardInfo>());
                        Instance.tabPageIndices[num4] = list2.Count - 1;
                    }
                }
                list2[list2.Count - 1] = list[0];
                int num5 = list2.Count - 1;
                bool flag11 = list.Count > 0;
                if (flag11)
                {
                    for (int num6 = 1; num6 < list.Count; num6++)
                    {
                        list2.Add(list[num6]);
                    }
                }
                Instance.tabPageIndices[4] = num5;
                return list2;
            }
        }
    }
}

