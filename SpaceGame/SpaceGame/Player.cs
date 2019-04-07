using System;
using SpriteKit;
using CoreGraphics;
namespace SpaceGame
{
  public class Player : GameUnit
  {
    public CGPoint LookDirection { get; set; }
    public Player(string imgName) : base(imgName, GameObjects.player) 
    {
      _node.Position = new CGPoint(0, 0);
      LookDirection = new CGPoint(1, 0);
    }
  }
}
