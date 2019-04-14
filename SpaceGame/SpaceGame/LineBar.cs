using System;
using AppKit;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{

  public class LineBar
  {
    private GameController controller;
    private CGPoint position;
    private NSColor color;


    public SKLabelNode Label { get; }
    public SKShapeNode Line { get; set; }
    public SKShapeNode Border { get; }
    public double MaxLineWidth { get; }
    public double MaxlineHeigth { get; }
    public string StrTemplate { get; set; }

    public LineBar(GameController controller, CGPoint position, NSColor color)
    {
      this.controller = controller;
      this.position = position;
      this.color = color;

      MaxLineWidth = 300;
      MaxlineHeigth = 10;


      Line = SKShapeNode.FromRect(new CGSize(MaxLineWidth, MaxlineHeigth));
      Line.FillColor = color;
      Line.Position = position;
      Line.ZPosition = 1;

      Border = SKShapeNode.FromRect(new CGSize(MaxLineWidth, MaxlineHeigth));
      Border.StrokeColor = NSColor.Black;
      Border.Position = position;
      Border.ZPosition = 1;

      Label = new SKLabelNode
      {
        FontColor = NSColor.Black,
        FontName = "Arial",
        FontSize = 20,
        Text = "txt",
        ZPosition = 1
      };

      Label.Position = new CGPoint(
        Line.Position.X, Line.Position.Y + Label.FontSize / 2);

      controller.Scene.AddNodes(Line, Border, Label);
    }

    public void UpdateBarData(double data)
    {

      Label.Text = StrTemplate + $" { data } / 100";

      double widthHpAspect = MaxLineWidth / 100;
      double HPBarWidth = data * widthHpAspect;
      controller.Scene.RemoveChildren(new SKNode[] { Line });
      double HPBarHeight = MaxlineHeigth;
      
      Line = SKShapeNode.FromRect(new CGSize(HPBarWidth, HPBarHeight));
      Line.Position = 
        new CGPoint(position.X - (MaxLineWidth - HPBarWidth) / 2, position.Y);
      Line.FillColor = color;

      controller.Scene.AddChild(Line);
    }
  }
}
