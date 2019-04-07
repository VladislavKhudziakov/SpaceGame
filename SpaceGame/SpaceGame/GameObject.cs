using System;
using SpriteKit;

namespace SpaceGame
{
  public class GameObject : IGameObject
  {
    protected SKSpriteNode _node;
    protected SKTexture _spriteTexture;

    public SKSpriteNode Node { get => _node; set => _node = value; }

    public GameObject(string spriteImgName)
    {
      _spriteTexture = SKTexture.FromImageNamed(spriteImgName);
      _node = SKSpriteNode.FromTexture(_spriteTexture);
      _node.ZPosition = 1;
    }
  }
}
