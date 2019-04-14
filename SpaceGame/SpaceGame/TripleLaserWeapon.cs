using System;
namespace SpaceGame
{
  public class TripleLaserWeapon : BaseLaserWeapon
  {
    public TripleLaserWeapon(GameUnit weaponOwner) : base(weaponOwner) { }

    public override void ShootOnce()
    {
      CreateBullet(15);
      CreateBullet(0);
      CreateBullet(-15);
    }
  }
}
