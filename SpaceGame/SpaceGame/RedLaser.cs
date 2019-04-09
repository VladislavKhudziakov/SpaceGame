using System;
namespace SpaceGame
{
  public class RedLaser : LaserBullet
  {
    public RedLaser() : base(5, "redLaser.png", GameObjects.enemyBullet) 
    {
      DefaultRotation = -Math.PI / 2;
      Node.SetScale(0.125f / 3);
      Node.ZRotation += (nfloat)DefaultRotation;
    }
  }
}
