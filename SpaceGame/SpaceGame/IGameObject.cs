using System;
using SpriteKit;
namespace SpaceGame
{
  public interface IGameObject
  {
    SKSpriteNode Node { get; set; }
    double DefaultRotation { get; set; }
    double CurrentRotation { get; }
    Guid ID { get; }

  }
}
