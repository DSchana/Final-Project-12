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
        public List<Weapon> Weapons { get; private set; }

        public PlayableCharacter(string name) : base(name)
        {
            Console.WriteLine("\nMAKING FREEMAN");
            ScreenPosition = new Vector2(ArtemisEngine.DisplayManager.WindowResolution.Width / 2, ArtemisEngine.DisplayManager.WindowResolution.Height / 2);

            Weapons = new List<Weapon>();

            Weapons.Add(new Weapon("USPMatch", this, WeaponType.USPMatch));
            Weapons.Add(new Weapon("MP7", this, WeaponType.MP7));
            Weapons.Add(new Weapon("SPAS12", this, WeaponType.SPAS12));
            Weapons.Add(new Weapon("Knife", this, WeaponType.Knife));  // Probably take this away from dr.freeman

            ChangeWeapon(0);

            Type = EntityType.PlayableCharacter;
            IsPlayable = true;

            SetMaxHealth(100);

            AddUpdater(UpdateWeapon);
            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);

            Sprites.LoadDirectory(@"Content\Resources\Gordon Freeman\Knife");
            Sprites.LoadDirectory(@"Content\Resources\Gordon Freeman\MP7");
            Sprites.LoadDirectory(@"Content\Resources\Gordon Freeman\SPAS12");
            Sprites.LoadDirectory(@"Content\Resources\Gordon Freeman\USPMatch");

            Console.WriteLine("MADE FREEMAN\n");
        }

        public void UpdateWeapon()
        {
            if (ArtemisEngine.Keyboard.IsClicked(Keys.D1))      // UPSMatch
            {
                ChangeWeapon(0);
            }
            else if (ArtemisEngine.Keyboard.IsClicked(Keys.D2))  // MP7
            {
                ChangeWeapon(1);
            }
            else if (ArtemisEngine.Keyboard.IsClicked(Keys.D3))  // SPAS12
            {
                ChangeWeapon(2);
            }
            else if (ArtemisEngine.Keyboard.IsClicked(Keys.D4))  //  Knife
            {
                ChangeWeapon(3);
            }
            else if (ArtemisEngine.Keyboard.IsClicked(Keys.D5))  // Nothing Yet
            {
                ChangeWeapon(4);
            }
        }

        public void ChangeWeapon(int weaponSlot)
        {
            Console.WriteLine(weaponSlot + " " + Weapons.Count);
            if (weaponSlot < Weapons.Count)
            {
                Console.WriteLine("CHANGING WEAPON");
                CurrentWeapon = Weapons[weaponSlot];
                Console.WriteLine(CurrentWeapon.Name);
            }
        }

        private void Rotate()
        {
            Vector2 direction = ArtemisEngine.Mouse.PositionVector - ScreenPosition;
            direction.Normalize();

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        private void Move()
        {
            bool isMoving = false;
            Vector2 NewWorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y);

            if (ArtemisEngine.Keyboard.IsHeld(Keys.W))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));
            }
            if (ArtemisEngine.Keyboard.IsHeld(Keys.A))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation + 90));
            }
            if (ArtemisEngine.Keyboard.IsHeld(Keys.S))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation));
            }
            if (ArtemisEngine.Keyboard.IsHeld(Keys.D))
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
            ChangeState("shoot");
            if (ArtemisEngine.Mouse.IsClicked(MouseButton.Left) && CurrentWeapon.ClipAmmo > 0)
            {
                CurrentWeapon.Fire();
            }
            else if (ArtemisEngine.Mouse.IsClicked(MouseButton.Right))
            {
                CurrentWeapon.Fire(DamageType.Melee);
            }
        }
    }
}