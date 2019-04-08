using System;
using SpriteKit;
using CoreGraphics;
namespace SpaceGame
{
  public class Player : GameUnit
  {
    public CGPoint LookDirection { get; set; }
    public IWeapon Weapon { get; set; }


    public Player(string imgName) : base(imgName, GameObjects.player) 
    {
      DefaultRotation = -Math.PI / 2;
      Node.SetScale(0.25f);
      Node.ZRotation += (nfloat)DefaultRotation;
      Weapon = new PlayerWeapon(this);
    }

    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }
  }
}
