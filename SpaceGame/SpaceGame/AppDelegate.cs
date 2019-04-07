using System;

using AppKit;
using SpriteKit;
using Foundation;

namespace SpaceGame
{
  public partial class AppDelegate : NSApplicationDelegate
  {
    public override void DidFinishLaunching(NSNotification notification)
    {
      var scene = SKNode.FromFile<GameScene>("GameScene");

      // Set the scale mode to scale to fit the window
      scene.ScaleMode = SKSceneScaleMode.AspectFill;

      MyGameView.PresentScene(scene);

      // SpriteKit applies additional optimizations to improve rendering performance
      MyGameView.IgnoresSiblingOrder = true;

      MyGameView.ShowsFPS = true;
      MyGameView.ShowsNodeCount = true;
      //MyGameView.ShowsPhysics = false;
    }

    public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
    {
      return true;
    }
  }
}
