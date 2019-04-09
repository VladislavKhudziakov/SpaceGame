using System;
using SpriteKit;
namespace SpaceGame
{
  public interface IEnemy
  {
    SKAction MovingAction { get; set; }
    void Spawn();
  }
}
