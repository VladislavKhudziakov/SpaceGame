using SpriteKit;

namespace SpaceGame
{
  public class Enemy : GameUnit
  {
    public Enemy(SKScene scene, string imgName) 
      : base (scene, imgName, GameObjects.enemy)
    {
    }
  }
}
