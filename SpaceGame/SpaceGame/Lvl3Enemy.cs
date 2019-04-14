﻿using System;
using CoreGraphics;
using SpriteKit;

namespace SpaceGame
{
  public class Lvl3Enemy : Enemy
  {
    public Lvl3Enemy(GameController controller) : base(controller, "Lvl3Enemy.png")
    {
      hp = 15;
      shields = 10;

      DefaultRotation = -Math.PI / 2;
      Node.ZRotation += (nfloat)DefaultRotation;

      Node.SetScale(0.5f / 5);

      Weapon = new DoubleLaserEnemyWeapon(this);
    }


    public override void Spawn()
    {
      var x = Controller.Scene.Size.Width + Node.Size.Width / 2;

      var y = GMath.GenerateRandomInRange(
        Node.Size.Height / 2, Controller.Scene.Size.Height - Node.Size.Height / 2);

      Node.Position = new CGPoint(x, y);

      Move();

      Controller.Scene.AddChild(Node);
    }


    protected override void Move()
    {
      var action1 = SKAction.MoveBy(-50, 75, 0.5);
      var action2 = SKAction.MoveBy(-50, -75, 0.5);
      var seq = SKAction.Sequence(action1, action2);
      var finalAction = SKAction.RepeatActionForever(seq);
      Node.RunAction(finalAction);
    }


    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }
  }
}
