using System;
using CoreGraphics;
using SpriteKit;

namespace SpaceGame
{
  public class Boss : Enemy
  {
    private double timer;
    private double delay;

    public Boss(GameController controller) : base(controller, "Boss.png")
    {
      hp = 1000;
      shields = 1000;

      DefaultRotation = Math.PI / 2;
      Node.ZRotation += (nfloat)DefaultRotation;

      Node.SetScale(0.5f);

      Weapon = new BossLaserWeapon(this);

      timer = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      GenerateRandomDelay();
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

      Node.RunAction(action1);
    }


    private SKAction GenerateMovingAction()
    {
      double xMinLimit = Node.Size.Width / 2;
      double xMaxLimit = Controller.Scene.Size.Width - xMinLimit;

      double yMinLimit = Node.Size.Height / 2;
      double yMaxLimit = Controller.Scene.Size.Height - yMinLimit;

      double randX = GMath.GenerateRandomInRange(xMinLimit, xMaxLimit);
      double randY = GMath.GenerateRandomInRange(yMinLimit, yMaxLimit);

      double randTime = GMath.GenerateRandomInRange(2, 5);

      var movePoint = new CGPoint(randX, randY);

      return SKAction.MoveTo(movePoint, randTime);
    }


    private void GenerateRandomDelay()
    {
      delay = GMath.GenerateRandomInRange(2000, 10000);
    }


    private void MoveToRandomPoint()
    {
      var moveAction = GenerateMovingAction();
      Node.RunAction(moveAction);
    }


    public void OnSceneUpdate()
    {
      double now = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      SetLookAtPlayer();

      if (now >= timer + delay)
      {
        timer = now;
        GenerateRandomDelay();
        MoveToRandomPoint();
      }
    }


    private void SetLookAtPlayer()
    {
      double playerVectorX = Controller.Player.Node.Position.X - Node.Position.X;
      double playerVectorY = Controller.Player.Node.Position.Y - Node.Position.Y;

      var playerDir = new CGPoint(playerVectorX, playerVectorY);
      playerDir = GMath.Normalize(playerDir);

      var lookDir = Controller.Player.LookDirection;

      double angle = GMath.Dot(playerDir, lookDir);

      if (playerDir.Y < 0)
        Node.ZRotation = (nfloat)(-Math.Acos(angle) - Math.PI / 2);
      else
      Node.ZRotation = (nfloat)(Math.Acos(angle) - Math.PI / 2);
    }



    public void ShootOnce()
    {
      Weapon.ShootOnce();
    }
  }
}
