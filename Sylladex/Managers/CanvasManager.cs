using Sylladex.UI;

namespace Sylladex.Managers
{
    /// <summary>
    /// The CanvasManager class manages the UI surfaces in the Sylladex application.
    /// It inherits from the ObjectManager class and provides methods to update and draw the canvases.
    /// </summary>
    public class CanvasManager : ObjectManager<string, Canvas>
    {
        /// <summary>
        /// Updates all the visible canvases.
        /// </summary>
        public void Update()
        {
            foreach (var canvas in _objects.Values)
            {
                if (canvas.IsVisible)
                {
                    canvas.Update();
                }
            }
        }

        /// <summary>
        /// Draws all the visible canvases.
        /// </summary>
        public void Draw()
        {
            foreach (var canvas in _objects.Values)
            {
                if (canvas.IsVisible)
                {
                    canvas.Draw();
                }
            }
        }
    }
}
