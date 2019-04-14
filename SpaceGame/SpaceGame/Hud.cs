
using CoreGraphics;
using AppKit;
using SpriteKit;

namespace SpaceGame
{
  public class Hud
  {
    public LineBar HPBar { get; }
    public LineBar ShieldsBar { get; }
    public SKLabelNode ScoreLabel { get; }
    private GameController controller;


    public Hud(GameController controller)
    {
      this.controller = controller;

      HPBar = new LineBar(
        controller, new CGPoint(160, controller.Scene.Size.Height - 50), NSColor.Orange)
      {
        StrTemplate = "HP:"
      };

      ShieldsBar = new LineBar(
        controller, new CGPoint(160, controller.Scene.Size.Height - 100), NSColor.Purple)
      {
        StrTemplate = "Shields:"
      };

      ScoreLabel = new SKLabelNode
      {
        FontColor = NSColor.Black,
        FontName = "Arial",
        FontSize = 20,
        Text = $"Score: {controller.PlayerScore}",
        ZPosition = 1,
        Position = new CGPoint(HPBar.Label.Position.X + 200, HPBar.Label.Position.Y)
      };

      controller.Scene.AddChild(ScoreLabel);
    }


    public void UpdateHudData()
    {
      HPBar.UpdateBarData(controller.Player.HP);
      ShieldsBar.UpdateBarData(controller.Player.Shields);
    }


    public void UpdateScore()
    {
      ScoreLabel.Text = $"Score: {controller.PlayerScore}";
    }
  }
}
