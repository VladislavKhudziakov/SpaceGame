using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class GameObject : IGameObject
  {
    protected SKSpriteNode _node;
    protected SKTexture _spriteTexture;
    protected readonly Guid _id;


    public SKSpriteNode Node { get => _node; set => _node = value; }
    public double DefaultRotation { get; set; }
    public double CurrentRotation { get => Node.ZRotation - DefaultRotation; }
    public Guid ID { get => _id; }


    public GameObject(string spriteImgName)
    { 
      _id = Guid.NewGuid();
      _spriteTexture = SKTexture.FromImageNamed(spriteImgName);
      _node = SKSpriteNode.FromTexture(_spriteTexture);
      _node.Name = _id.ToString();
      _node.ZPosition = 1;
    }

  }
}
