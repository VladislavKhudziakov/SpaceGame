
using SpriteKit;
using AppKit;
using CoreGraphics;
namespace SpaceGame
{
  public class EndScene : SKScene
  {
    private bool isWin;

    public EndScene(bool winFlag)
    {
      isWin = winFlag;
    }

    public override void DidMoveToView(SKView view)
    {
      var messageLabel = new SKLabelNode
      {
        Text = $"You {(isWin ? "win" : "loose")}",
        FontName = "Arial",
        FontSize = 40,
        FontColor = NSColor.Black,
        Position = new CGPoint(Size.Width / 2, Size.Height - 100),
        ZPosition = 1
      };

      var PressAnykeyLabel = new SKLabelNode
      {
        Text = $"Press any key for restart",
        FontName = "Arial",
        FontSize = 40,
        FontColor = NSColor.Black,
        Position = new CGPoint(Size.Width / 2, 100),
        ZPosition = 1
      };

      AddChild(messageLabel);
      AddChild(PressAnykeyLabel);
    }

    public override void KeyDown(NSEvent theEvent)
    {
      var scene = FromFile<GameScene>("GameScene");
      View.PresentScene(scene);
    }
  }
}
