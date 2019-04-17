using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public abstract class Bonus : GameObject
  {
    protected readonly GameController controller;

    protected Bonus(string spriteImgName, GameController controller) 
      : base(spriteImgName) 
    {
      this.controller = controller;
      _node.PhysicsBody = SKPhysicsBody.CreateRectangularBody(_node.Size);
      _node.PhysicsBody.CategoryBitMask = (uint)GameObjects.bonus;
      _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.playerBullet;
      _node.PhysicsBody.CollisionBitMask = (uint)GameObjects.none;

      double maxY = controller.Scene.Size.Height - _node.Size.Height / 2;
      double minY = _node.Size.Height / 2;
      double y = GMath.GenerateRandomInRange(minY, maxY);
      double x = controller.Scene.Size.Width + _node.Size.Width / 2;

      _node.Position = new CGPoint(x, y);

      var movingAction = SKAction.MoveBy(-100, 0, 1);
      _node.RunActionAsync(SKAction.RepeatActionForever(movingAction));

      controller.Scene.AddChild(_node);

      controller.BonusesInScene.Add(this);
    }


    public abstract void Get();
  }
}
