namespace SpaceGame
{
  public class WeaponBonus : Bonus
  {
    public WeaponBonus(GameController controller)
      : base("laser.png", controller) 
      {
        _node.SetScale(0.125f/ 2);
      }


    public override void Get()
    {
      controller.Player.UpWeaponLvl();
    }
  }
}
