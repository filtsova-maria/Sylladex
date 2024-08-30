using Sylladex.Entities;
using System;

namespace Sylladex.Managers
{
    /// <summary>
    /// Manages collision detection between entities.
    /// </summary>
    public class CollisionManager: ObjectManager<Entity>
    {
        /// <summary>
        /// Determines whether two given entities are in a given radius of each other.
        /// </summary>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool IsInRadius(Entity entity1, Entity entity2, float radius)
        {
            return Math.Pow(entity1.Position.X - entity2.Position.X, 2) + Math.Pow(entity1.Position.Y - entity2.Position.Y, 2) <= Math.Pow(radius, 2);
        }
    }
}
