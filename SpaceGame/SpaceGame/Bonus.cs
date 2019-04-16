using System;
using SpriteKit;

namespace SpaceGame
{
  public class Bonus : GameObject
  {
    public delegate void BonusDelegate(Player player);
    private readonly GameController controller;


    public Bonus(string spriteImgName, GameController controller) 
      : base(spriteImgName) 
    {
      this.controller = controller;

      _node.PhysicsBody = SKPhysicsBody.CreateRectangularBody(_node.Size);
      _node.PhysicsBody.CategoryBitMask = (uint)GameObjects.bonus;
      _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.playerBullet;
      _node.PhysicsBody.CollisionBitMask = (uint)GameObjects.none;
    }


    public void Get(BonusDelegate action)
    {
      action(controller.Player);
    }
  }
}
