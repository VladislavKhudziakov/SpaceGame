using System;
namespace SpaceGame
{
  public class ShieldsBonus : Bonus
  {
    public ShieldsBonus(GameController controller) 
      : base("shield.png", controller) { }


    public override void Get()
    {
      controller.Player.GetShields(10);
    }
  }
}
