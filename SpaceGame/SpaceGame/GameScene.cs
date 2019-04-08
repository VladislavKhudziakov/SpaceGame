using System;

using AppKit;
using SpriteKit;
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
      PhysicsWorld.Gravity = new CGVector(0, 0);
      GC = new GameController(this);
      GC.SpawnPlayer();
    }


    public override void KeyDown(NSEvent theEvent)
    {
      GC.OnSceneKeyDown(theEvent);
    }

    public override void KeyUp(NSEvent theEvent)
    {
      GC.OnSceneKeyUp(theEvent);
    }


    public override void MouseDragged(NSEvent theEvent)
    {
      GC.OnSceneMouseDrag(theEvent);
    }


    public override void Update(double currentTime)
    {
      GC.OnSceneUpdate(currentTime);
    }
  }
}
