using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;

namespace ContextualCover
{
    public class Main : Script
    {
        bool inCover = false;
        bool weaponOnly = false;
        public Main()
        {
            Tick += onTick;
            loadSettings();
        }
        public void loadSettings()
        {
            weaponOnly = Settings.GetValue<bool>("SETTINGS", "weaponOnly", false);
        }
        public void cover()
        {
            Ped player = Game.Player.Character;
            if (!player.IsInCover)
            {
                inCover = false;
                GTA.UI.Screen.ShowHelpText("NOT", 1000, false, false);
            }
            if (player.HasCollided && !inCover)
            {
                //GTA.UI.Screen.ShowHelpText("COVER", 1000, false, false);
                inCover = true;
                Function.Call(Hash._SET_CONTROL_NORMAL, 0, 44, 100f);
            }
        }
        public void onTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            if (player.IsOnFoot)
            {
                //GTA.UI.Screen.ShowHelpText("COMB", 1000);
                if (weaponOnly && player.Weapons.Current.Group != WeaponGroup.Unarmed)
                {
                    cover();
                }
                else if (!weaponOnly)
                {
                    cover();
                }
            }
        }
    }
}
