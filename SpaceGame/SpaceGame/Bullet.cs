using System;
using SpriteKit;
using CoreGraphics;
namespace SpaceGame
{
  public abstract class Bullet : GameObject, IBullet
  {
    protected SKAction _moveAction;
    protected GameObjects _type;

    public SKAction MoveAction { get => _moveAction; }

    protected Bullet(string imgName, GameObjects type) : base(imgName)
    {
      _type = type;
      _moveAction = SKAction.MoveBy(10, 0, 0.5);
    }

    public void ShootOnce(CGPoint bulletPos)
    {
      _node.Position = bulletPos;
      _node.RunAction(_moveAction);
    }
  }
}
