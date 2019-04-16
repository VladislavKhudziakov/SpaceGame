using System;
using CoreGraphics;

namespace SpaceGame
{
  public class Player : GameUnit
  {
    private int currWeaponLvl;

    public Player(GameController controller, string imgName)
      : base(controller, imgName, GameObjects.player)
    {
      currWeaponLvl = 1;
      hp = 100;
      shields = 100;

      DefaultRotation = -Math.PI / 2;
      Node.ZRotation += (nfloat)DefaultRotation;

      Node.SetScale(0.25f);

      Node.Position = 
        new CGPoint(Node.Size.Width * 1.5, Controller.Scene.Size.Height * 0.5);

      UpdateWeapon();

      LookDirection = GMath.Normalize(new CGPoint(Controller.Scene.Size.Width, 0));

      Controller.Scene.AddChild(Node);
    }


    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }


    public override void GetDamage(double incomeDmg)
    {
      base.GetDamage(incomeDmg);
      Controller.Hud.UpdateHudData();
    }

    public override void GetHeal(double incomeHeal)
    {
      base.GetHeal(incomeHeal);
      Controller.Hud.UpdateHudData();
    }

    public override void GetShields(double incomeArmor)
    {
      base.GetShields(incomeArmor);
      Controller.Hud.UpdateHudData();
    }


    public void UpWeaponLvl()
    {
      currWeaponLvl = currWeaponLvl >= 3 ? 3 : currWeaponLvl + 1;
      UpdateWeapon();
    }


    private void UpdateWeapon()
    {
      if (currWeaponLvl == 2)
      {
        Weapon = new DoubleLaserWeapon(this);
      }
      else if (currWeaponLvl == 3)
      {
        Weapon = new TripleLaserWeapon(this);
      }
      else
      {
        Weapon = new BaseLaserWeapon(this);
      }
    }
  }
}
