using System;

using AppKit;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class GameScene : SKScene
  {

    GameController GC;
    public GameScene(IntPtr handle) : base(handle) { }


    public override void DidMoveToView(SKView view)
    {
      var bgNode = SKSpriteNode.FromImageNamed("bg.jpg");
      var scale = Size.Width / bgNode.Size.Width;
      bgNode.SetScale(scale * 2);
      bgNode.Position = new CGPoint(Size.Width / 2, Size.Height / 2);
      AddChild(bgNode);
      bgNode.ZPosition = -1;

      PhysicsWorld.Gravity = new CGVector(0, 0);
      GC = new GameController(this);
      GC.SpawnPlayer();
      var colDelegate = new CollisionDelegate();
      colDelegate.Callbacks += NodesCollided;
      PhysicsWorld.ContactDelegate = colDelegate;
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


    private void NodesCollided(SKPhysicsContact contact)
    {
      GC.OnNodesCollision(contact);
    }
  }
}
