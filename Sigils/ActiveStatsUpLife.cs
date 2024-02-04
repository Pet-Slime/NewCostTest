using System.Collections;
using DiskCardGame;
using UnityEngine;

namespace LifeCost.Sigils
{
    public class lifecost_ActivateStatsUpLife : LifeActiveAbilityCost
    {

        public static Ability ability;

        private const string MOD_ID = Plugin.PluginGuid + "_statsUp";

        public override Ability Ability
        {
            get
            {
                return lifecost_ActivateStatsUpLife.ability;
            }
        }

        public override int LifeCost
        {
            get
            {
                return 3;
            }
        }

        public override IEnumerator Activate()
        {
            bool flag = !SaveManager.SaveFile.IsPart2;
            bool flag3 = flag;
            if (flag3)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, true);
            }
            CardModificationInfo mod = base.Card.TemporaryMods.Find((CardModificationInfo x) => x.singletonId == MOD_ID);
            bool flag2 = mod == null;
            bool flag4 = flag2;
            if (flag4)
            {
                mod = new CardModificationInfo();
                mod.singletonId = MOD_ID;
                base.Card.AddTemporaryMod(mod);
            }
            mod.attackAdjustment++;
            mod.healthAdjustment++;
            base.Card.OnStatsChanged();
            yield return new WaitForSeconds(0.25f);
            bool flag5 = flag;
            if (flag5)
            {
                Singleton<ViewManager>.Instance.Controller.LockState = 0;
            }
            yield break;
        }
    }
}