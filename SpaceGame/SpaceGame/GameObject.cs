using System;
using SpriteKit;
using CoreGraphics;

namespace SpaceGame
{
  public class GameObject : IGameObject
  {
    protected SKSpriteNode _node;
    protected SKTexture _spriteTexture;
   
    public SKSpriteNode Node { get => _node; set => _node = value; }
    public double DefaultRotation { get; set; }
    public double CurrentRotation { get => Node.ZRotation - DefaultRotation; }

    public GameObject(string spriteImgName)
    {
      _spriteTexture = SKTexture.FromImageNamed(spriteImgName);
      _node = SKSpriteNode.FromTexture(_spriteTexture);
      _node.ZPosition = 1;
    }


    public GameObject(CGPoint position, string spriteImgName)
    {
      _spriteTexture = SKTexture.FromImageNamed(spriteImgName);
      _node = SKSpriteNode.FromTexture(_spriteTexture);
      _node.ZPosition = 1;
      _node.Position = new CGPoint(position);
    }
  }
}
