using System;
using SpriteKit;
using PrintCore;
using CoreGraphics;
namespace SpaceGame
{
  public class GameController
  {
    private readonly Player _player;
    private readonly SKScene _scene;

    public SKSpriteNode Player { get => _player.Node; }

    public GameController(SKScene scene)
    {
      _scene = scene;
      _player = new Player("playerStartSprite.png");

      SpawnPlayer();
    }

    private void SpawnPlayer()
    {
      _player.Node.SetScale(0.25f);
      _player.Node.ZRotation = (nfloat)(-Math.PI / 2);
      _player.Node.Position =
        new CGPoint(_player.Node.Size.Width * 1.5, _scene.Size.Height * 0.5);
      _scene.AddChild(_player.Node);

      var lookDir = new CGPoint(_scene.Size.Width, 0);
      double lookLen = Math.Sqrt(Math.Pow(lookDir.X, 2) + Math.Pow(lookDir.Y, 2));
      _player.LookDirection = new CGPoint(
        lookDir.X / lookLen, lookDir.Y / lookLen);

    }

    public void RotatePlayer(CGPoint mousePosition)
    {
      var playerPosition = _player.Node.Position;

      var center = new CGPoint(_scene.Size.Width / 2, _scene.Size.Height / 2);

      var mouseLookDir = new CGPoint(
        mousePosition.X - playerPosition.X, mousePosition.Y - playerPosition.Y);

      double mouseLookDirLen = Math.Sqrt(
        Math.Pow(mouseLookDir.X, 2) + Math.Pow(mouseLookDir.Y, 2));

      var mouseLookDirNormalized = new CGPoint(
        mouseLookDir.X / mouseLookDirLen, mouseLookDir.Y / mouseLookDirLen);


      double angle = (
        mouseLookDirNormalized.X * _player.LookDirection.X +
        mouseLookDirNormalized.Y * _player.LookDirection.Y);


      if (mouseLookDirNormalized.Y < 0)
      {
        _player.Node.ZRotation = (nfloat)(-Math.Acos(angle) - Math.PI / 2);
      } 
      else
      {
        _player.Node.ZRotation = (nfloat)(Math.Acos(angle) - Math.PI / 2);
      }
    }
  }
}
