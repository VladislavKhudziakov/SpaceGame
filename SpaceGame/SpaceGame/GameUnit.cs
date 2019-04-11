using SpriteKit;
using CoreGraphics;
using System;
using System.Timers;

namespace SpaceGame
{
  public class GameUnit : GameObject
  {
    protected double hp;
    protected double shields;


    public GameController Controller { get; }
    public CGPoint LookDirection { get; set; }
    public IWeapon Weapon { get; set; }
    public double HP { get => hp; }
    public double Shields { get => shields; }


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


    public virtual void GetDamage(double incomeDmg)
    {
      AnimateGettingGamage();
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
      Node.Texture = SKTexture.FromImageNamed("explosion.png");

      var timer = new Timer(100);

      timer.Start();

      timer.Elapsed += (sender, e) => 
      { Node.RemoveFromParent();
        Controller.SceneGameUnits.Remove(this);
      };
    }


    public virtual void GetShields(double incomeArmor)
    {
      shields += incomeArmor;
    }


    protected virtual void AnimateGettingGamage()
    {
      var fadeAlphaOut = SKAction.FadeAlphaTo(0.5f, 0.25);
      var fadeAlphaIn = SKAction.FadeAlphaTo(1f, 0.25);
      var resultingAction = SKAction.Sequence(
        fadeAlphaOut, fadeAlphaIn, fadeAlphaOut, fadeAlphaIn);

      Node.RunAction(resultingAction);
    }


    public virtual void GetHeal(double incomeHeal)
    {
      hp += incomeHeal;
    }
  }
}