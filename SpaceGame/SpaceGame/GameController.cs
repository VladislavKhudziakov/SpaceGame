using System;
using SpriteKit;
using CoreGraphics;
using AppKit;
using System.Collections.Generic;

namespace SpaceGame
{
  public class GameController
  {
    private Player _player;
    private readonly SKScene _scene;
    private List<ushort> pressedKeys;

    public SKSpriteNode Player { get => _player.Node; set => _player.Node = value; }

    public GameController(SKScene scene)
    {
      _scene = scene;
      pressedKeys = new List<ushort>();
    }


    public void SpawnPlayer()
    {
      _player = new Player(_scene, "playerStartSprite.png");
    }


    private void RotatePlayer(CGPoint mousePosition)
    {
      var playerPosition = Player.Position;

      var mouseDirection = GMath.Normalize(new CGPoint(
          mousePosition.X - playerPosition.X, mousePosition.Y - playerPosition.Y
      ));

      double angle = GMath.Dot(mouseDirection, _player.LookDirection);

      if (mouseDirection.Y < 0)
        Player.ZRotation = (nfloat)(-Math.Acos(angle) - Math.PI / 2);
      else
        Player.ZRotation = (nfloat)(Math.Acos(angle) - Math.PI / 2);
    }


    public void OnSceneUpdate(double currTime)
    {
      Player.RunAction(CreatePlayerMoveAction());
    }


    public void OnSceneKeyDown(NSEvent theEvent)
    {

      if (!pressedKeys.Contains(theEvent.KeyCode))
      {
        if (theEvent.KeyCode == 49)
        {
          _player.ShootOnce();
        } 
        else
        {
          pressedKeys.Add(theEvent.KeyCode);
        }
      }

    }


    public void OnSceneKeyUp(NSEvent theEvent)
    {
      if (pressedKeys.Contains(theEvent.KeyCode))
      {
        pressedKeys.Remove(theEvent.KeyCode);
      }
    }


    public void OnSceneMouseDrag(NSEvent theEvent)
    {
      RotatePlayer(theEvent.LocationInNode(_scene));
    }


    private SKAction CreatePlayerMoveAction()
    {
      var endPoint = new CGPoint(0, 0);

      foreach (ushort keyCode in pressedKeys)
      {
        switch (keyCode)
        {
          case (ushort)GameKeyCodes.W:
            //
            if (Player.Position.Y + 1 < _scene.Size.Height - Player.Size.Height / 2)
              endPoint.Y += 1;
            break;
          case (ushort)GameKeyCodes.A:
            if (Player.Position.X - 1 > Player.Size.Width / 2)
              endPoint.X -= 1;
            break;
          case (ushort)GameKeyCodes.S:
            if (Player.Position.Y - 1 > Player.Size.Height / 2)
              endPoint.Y -= 1;
            break;
          case (ushort)GameKeyCodes.D:
            if (Player.Position.X + 1 < _scene.Size.Width - Player.Size.Width / 2)
              endPoint.X += 1;
            break;
        }
      }

      return SKAction.MoveBy(endPoint.X * 5, endPoint.Y * 5, 0.1);
    }


  }
}
