using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDR2;
using RDR2.Native;
using RDR2.Math;
using RDR2.UI;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

namespace ThunderStruck
{
    public class Main : Script
    {
        
        
        private Vector3 lastHitPos = Vector3.Zero;
        
        public Main()
        {
            Tick += OnTick;
            Interval = 1;
        }


        private void OnTick(object sender, EventArgs evt)
        {
            if (CurrentPlayerWeapon() != WeaponHash.Bow)
            {
                return;
            }
            var out69 = new OutputArgument();
            Function.Call(Hash.GET_PED_LAST_WEAPON_IMPACT_COORD, Game.Player.Character.Handle, out69);
            var pos = out69.GetResult<Vector3>();
            if (pos.DistanceTo(Game.Player.Character.Position) < 200f && lastHitPos != pos)
            {
                Function.Call((Hash)0x67943537D179597C, pos.X, pos.Y, pos.Z);
                lastHitPos = pos;
            }
        }


        private WeaponHash CurrentPlayerWeapon()
        {
            var out2 = new OutputArgument();
            Function.Call(Hash.GET_CURRENT_PED_WEAPON, Game.Player.Character.Handle, out2, 0, 0, 1);
            var hash = out2.GetResult<WeaponHash>();
            return hash;
        }
    }
}
