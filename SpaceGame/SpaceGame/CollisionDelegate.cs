
using SpriteKit;

namespace SpaceGame
{
  public class CollisionDelegate : SKPhysicsContactDelegate
  {
    public delegate void CollisionCallback(SKPhysicsContact contact);
    public CollisionCallback Callbacks { get; set; }
    public override void DidBeginContact(SKPhysicsContact contact) => Callbacks(contact);
  }
}
