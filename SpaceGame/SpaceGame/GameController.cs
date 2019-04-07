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
    }
  }
}
