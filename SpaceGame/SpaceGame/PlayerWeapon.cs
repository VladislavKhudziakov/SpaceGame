
using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class PlayerWeapon : Weapon
  {
    public PlayerWeapon(GameUnit weaponOwner) : base(weaponOwner) { }


    public override void ShootOnce()
    {
      var laserBeam = new PurpleLaser();
      
      laserBeam.Node.Position = new CGPoint(owner.Node.Position);

      laserBeam.Node.ZRotation = 
        (nfloat)(owner.CurrentRotation + laserBeam.DefaultRotation);

      owner.Node.Scene.AddChild(laserBeam.Node);

      owner.Controller.BulletsInScene.Add(laserBeam);

      laserBeam.Node.RunAction(SKAction.RepeatActionForever(CreateShootAction()));
    }
  }
}
