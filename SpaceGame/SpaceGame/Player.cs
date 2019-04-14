using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class Player : GameUnit
  {
    public Player(GameController controller, string imgName)
      : base(controller, imgName, GameObjects.player)
    {
      hp = 100;
      shields = 100;

      DefaultRotation = -Math.PI / 2;
      Node.ZRotation += (nfloat)DefaultRotation;

      Node.SetScale(0.25f);

      Node.Position = 
        new CGPoint(Node.Size.Width * 1.5, Controller.Scene.Size.Height * 0.5);

      Weapon = new PlayerWeapon(this);

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
  }
}
