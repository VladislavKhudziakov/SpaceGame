using System;
using SpriteKit;
namespace SpaceGame
{
  public class LaserBullet : Bullet
  { 
    public LaserBullet(string imgName, GameObjects type) : base(imgName, type)
    {
      _node.PhysicsBody = SKPhysicsBody.CreateRectangularBody(_node.Size);
      _node.PhysicsBody.CategoryBitMask = (uint)type;

      if (type == GameObjects.playerBullet)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.enemy;
      }
      else if (type == GameObjects.enemyBullet)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.player;
      }

      _node.PhysicsBody.CollisionBitMask = (uint)GameObjects.none;
    }
  }
}
