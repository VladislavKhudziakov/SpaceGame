
using CoreGraphics;
using AppKit;
using SpriteKit;

namespace SpaceGame
{
  public class Hud
  {

    public LineBar HPBar { get; }
    public LineBar ShieldsBar { get; }
    private GameController controller;


    public Hud(GameController controller)
    {
      this.controller = controller;

      HPBar = new LineBar(
        controller, new CGPoint(160, controller.Scene.Size.Height - 50),
        NSColor.Orange)
      {
        StrTemplate = "HP:"
      };

      ShieldsBar = new LineBar(
        controller, new CGPoint(160, controller.Scene.Size.Height - 100),
        NSColor.Purple)
      {
        StrTemplate = "Shields:"
      };
    }


    public void UpdateHudData()
    {
      HPBar.UpdateBarData(controller.Player.HP);
      ShieldsBar.UpdateBarData(controller.Player.Shields);
    }
  }
}
