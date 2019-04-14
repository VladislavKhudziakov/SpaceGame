using System;
namespace SpaceGame
{
  public class DoubleEnemyLaserWeapon : BaseLaserWeapon
  {
    public DoubleEnemyLaserWeapon()
    {
    }

    public override void ShootOnce()
    {
      //CreateBullet(10);
      CreateBullet(0);
      //CreateBullet(-10);
    }
  }
}
