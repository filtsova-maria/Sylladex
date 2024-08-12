using Sylladex.Entities;
using System;

namespace Sylladex.Managers
{
    public class CollisionManager: ObjectManager<Entity>
    {
        public static bool IsInRadius(Entity entity1, Entity entity2, float radius)
        {
            return Math.Pow(entity1.Position.X - entity2.Position.X, 2) + Math.Pow(entity1.Position.Y - entity2.Position.Y, 2) <= Math.Pow(radius, 2);
        }
    }
}
