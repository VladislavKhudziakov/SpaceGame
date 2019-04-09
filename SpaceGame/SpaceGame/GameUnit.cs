using SpriteKit;
using CoreGraphics;
using System.Collections.Generic;
namespace SpaceGame
{
  public class GameUnit : GameObject
  {
    protected double hp;
    protected double shields;
    //protected GameController _controller;


    public CGPoint LookDirection { get; set; }
    public GameController Controller { get; }
    public IWeapon Weapon { get; set; }


    public GameUnit(GameController controller, string spriteImgName, GameObjects type) 
      : base(spriteImgName)
    {
      Controller = controller;
      _node.PhysicsBody = SKPhysicsBody.CreateRectangularBody(_node.Size);
      _node.PhysicsBody.CategoryBitMask = (uint)type;

      if (type == GameObjects.player)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.enemy;
      }
      else if (type == GameObjects.enemy)
      {
        _node.PhysicsBody.ContactTestBitMask = (uint)GameObjects.player;
      }

      _node.PhysicsBody.CollisionBitMask = (uint)GameObjects.none;
    }
    

    public virtual void GetDamage(double incomeDmg, List<GameUnit> gameObjects)
    {
      if (shields - incomeDmg >= 0)
      {
        shields -= incomeDmg;
      }
      else
      {
        double dmg = incomeDmg - shields;
        hp -= dmg;

        if (hp <= 0)
        {
          Destroy();
        }
      }
    }


    protected virtual void Destroy() 
    {
      Node.RemoveFromParent();
      Controller.SceneGameUnits.Remove(this);
    }


    public virtual void GetShields(double incomeArmor)
    {
      shields += incomeArmor;
    }


    public virtual void GetHeal(double incomeHeal)
    {
      hp += incomeHeal;
    }
  }
}