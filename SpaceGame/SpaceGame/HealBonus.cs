namespace SpaceGame
{
  public class HealBonus : Bonus
  {
    public HealBonus(GameController controller) 
      : base ("heal.png", controller) { }


    public override void Get()
    {
      controller.Player.GetHeal(10);
    }
  }
}
