using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class Weapon
    {
        public int ClipAmmo { get; private set; }
        public int MaxClipSize { get; private set; }
        public int TotalAmmo { get; private set; }
        public int MaxAmmo { get; private set; }
        public int Damage { get; private set; }
        public WeaponType Type { get; private set; }

        public Weapon(WeaponType type)
        {
            Type = type;

            if (Type == WeaponType.USPMatch)
            {
                MaxClipSize = 18;
                MaxAmmo = 150;
                Damage = 5;
            }
            else if (Type == WeaponType.MP7)
            {
                MaxClipSize = 45;
                MaxAmmo = 225;
                Damage = 4;
            }
            else if (Type == WeaponType.SPAS12)
            {
                MaxClipSize = 8;
                MaxAmmo = 125;
                Damage = 30;
            }
        }
    }
}
