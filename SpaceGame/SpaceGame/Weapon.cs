using SpriteKit;

namespace SpaceGame
{
  public abstract class Weapon : IWeapon
  {
    protected GameUnit owner;


    protected Weapon(GameUnit weaponOwner) { owner = weaponOwner; }


    protected abstract SKAction CreateShootAction();
    public abstract void ShootOnce();
  }
}
