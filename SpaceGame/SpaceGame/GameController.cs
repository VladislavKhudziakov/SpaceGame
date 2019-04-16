using System;
using SpriteKit;
using CoreGraphics;
using AppKit;
using System.Collections.Generic;

namespace SpaceGame
{
  public delegate void UpdateDelegate();

  public class GameController
  {
    private readonly List<ushort> pressedKeys;
    private double lastTime;
    private double bonusTimer;
    private double bonusDelay;
    private Boss boss;


    public Player Player { get; set; }
    public List<GameUnit> SceneGameUnits { get; }
    public List<Bullet> BulletsInScene { get; }
    public List<Bonus> BonusesInScene { get; }
    public SKScene Scene { get; }
    public Hud Hud { get; }
    public int PlayerScore { get; set; }
    public UpdateDelegate SceneUpdateDelegate { get; set; }


    public GameController(SKScene scene)
    {
      Scene = scene;
      SceneGameUnits = new List<GameUnit>();
      BulletsInScene = new List<Bullet>();
      BonusesInScene = new List<Bonus>();
      pressedKeys = new List<ushort>();
      Hud = new Hud(this);
      boss = null;

      SceneUpdateDelegate = new UpdateDelegate(CheckBulletsForActions);
      SceneUpdateDelegate += CheckUnitsForActions;
      SceneUpdateDelegate += CheckForMovingPlayer;
      SceneUpdateDelegate += CheckForEnemySpawning;
      SceneUpdateDelegate += CheckForBonusCreating;
    }


    public void SpawnPlayer()
    {
      SpawnEnemy();
      GenerateBonusDelay();
      lastTime = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
      bonusTimer = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
      Player = new Player(this, "playerStartSprite.png");
      SceneGameUnits.Add(Player);
      Hud.UpdateHudData();
    }


    private void RotatePlayer(CGPoint mousePosition)
    {
      var playerPosition = Player.Node.Position;

      double mouseDirX = mousePosition.X - playerPosition.X;
      double mouseDirY = mousePosition.Y - playerPosition.Y;

      var mouseDirection = GMath.Normalize(new CGPoint(mouseDirX, mouseDirY));

      double angle = GMath.Dot(mouseDirection, Player.LookDirection);

      if (mouseDirection.Y < 0)
        Player.Node.ZRotation = (nfloat)(-Math.Acos(angle) - Math.PI / 2);
      else
        Player.Node.ZRotation = (nfloat)(Math.Acos(angle) - Math.PI / 2);
    }


    public void OnSceneUpdate(double currTime)
    {
      SceneUpdateDelegate();
    }


