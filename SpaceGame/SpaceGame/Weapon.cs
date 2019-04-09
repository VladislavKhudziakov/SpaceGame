using SpriteKit;
using System;

namespace SpaceGame
{
  public abstract class Weapon : IWeapon
  {
    protected GameUnit owner;


    protected Weapon(GameUnit weaponOwner) { owner = weaponOwner; }


    protected virtual SKAction CreateShootAction()
    {
      Mat3 rotationMatrix = new Mat3();
      rotationMatrix.SetRotation(-owner.CurrentRotation);

      Mat3 translationMatrix = new Mat3();
      translationMatrix.SetTranslation(owner.Node.Scene.Size.Width, 0);

      Mat3 transformation = rotationMatrix * translationMatrix;

      var moveAction = SKAction.MoveBy(
        (nfloat)transformation[6], (nfloat)transformation[7], 1);

      var doneAction = SKAction.RemoveFromParent();

      return SKAction.Sequence(moveAction, doneAction);
    }


    public abstract void ShootOnce();
  }
}
