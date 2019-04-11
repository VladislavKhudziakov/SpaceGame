using SpriteKit;
using System.Timers;
using System;

namespace SpaceGame
{
  public abstract class Enemy : GameUnit, IEnemy
  {
    public SKAction MovingAction { get; set; }
    protected double reloadTimer;
    protected double reloadDelay;

    protected Enemy(GameController controller, string imgName) 
      : base (controller, imgName, GameObjects.enemy) 
      {
        reloadTimer = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;
        reloadDelay = 1000;
      }

    
    public abstract void Spawn();
    protected abstract void Move();


    public virtual void TryShoot()
    {
      double now = new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds;

      if (now >= reloadTimer + reloadDelay)
      {
        Weapon.ShootOnce();
        reloadTimer = now;
      }
    }
  }
}
