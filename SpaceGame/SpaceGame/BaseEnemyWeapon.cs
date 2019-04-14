using System;
using CoreGraphics;
using SpriteKit;


namespace SpaceGame
{
  public class BaseEnemyWeapon : Weapon
  {
    public BaseEnemyWeapon(GameUnit owner) : base(owner) { }


    protected override SKAction CreateShootAction()
    {
      Mat3 rotationMatrix = new Mat3();
      rotationMatrix.SetRotation(-owner.CurrentRotation);

      Mat3 translationMatrix = new Mat3();
      translationMatrix.SetTranslation(owner.Node.Scene.Size.Width, 0);

      Mat3 transformation = rotationMatrix * translationMatrix;

      var moveAction = SKAction.MoveBy(
        -(nfloat)transformation[6], (nfloat)transformation[7], 2.5);

      var doneAction = SKAction.RemoveFromParent();

      return SKAction.Sequence(moveAction, doneAction);
    }


    public override void ShootOnce()
    {
      //CreateBullet(10);
      CreateBullet(0);
      //CreateBullet(-10);
    }

    protected virtual void CreateBullet(double offset)
    {
      var laserBeam = new RedLaser();

      laserBeam.Node.ZRotation =
       (nfloat)(owner.CurrentRotation + laserBeam.DefaultRotation);

      double radius = offset;

      double x = owner.Node.Position.X +
        radius * Math.Cos(owner.CurrentRotation + Math.PI / 2);

      double y = owner.Node.Position.Y +
        radius * Math.Sin(owner.CurrentRotation + Math.PI / 2);

      var finalPoint = new CGPoint(x, y);

      laserBeam.Node.Position = finalPoint;

      owner.Node.Scene.AddChild(laserBeam.Node);

      owner.Controller.BulletsInScene.Add(laserBeam);

      laserBeam.Node.RunAction(SKAction.RepeatActionForever(CreateShootAction()));
    }
  }
}
