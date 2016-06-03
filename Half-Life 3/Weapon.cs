using Artemis.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class Weapon
    {
        /// <summary>
        /// Current amount of ammo in clip
        /// </summary>
        public int ClipAmmo { get; private set; }

        /// <summary>
        /// Amx possible amount of ammo in clip
        /// </summary>
        public int MaxClipSize { get; private set; }

        /// <summary>
        /// Current amount of ammo avalible
        /// </summary>
        public int TotalAmmo { get; private set; }

        /// <summary>
        /// Total amount of ammo
        /// </summary>
        public int MaxAmmo { get; private set; }

        /// <summary>
        /// Damage done by weapon
        /// </summary>
        public int Damage { get; private set; }

        /// <summary>
        /// Type of weapon
        /// </summary>
        public WeaponType Type { get; private set; }

        /// <summary>
        /// True if player is using this weapon
        /// </summary>
        public bool IsActive { get; set; }

        private Random rnd = new Random();
        private MouseInput mouseIn = new MouseInput();

        public Weapon(WeaponType type)
        {
            Type = type;
            IsActive = false;
            ClipAmmo = 0;

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
            else if (Type == WeaponType.Knife)
            {
                MaxClipSize = 0;
                ClipAmmo = 1;
                Damage = 60;
            }
        }

        public void Update()
        {
            if (IsActive)
            {
                if (ClipAmmo == 0 && MaxAmmo > 0 && Type != WeaponType.Knife)
                {
                    Reload();
                }
                if (mouseIn.IsClicked(MouseButton.Left) && ClipAmmo > 0)
                {
                    Fire();
                }
            }
        }

        public void Reload()
        {
            // Animate
            if (MaxAmmo > MaxClipSize)
            {
                TotalAmmo -= MaxClipSize;
                ClipAmmo = MaxClipSize;
            }
            else
            {
                ClipAmmo = TotalAmmo;
                TotalAmmo = 0;
            }
        }

        public void Fire()
        {
            if (Type != WeaponType.Knife)
            {
                ClipAmmo--;
            }
            // Animate
            // Deal damage
        }
    }
}
