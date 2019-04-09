using System;
using SpriteKit;
using PrintCore;
using CoreGraphics;
namespace SpaceGame
{
  public class GameUnit : GameObject
  {
    protected double HP;
    protected double shields;
    protected SKScene _scene;
    public CGPoint LookDirection { get; set; }
    public IWeapon Weapon { get; set; }

    public GameUnit(SKScene scene, string spriteImgName, GameObjects type) : base(spriteImgName)
    {
      _scene = scene;
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