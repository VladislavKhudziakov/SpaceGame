using System;
using SpriteKit;
using CoreGraphics;
using AppKit;
namespace SpaceGame
{
  public class GameController
  {
    private Player _player;
    private readonly SKScene _scene;

    public SKSpriteNode Player { get => _player.Node; }

    public GameController(SKScene scene)
    {
      _scene = scene;
    }


    public void SpawnPlayer()
    {
      _player = new Player("playerStartSprite.png");
      Player.SetScale(0.25f);
      Player.ZRotation = (nfloat)(-Math.PI / 2);
      Player.Position = new CGPoint(
        Player.Size.Width * 1.5, _scene.Size.Height * 0.5);

      _scene.AddChild(_player.Node);

      _player.LookDirection = GMath.Normalize(new CGPoint(_scene.Size.Width, 0));
    }


    private void RotatePlayer(CGPoint mousePosition)
    {
      var playerPosition = _player.Node.Position;

      var mouseDirection = GMath.Normalize(new CGPoint(
          mousePosition.X - playerPosition.X, mousePosition.Y - playerPosition.Y
      ));

      double angle = GMath.Dot(mouseDirection, _player.LookDirection);

      if (mouseDirection.Y < 0)
      {
        Player.ZRotation = (nfloat)(-Math.Acos(angle) - Math.PI / 2);
      } 
      else
      {
        Player.ZRotation = (nfloat)(Math.Acos(angle) - Math.PI / 2);
      }
    }


    public void OnSceneUpdate(double currTime)
    {

    }


    public void OnSceneKeyDown(NSEvent theEvent)
    {

    }


    public void OnSceneMouseDrag(NSEvent theEvent)
    {
      RotatePlayer(theEvent.LocationInNode(_scene));
    }
  }
}
