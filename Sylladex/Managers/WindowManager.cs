using Sylladex.UI;

namespace Sylladex.Managers
{
    /// <summary>
    /// The WindowManager class manages the UI surfaces in the Sylladex application.
    /// It inherits from the ObjectManager class and provides methods to update and draw the windows.
    /// </summary>
    public class WindowManager : ObjectManager<string, Window>
    {
        /// <summary>
        /// Updates all the visible windows.
        /// </summary>
        public void Update()
        {
            foreach (var window in _objects.Values)
            {
                if (window.IsVisible)
                {
                    window.Update();
                }
            }
        }

        /// <summary>
        /// Draws all the visible windows.
        /// </summary>
        public void Draw()
        {
            foreach (var window in _objects.Values)
            {
                if (window.IsVisible)
                {
                    window.Draw();
                }
            }
        }
    }
}
