using System;
using SpriteKit;
namespace SpaceGame
{
  public class GameUnit : GameObject
  {
    public GameUnit(string spriteImgName, GameObjects type) : base(spriteImgName)
    {
      _node.PhysicsBody = SKPhysicsBody.CreateRectangularBody(_node.Size);
      _node.PhysicsBody.CategoryBitMask = (uint)type;

      if (type == GameObjects.player)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.enemy;
      }
      else if (type == GameObjects.enemy)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.player;
      }

      _node.PhysicsBody.CollisionBitMask = (uint)GameObjects.none;
    }
  }
}