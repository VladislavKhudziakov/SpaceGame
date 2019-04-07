using System;
using SpriteKit;
namespace SpaceGame
{
  public interface IGameObject
  {
    SKSpriteNode Node { get; set; }
  }
}
