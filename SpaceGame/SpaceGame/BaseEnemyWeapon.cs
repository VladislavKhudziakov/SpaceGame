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
      var laserBeam = new RedLaser();
      laserBeam.Node.Position = new CGPoint(owner.Node.Position);

      laserBeam.Node.ZRotation =
        (nfloat)(owner.CurrentRotation + laserBeam.DefaultRotation);

      owner.Node.Scene.AddChild(laserBeam.Node);

      owner.Controller.BulletsInScene.Add(laserBeam);

      laserBeam.Node.RunAction(SKAction.RepeatActionForever(CreateShootAction()));
    }
  }
}
