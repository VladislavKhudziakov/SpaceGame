﻿using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class BaseLaserWeapon : Weapon
  {
    public BaseLaserWeapon(GameUnit weaponOwner) : base(weaponOwner) { }


    public override void ShootOnce()
    {
      CreateBullet(0);
    }


    protected virtual void CreateBullet(double offset)
    {
      var laserBeam = new PurpleLaser();

      laserBeam.Node.ZRotation =
       (nfloat)(owner.CurrentRotation + laserBeam.DefaultRotation);

      double radius = offset;

      double x = owner.Node.Position.X +
        radius * Math.Cos(owner.CurrentRotation + Math.PI / 2);

      double y = owner.Node.Position.Y +
        radius * Math.Sin(owner.CurrentRotation + Math.PI / 2);

      var finalPoint = new CGPoint(x, y);

      laserBeam.Node.Position = finalPoint;

      owner.Node.Scene?.AddChild(laserBeam.Node);

      owner.Controller.BulletsInScene.Add(laserBeam);

      laserBeam.Node.RunAction(SKAction.RepeatActionForever(CreateShootAction()));
    }
  }
}
