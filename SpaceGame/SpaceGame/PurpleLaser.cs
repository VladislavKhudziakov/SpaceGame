using System;
namespace SpaceGame
{
  public class PurpleLaser : LaserBullet
  {
    public PurpleLaser() : base(10, "purpleLaser.png", GameObjects.playerBullet) 
    {
      DefaultRotation = -Math.PI / 2;
      Node.SetScale(0.125f / 2);
      Node.ZRotation += (nfloat)DefaultRotation;
    }
  }
}
