using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;

namespace SpaceGame
{
  public class GameScene : SKScene
  {

    GameController GC;
    public GameScene(IntPtr handle) : base(handle)
    {
    }

    public override void DidMoveToView(SKView view)
    {
      GC = new GameController(this);
      PhysicsWorld.Gravity = new CGVector(0, 0);
    }

    public override void MouseDown(NSEvent theEvent)
    {
    }

    public override void Update(double currentTime)
    {
    }
  }
}
