namespace SpaceGame
{
  public class DoubleLaserWeapon : BaseLaserWeapon
  {
    public DoubleLaserWeapon(GameUnit weaponOwner) : base(weaponOwner) { }

    public override void ShootOnce()
    {
      CreateBullet(15);
      CreateBullet(-15);
    }
  }
}
