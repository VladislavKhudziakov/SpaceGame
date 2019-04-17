using System;
using SpriteKit;
using CoreGraphics;
using System.Runtime.CompilerServices;
using AppKit;

namespace SpaceGame
{
  public class StartScene : SKScene
  {
    public StartScene()
    {
      //ScaleMode = SKSceneScaleMode.;
    }


    public override void DidMoveToView(SKView view)
    {
      var InfoLabel = new SKLabelNode
      {
        Text = "W/A/S/D for moving, spase for shooting",
        FontName = "Arial",
        FontSize = 40,
        FontColor = NSColor.Black,
        Position = new CGPoint(Size.Width / 2, Size.Height - 60),
        ZPosition = 1
      };

      var shieldsNode = SKSpriteNode.FromImageNamed("shield2.png");
      shieldsNode.SetScale(0.25f / 2);
      shieldsNode.Position = new CGPoint(
        InfoLabel.Position.X - shieldsNode.Size.Width, InfoLabel.Position.Y - 70);

      var shieldsLabel = new SKLabelNode
      {
        Text = "Shields Bonus",
        FontName = "Arial",
        FontSize = 20,
        FontColor = NSColor.Black,
        Position = new CGPoint(
          InfoLabel.Position.X + shieldsNode.Size.Width, shieldsNode.Position.Y),
        ZPosition = 1
      };

      var healNode = SKSpriteNode.FromImageNamed("heal2.png");
      healNode.SetScale(0.25f);
      healNode.Position = new CGPoint(
        shieldsNode.Position.X, shieldsNode.Position.Y - shieldsNode.Size.Height - 20);

      var healLabel = new SKLabelNode
      {
        Text = "Heal Bonus",
        FontName = "Arial",
        FontSize = 20,
        FontColor = NSColor.Black,
        Position = new CGPoint(
          InfoLabel.Position.X + healNode.Size.Width, healNode.Position.Y),
        ZPosition = 1
      };

      var weaponNode = SKSpriteNode.FromImageNamed("laser.png");
      weaponNode.SetScale(0.125f);
      weaponNode.Position = new CGPoint(
        healNode.Position.X, healNode.Position.Y - healNode.Size.Height - 10);

      var weaponLabel = new SKLabelNode
      {
        Text = "Weapon Bonus",
        FontName = "Arial",
        FontSize = 20,
        FontColor = NSColor.Black,
        Position = new CGPoint(
          InfoLabel.Position.X + weaponNode.Size.Width, weaponNode.Position.Y),
        ZPosition = 1
      };

      var PressToStartLabel = new SKLabelNode
      {
        Text = "Press Any Key to start",
        FontName = "Arial",
        FontSize = 40,
        FontColor = NSColor.Black,
        Position = new CGPoint(
          InfoLabel.Position.X, weaponNode.Position.Y - weaponNode.Size.Height * 2),
        ZPosition = 1
      };


      AddChild(InfoLabel);
      AddChild(PressToStartLabel);
      AddChild(shieldsNode);
      AddChild(shieldsLabel);
      AddChild(healNode);
      AddChild(healLabel);
      AddChild(weaponNode);
      AddChild(weaponLabel);
    }


    public override void KeyDown(NSEvent theEvent)
    {
      //base.KeyDown(theEvent);
      var scene = FromFile<GameScene>("GameScene");
      View.PresentScene(scene);
    }
  }
}