    public void OnSceneKeyDown(NSEvent theEvent)
    {
      if (!pressedKeys.Contains(theEvent.KeyCode))
      {
        if (theEvent.KeyCode == 49)
        {
          Player.ShootOnce();
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
      OnBulletCollision(contact);
      OnBonusCollision(contact);
      OnEnemyCollision(contact);
    }


    public void SpawnEnemy()
    {
      Enemy enemy = null;

      if (PlayerScore <= 5)
      {
        enemy = new Lvl1Enemy(this);
      }
      else if (PlayerScore <= 10)
      {
        enemy = new Lvl2Enemy(this);
      }
      else if (PlayerScore <= 15)
      {
        enemy = new Lvl3Enemy(this);
      }
      else if (PlayerScore <= 20)
      {
        enemy = new Lvl4Enemy(this);
      }
      else
      {
        boss = new Boss(this);
        enemy = boss;
        SceneUpdateDelegate += boss.OnSceneUpdate;
      }

      if (enemy != null)
      {
        enemy.Spawn();
        SceneGameUnits.Add(enemy);
      }
    }


    private void OnBulletCollision(SKPhysicsContact contact)
    {
      if (IsBulletContact(contact))
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


    private bool IsBulletContact(SKPhysicsContact contact)
    {
      bool isPlayerAndEnemyBullet =
        (contact.BodyA.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyB.CategoryBitMask == (uint)GameObjects.enemyBullet) ||
        (contact.BodyB.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyA.CategoryBitMask == (uint)GameObjects.enemyBullet);

      bool isEnemyAndPlayerBullet =
        (contact.BodyA.CategoryBitMask == (uint)GameObjects.enemy &&
        contact.BodyB.CategoryBitMask == (uint)GameObjects.playerBullet) ||
        (contact.BodyB.CategoryBitMask == (uint)GameObjects.enemy &&
        contact.BodyA.CategoryBitMask == (uint)GameObjects.playerBullet);

      return contact.BodyA.CategoryBitMask != contact.BodyB.CategoryBitMask &&
        isEnemyAndPlayerBullet || isPlayerAndEnemyBullet;
    }


    private void OnBonusCollision(SKPhysicsContact contact)
    {
      if (IsBonusContact(contact))
      {
        SKPhysicsBody BonusBody;

        if (contact.BodyA.CategoryBitMask == (uint)GameObjects.bonus)
        {
          BonusBody = contact.BodyA;
        }
        else
        {
          BonusBody = contact.BodyB;
        }

        var BonusObject = BonusesInScene.Find(
          (obj) => obj.ID.ToString() == BonusBody.Node.Name);

        if (BonusObject != null)
        {
          BonusObject.Get();
          DestroyBonus(BonusObject);
        }
      }
    }


    private bool IsBonusContact(SKPhysicsContact contact)
    {
      bool isPlayerAndBonusContact =
        (contact.BodyA.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyB.CategoryBitMask == (uint)GameObjects.bonus) ||
        (contact.BodyB.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyA.CategoryBitMask == (uint)GameObjects.bonus);

      bool isPlayerBulletAndBonus =
        (contact.BodyA.CategoryBitMask == (uint)GameObjects.playerBullet &&
        contact.BodyB.CategoryBitMask == (uint)GameObjects.bonus) ||
        (contact.BodyB.CategoryBitMask == (uint)GameObjects.playerBullet &&
        contact.BodyA.CategoryBitMask == (uint)GameObjects.bonus);

      return contact.BodyA.CategoryBitMask != contact.BodyB.CategoryBitMask &&
        isPlayerBulletAndBonus || isPlayerAndBonusContact;
    }


    private void OnEnemyCollision(SKPhysicsContact contact)
    {
      bool isPlayerAndEnemyContact =
        (contact.BodyA.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyB.CategoryBitMask == (uint)GameObjects.enemy) ||
        (contact.BodyB.CategoryBitMask == (uint)GameObjects.player &&
        contact.BodyA.CategoryBitMask == (uint)GameObjects.enemy);

      if (isPlayerAndEnemyContact)
      {
        SKPhysicsBody EnemyBody;
        SKPhysicsBody PlayerBody;

        if (contact.BodyA.CategoryBitMask == (uint)GameObjects.enemy ||
          contact.BodyA.CategoryBitMask == (uint)GameObjects.enemy)
        {
          EnemyBody = contact.BodyA;
          PlayerBody = contact.BodyB;
        }
        else
        {
          EnemyBody = contact.BodyB;
          PlayerBody = contact.BodyA;
        }

        var enemyObject = SceneGameUnits.Find(
          (obj) => obj.ID.ToString() == EnemyBody.Node.Name);





        if (enemyObject != null)
        {
          enemyObject.GetDamage(100);
          Player.GetDamage(10);
        }
        else if (EnemyBody.Node == boss.Node)
        {
          boss.GetDamage(50);
          Player.GetDamage(100);
        }
      }
    }


    private void DestroyBullet(Bullet bullet)
    {
      bullet.Node.RemoveFromParent();
      BulletsInScene.Remove(bullet);
    }


    private void DestroyBonus(Bonus bonus)
    {
      bonus.Node.RemoveFromParent();
      BonusesInScene.Remove(bonus);
    }


    private SKAction CreatePlayerMoveAction()
    {
      var endPoint = new CGPoint(0, 0);

      foreach (ushort keyCode in pressedKeys)
      {
        switch (keyCode)
        {
          case (ushort)GameKeyCodes.W:
            if (Player.Node.Position.Y + 1 < 
              Scene.Size.Height - Player.Node.Size.Height / 2)
                endPoint.Y += 1;
            break;
          case (ushort)GameKeyCodes.A:
            if (Player.Node.Position.X - 1 > Player.Node.Size.Width / 2)
              endPoint.X -= 1;
            break;
          case (ushort)GameKeyCodes.S:
            if (Player.Node.Position.Y - 1 > Player.Node.Size.Height / 2)
              endPoint.Y -= 1;
            break;
          case (ushort)GameKeyCodes.D:
            if (Player.Node.Position.X + 1 < 
              Scene.Size.Width - Player.Node.Size.Width / 2)
                endPoint.X += 1;
            break;
        }
      }

      return SKAction.MoveBy(endPoint.X * 5, endPoint.Y * 5, 0.1);
    }


    private void SpanwEnemyWithTimeOut(double spawnDelay)
    {
      double now = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      if (boss == null)
      {
        if (now >= lastTime + spawnDelay)
        {
          SpawnEnemy();
          lastTime = now;
        }
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
        if (SceneGameUnits[i] is Enemy unit)
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
      {
        DeleteUnit(unit);
        Player.GetDamage(10);
      }
        
    }


    private void CheckForMovingPlayer()
    {
      Player.Node.RunAction(CreatePlayerMoveAction());
    }


    private void DeleteUnit(GameUnit unit)
    {
      SceneGameUnits.Remove(unit);
      unit.Node.RemoveFromParent();
    }


    private void CheckForEnemySpawning()
    {
      SpanwEnemyWithTimeOut(2000);
    }


    private void CheckForBonusCreating()
    {
      double now = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      if (now >= bonusTimer + bonusDelay)
      {
        GenerateBonus();
        bonusTimer = now;
        GenerateBonusDelay();
      }
    }


    private void GenerateBonusDelay()
    {
      bonusDelay = GMath.GenerateRandomInRange(1000, 2000);
    }


    private Bonus GenerateBonus()
    {
      var randValue = GMath.GenerateRandomInRange(1, 4);

      if ((int)randValue == 1)
      {
        return new HealBonus(this);
      }

      if ((int)randValue == 2)
      {
        return new ShieldsBonus(this);
      }
 
      return new WeaponBonus(this);
    }
  }
}
