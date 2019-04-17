using System;
namespace SpaceGame
{
  public class ShieldsBonus : Bonus
  {
    public ShieldsBonus(GameController controller) 
      : base("shield2.png", controller) 
      {
      _node.SetScale(0.25f / 3);
    }


    public override void Get()
    {
      controller.Player.GetShields(10);
    }
  }
}
