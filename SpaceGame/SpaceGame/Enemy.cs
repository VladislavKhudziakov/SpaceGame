using SpriteKit;

namespace SpaceGame
{
  public abstract class Enemy : GameUnit, IEnemy
  {
    public SKAction MovingAction { get; set; }


    protected Enemy(GameController controller, string imgName) 
      : base (controller, imgName, GameObjects.enemy) { }

    
    public abstract void Spawn();
    protected abstract void Move();
  }
}
