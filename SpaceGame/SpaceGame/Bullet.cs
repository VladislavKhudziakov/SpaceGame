using System.Timers;
using System;

namespace SpaceGame
{
  public abstract class Bullet : GameObject, IBullet
  {
    protected GameObjects _type;
    protected readonly double _dmg;


    public double DMG { get => _dmg; }


    protected Bullet(double dmg, string imgName, GameObjects type) 
      : base(imgName)
    {
      _type = type;
      _dmg = dmg;
    }
  }
}
