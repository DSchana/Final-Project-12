using Artemis.Engine;
using Artemis.Engine.Input;
using Half_Life_3.Entities.Weapons;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities.Characters
{
    class PlayableCharacter : Character
    {
        private KeyboardInput KeyIn = new KeyboardInput();
        private MouseInput MouseIn = new MouseInput();

        // TODO: Populate
        public List<Weapon> Weapons { get; private set; }

        public PlayableCharacter(string name) : base(name)
        {
            ScreenPosition = new Vector2(ArtemisEngine.DisplayManager.WindowResolution.Width / 2, ArtemisEngine.DisplayManager.WindowResolution.Height / 2);

            Weapons = new List<Weapon>();

            Weapons.Add(new Weapon("USPMatch", this, WeaponType.USPMatch));
            Weapons.Add(new Weapon("MP7", this, WeaponType.MP7));
            Weapons.Add(new Weapon("SPAS12", this, WeaponType.SPAS12));
            Weapons.Add(new Weapon("Knife", this, WeaponType.Knife));  // Probably take this away from dr.freeman

            ChangeWeapon(0);

            Type = EntityType.PlayableCharacter;
            IsPlayable = true;
            Weapons = new List<Weapon>();

            SetMaxHealth(100);

            AddUpdater(UpdateWeapon);
            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);
        }

        public void UpdateWeapon()
        {
            if (KeyIn.IsClicked(Keys.D1))
            {
                ChangeWeapon(0);
            }
            else if (KeyIn.IsClicked(Keys.D2))
            {
                ChangeWeapon(1);
            }
            else if (KeyIn.IsClicked(Keys.D3))
            {
                ChangeWeapon(2);
            }
            else if (KeyIn.IsClicked(Keys.D4))
            {
                ChangeWeapon(3);
            }
            else if (KeyIn.IsClicked(Keys.D5))
            {
                ChangeWeapon(4);
            }
        }

        public void ChangeWeapon(int weaponSlot)
        {
            if (weaponSlot < Weapons.Count)
            {
                CurrentWeapon = Weapons[weaponSlot];
            }
        }

        private void Rotate()
        {
            Vector2 direction = MouseIn.PositionVector - ScreenPosition;
            direction.Normalize();

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        private void Move()
        {
            bool isMoving = false;
            Vector2 NewWorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y);

            if (KeyIn.IsHeld(Keys.W))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));
            }
            if (KeyIn.IsHeld(Keys.A))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation + 90));
            }
            if (KeyIn.IsHeld(Keys.S))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation));
            }
            if (KeyIn.IsHeld(Keys.D))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation + 90));
            }

            if (isMoving)
            {
                ChangeState("move");
            }
            else
            {
                ChangeState("idle");
            }

            WorldPosition = NewWorldPosition;
        }

        private void Attack()
        {
            //ChangeState("Attack");
            if (MouseIn.IsClicked(MouseButton.Left) && CurrentWeapon.ClipAmmo > 0)
            {
                CurrentWeapon.Fire();
            }
            else if (MouseIn.IsClicked(MouseButton.Right))
            {
                CurrentWeapon.Fire(DamageType.Melee);
            }
        }
    }
}