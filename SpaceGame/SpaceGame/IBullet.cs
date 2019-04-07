using System;

using SpriteKit;
using CoreGraphics;
namespace SpaceGame
{
  public interface IBullet
  {
    void ShootOnce(CGPoint from);
    SKAction MoveAction { get; }
  }
}
