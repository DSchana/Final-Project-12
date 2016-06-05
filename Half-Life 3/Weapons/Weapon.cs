﻿using Artemis.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Weapons
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
        /// The character that is holding this weapon
        /// </summary>
        public Character Holder { get; private set; }

        /// <summary>
        /// Damage done by weapon
        /// </summary>
        public int RangeDamage { get; private set; }

        /// <summary>
        /// Damage done by melee attack of weapon
        /// </summary>
        public int MeleeDamage { get; private set; }

        /// <summary>
        /// Damage range of weapon
        /// </summary>
        public int Range { get; private set; }

        /// <summary>
        /// Damage range of melee attack
        /// </summary>
        public int MeleeRange { get; private set; }

        /// <summary>
        /// Type of weapon
        /// </summary>
        public WeaponType TypeWeapon { get; private set; }

        /// <summary>
        /// Damage type, determines how the game will deal damage
        /// </summary>
        public DamageType TypeDamage { get; private set; }

        /// <summary>
        /// True if player is using this weapon
        /// </summary>
        public bool IsActive { get; set; }

        private Random rnd = new Random();

        public Weapon(Character character, WeaponType type)
        {
            TypeWeapon = type;
            IsActive = false;
            ClipAmmo = 0;
            Holder = character;

            if (TypeWeapon == WeaponType.USPMatch)
            {
                MaxClipSize = 18;
                MaxAmmo = 150;
                RangeDamage = 5;
                MeleeDamage = 4;
                Range = 1000;
                MeleeRange = 100;
                TypeDamage = DamageType.Hitscan;
            }
            else if (TypeWeapon == WeaponType.MP7)
            {
                MaxClipSize = 45;
                MaxAmmo = 225;
                RangeDamage = 4;
                MeleeDamage = 5;
                Range = 700;
                MeleeRange = 100;
                TypeDamage = DamageType.Hitscan;
            }
            else if (TypeWeapon == WeaponType.SPAS12)
            {
                MaxClipSize = 8;
                MaxAmmo = 125;
                RangeDamage = 30;
                MeleeDamage = 8;
                Range = 500;
                MeleeRange = 200;
                TypeDamage = DamageType.Hitscan;
            }
            else if (TypeWeapon == WeaponType.Knife)
            {
                MaxClipSize = 0;
                ClipAmmo = 1;
                RangeDamage = 0;
                MeleeDamage = 10;
                Range = 0;
                MeleeRange = 100;
                TypeDamage = DamageType.Melee;
            }
        }

        public void Update()
        {
            if (IsActive)
            {
                if (ClipAmmo == 0 && MaxAmmo > 0 && TypeWeapon != WeaponType.Knife)
                {
                    Reload();
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
            if (TypeWeapon != WeaponType.Knife)
            {
                ClipAmmo--;
            }
            // Animate
            Game1.CharManager.DealDamage(Holder);
        }

        public void Fire(DamageType damageType)
        {
            if (TypeWeapon != WeaponType.Knife)
            {
                ClipAmmo--;
            }
            // Animate
            Game1.CharManager.DealDamage(Holder, damageType);
        }
    }
}
