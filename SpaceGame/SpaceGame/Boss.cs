using System;
using CoreGraphics;
using SpriteKit;

namespace SpaceGame
{
  public class Boss : Enemy
  {
    public Boss(GameController controller) : base(controller, "Boss.png")
    {
      hp = 1000;
      shields = 1000;

      DefaultRotation = Math.PI / 2;
      Node.ZRotation += (nfloat)DefaultRotation;

      Node.SetScale(0.5f);

      Weapon = new TrippleLaserEnemyWeapon(this);
    }


    public override void Spawn()
    {
      double x = Controller.Scene.Size.Width + Node.Size.Width / 2;
      double y = Controller.Scene.Size.Height / 2;

      Node.Position = new CGPoint(x, y);

      Move();

      Controller.Scene.AddChild(Node);
    }


    protected override void Move()
    {
      double centerX = Controller.Scene.Size.Width / 2;
      double centerY = Controller.Scene.Size.Height / 2;

      var action1 = SKAction.MoveTo(new CGPoint(centerX, centerY), 3);

      //var action2 = SKAction.MoveBy(-50, -75, 0.5);
      //var seq = SKAction.Sequence(action1, action2);
      //var finalAction = SKAction.RepeatActionForever(seq);
      Node.RunAction(action1);
    }


    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }
  }
}
