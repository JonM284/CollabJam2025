using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;

namespace Runtime.GameControllers
{
    public class MainController: GameControllerBase
    {

        #region Static

        public static MainController Instance { get; private set; }

        #endregion
        
        #region Public Fields

        public List<GameControllerBase> game_controllers = new List<GameControllerBase>();

        #endregion

        #region Accessors

        public bool allInitialized { get; private set; }

        #endregion

        #region Unity Events

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            CleanupControllers();
        }

        private void OnApplicationQuit()
        {
            CleanupControllers();
        }

        #endregion

        #region Class Implementation

        [ContextMenu("Initialize")]
        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            StartCoroutine(C_SetupControllers());
            game_controllers.ForEach(gc => gc.Initialize());
            base.Initialize();
            DontDestroyOnLoad(this.gameObject);
        }

        public IEnumerator C_SetupControllers()
        {
            yield return new WaitUntil(() => game_controllers.TrueForAll(gc => gc.is_Initialized));
            allInitialized = true;
        }

        public void CleanupControllers()
        {
            game_controllers.ForEach(gc => gc.Cleanup());
        }


        #endregion

    }
}