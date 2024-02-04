using System.Collections;
using DiskCardGame;
using UnityEngine;

namespace LifeCost.Sigils
{

    public class lifecost_ActiveStatsUpMoney : LifeActiveAbilityCost
    {

        public static Ability ability;

        private const string MOD_ID = Plugin.PluginGuid + "_statsUp";


        public override Ability Ability
        {
            get
            {
                return lifecost_ActiveStatsUpMoney.ability;
            }
        }

        public override int MoneyCost
        {
            get
            {
                return 5;
            }
        }

        public override IEnumerator Activate()
        {
            CardModificationInfo mod = base.Card.TemporaryMods.Find((CardModificationInfo x) => x.singletonId == MOD_ID);
            bool flag = mod == null;
            if (flag)
            {
                mod = new CardModificationInfo();
                mod.singletonId = MOD_ID;
                base.Card.AddTemporaryMod(mod);
            }
            mod.attackAdjustment++;
            mod.healthAdjustment++;
            base.Card.OnStatsChanged();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, true);
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.Controller.LockState = 0;
            yield break;
        }

    }
}