namespace SpaceGame
{
  public class HealBonus : Bonus
  {
    public HealBonus(GameController controller) 
      : base ("heal2.png", controller) 
      {
      _node.SetScale(0.25f / 2);
    }


    public override void Get()
    {
      controller.Player.GetHeal(10);
    }
  }
}
