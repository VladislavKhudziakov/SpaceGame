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
      var scene = new StartScene
      {
        ScaleMode = SKSceneScaleMode.ResizeFill,
        BackgroundColor = NSColor.White
      };

      MyGameView.PresentScene(scene);

      // SpriteKit applies additional optimizations to improve rendering performance
      MyGameView.IgnoresSiblingOrder = true;

      MyGameView.ShowsFPS = true;
      MyGameView.ShowsNodeCount = true;
      MyGameView.ShowsPhysics = true;
    }

    public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
    {
      return true;
    }
  }
}
