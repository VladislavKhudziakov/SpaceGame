﻿using System;
using SpriteKit;
using CoreGraphics;
using AppKit;
using System.Collections.Generic;
using PrintCore;

namespace SpaceGame
{
  public class GameController
  {
    private Player _player;
    private readonly List<ushort> pressedKeys;
    private double lastTime;

    public SKSpriteNode Player { get => _player.Node; set => _player.Node = value; }
    public List<GameUnit> SceneGameUnits { get; }
    public List<Bullet> BulletsInScene { get; set; }
    public SKScene Scene { get; }


    public GameController(SKScene scene)
    {
      Scene = scene;
      SceneGameUnits = new List<GameUnit>();
      BulletsInScene = new List<Bullet>();
      pressedKeys = new List<ushort>();
    }


    public void SpawnPlayer()
    {
      SpawnEnemy();
      lastTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
      _player = new Player(this, "playerStartSprite.png");
      SceneGameUnits.Add(_player);
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
      CheckBulletsForActions();
      CheckUnitsForActions();
      SpanwEnemyWithTimeOut(2000);
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
      RotatePlayer(theEvent.LocationInNode(Scene));
    }


    public void OnNodesCollision(SKPhysicsContact contact)
    {

      if (contact.BodyA.CategoryBitMask == (uint)GameObjects.enemyBullet ||
          contact.BodyB.CategoryBitMask == (uint)GameObjects.enemyBullet ||
          contact.BodyA.CategoryBitMask ==(uint)GameObjects.playerBullet ||
          contact.BodyB.CategoryBitMask == (uint)GameObjects.playerBullet )
      {
        OnBulletCollision(contact);
      }
    }


    public void SpawnEnemy()
    {
      var enemy = new BaseEnemy(this);
      enemy.Spawn();
      SceneGameUnits.Add(enemy);
    }


    private void OnBulletCollision(SKPhysicsContact contact)
    {
      bool isDifferentTypes =
          contact.BodyA.CategoryBitMask != contact.BodyB.CategoryBitMask &&
          !(contact.BodyA.CategoryBitMask == (uint)GameObjects.player &&
          contact.BodyB.CategoryBitMask == (uint)GameObjects.playerBullet) &&
          !(contact.BodyA.CategoryBitMask == (uint)GameObjects.enemy &&
          contact.BodyB.CategoryBitMask == (uint)GameObjects.enemyBullet);

      if (isDifferentTypes)
      {
        SKPhysicsBody bulletBody;
        SKPhysicsBody otherBody;

        if (contact.BodyA.CategoryBitMask == (uint)GameObjects.playerBullet ||
          contact.BodyA.CategoryBitMask == (uint)GameObjects.enemyBullet)
        {
          bulletBody = contact.BodyA;
          otherBody = contact.BodyB;
        }
        else
        {
          bulletBody = contact.BodyB;
          otherBody = contact.BodyA;
        }

        var bulletObject = BulletsInScene.Find(
          (obj) => obj.ID.ToString() == bulletBody.Node.Name);

        var otherObject = SceneGameUnits.Find(
          (obj) => obj.ID.ToString() == otherBody.Node.Name);

        if (otherObject != null && bulletObject != null)
        {
          otherObject.GetDamage(bulletObject.DMG);
          DestroyBullet(bulletObject);
        }
      }
    }


    private void DestroyBullet(Bullet bullet)
    {
      bullet.Node.RemoveFromParent();
      BulletsInScene.Remove(bullet);
    }


    private SKAction CreatePlayerMoveAction()
    {
      var endPoint = new CGPoint(0, 0);

      foreach (ushort keyCode in pressedKeys)
      {
        switch (keyCode)
        {
          case (ushort)GameKeyCodes.W:

            if (Player.Position.Y + 1 < Scene.Size.Height - Player.Size.Height / 2)
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
            if (Player.Position.X + 1 < Scene.Size.Width - Player.Size.Width / 2)
              endPoint.X += 1;
            break;
        }
      }

      return SKAction.MoveBy(endPoint.X * 5, endPoint.Y * 5, 0.1);
    }

    private void SpanwEnemyWithTimeOut(double spawnDelay)
    {
      double now = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      if (now >= lastTime + spawnDelay)
      {
        SpawnEnemy();
        lastTime = now;
      }
        
    }


    private void CheckBulletsForActions()
    {
      for (int i = 0; i < BulletsInScene.Count; i++)
      {
        var bullet = BulletsInScene[i];

        CheckIsBulletOutOfScreen(bullet);
      }
    }


    private void CheckIsBulletOutOfScreen(Bullet bullet)
    {
      bool isNotInScreen = bullet.Node.Position.X + bullet.Node.Size.Width < 0 ||
          bullet.Node.Position.X + bullet.Node.Size.Width > Scene.Size.Width ||
          bullet.Node.Position.Y + bullet.Node.Size.Height < 0 ||
          bullet.Node.Position.Y + bullet.Node.Size.Height > Scene.Size.Width;

      if (isNotInScreen)
        DestroyBullet(bullet);
    }




    private void CheckUnitsForActions()
    {
      for (int i = 0; i < SceneGameUnits.Count; i++)
      {
        var unit = SceneGameUnits[i] as Enemy;

        if (unit != null)
        {
          CheckIsUnitOutOfScreen(unit);
          unit.TryShoot();
        }

      }
    }


    private void CheckIsUnitOutOfScreen(GameUnit unit)
    {
      bool isNotInScreen = unit.Node.Position.X + unit.Node.Size.Width < 0 ||
          unit.Node.Position.Y + unit.Node.Size.Height > Scene.Size.Width ||
          unit.Node.Position.Y + unit.Node.Size.Height < 0;

      if (isNotInScreen)
        DeleteUnit(unit);
    }


    private void DeleteUnit(GameUnit unit)
    {
      SceneGameUnits.Remove(unit);
      unit.Node.RemoveFromParent();
    }
  }
}
