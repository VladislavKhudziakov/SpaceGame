using System;
namespace SpaceGame
{
  public class TrippleLaserEnemyWeapon : BaseEnemyWeapon
  {
    public TrippleLaserEnemyWeapon(GameUnit owner) : base(owner) { }
    public override void ShootOnce()
    {
      CreateBullet(10);
      CreateBullet(0);
      CreateBullet(-10);
    }
  }
}
