using System;
namespace SpaceGame
{
  public class DoubleLaserEnemyWeapon : BaseEnemyWeapon
  {
    public DoubleLaserEnemyWeapon(GameUnit owner) : base(owner) { }
    public override void ShootOnce()
    {
      CreateBullet(5);
      CreateBullet(-5);
    }
  }
}
