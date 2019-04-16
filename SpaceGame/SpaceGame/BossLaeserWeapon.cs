using System;
namespace SpaceGame
{
  public class BossLaserWeapon : BaseEnemyWeapon
  {
    public BossLaserWeapon(GameUnit owner) : base(owner) { }

    public override void ShootOnce()
    {
      CreateBullet(35);
      CreateBullet(30);
      CreateBullet(15);
      CreateBullet(10);
      CreateBullet(0);
      CreateBullet(-10);
      CreateBullet(-15);
      CreateBullet(-30);
      CreateBullet(-35);
    }
  }
}
