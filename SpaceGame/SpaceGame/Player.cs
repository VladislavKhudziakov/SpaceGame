using System;
using SpriteKit;
using CoreGraphics;
namespace SpaceGame
{
  public class Player : GameUnit
  {
    public Player(SKScene scene, string imgName) 
      : base(scene, imgName, GameObjects.player) 
    {
      HP = 100;
      shields = 100;
      DefaultRotation = -Math.PI / 2;
      Node.SetScale(0.25f);
      Node.ZRotation += (nfloat)DefaultRotation;
      Node.Position = new CGPoint(Node.Size.Width * 1.5, _scene.Size.Height * 0.5);
      Weapon = new PlayerWeapon(this);
      LookDirection = GMath.Normalize(new CGPoint(_scene.Size.Width, 0));
      _scene.AddChild(Node);
    }


    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }
  }
}
