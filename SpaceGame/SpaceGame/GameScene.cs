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

    //public override void MouseDown(NSEvent theEvent)
    //{
    //  GC.RotatePlayer(theEvent.LocationInNode(this));
    //}


    public override void MouseDragged(NSEvent theEvent)
    {
      GC.RotatePlayer(theEvent.LocationInNode(this));
    }


    public override void Update(double currentTime)
    {
    }
  }
}
