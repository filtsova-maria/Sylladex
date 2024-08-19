using Sylladex.Entities;
using Sylladex.Graphics;
using System.Diagnostics;

namespace Sylladex.Managers
{

    /// <summary>
    /// The EntityManager class is responsible for managing entities in the game.
    /// It provides methods for updating and drawing entities.
    /// </summary>
    public class EntityManager : ObjectManager<Entity>
    {
        /// <summary>
        /// Updates all the entities managed by the EntityManager.
        /// </summary>
        public void Update()
        {
            foreach (var entity in _objects.Values)
            {
                entity.Update();
                // Calculate the layer index based on the entity's vertical position so that the ones below are rendered on top and vice versa.
                entity.LayerIndex.SetIndex((int)(entity.BottomPosition / LayerIndex.PixelsPerLayer));
            }
        }

        /// <summary>
        /// Draws all the entities managed by the EntityManager.
        /// </summary>
        public void Draw()
        {
            foreach (var entity in _objects.Values)
            {
                entity.Draw();
            }
        }
    }
}
