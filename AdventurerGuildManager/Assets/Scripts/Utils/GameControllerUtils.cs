using System.Linq;
using Runtime.GameControllers;

namespace Utils
{
    public static class GameControllerUtils
    {

        #region Class Implementation

        public static T GetGameController<T>(ref T reference) where T : IController
        {
            if (reference == null)
            {
                reference = MainController.Instance.game_controllers.OfType<T>().FirstOrDefault();
            }
            return reference;
        }

        #endregion
        
    }
}