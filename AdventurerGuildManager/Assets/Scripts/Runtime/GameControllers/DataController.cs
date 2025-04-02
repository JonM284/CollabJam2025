using System.Collections.Generic;
using System.Linq;
using Data.DataSaving;
using Project.Scripts.Utils;
using UnityEngine;

//Data saving was made using the help of this youtube tutorial: https://www.youtube.com/watch?v=aUi9aijvpgs

namespace Runtime.GameControllers
{
    public class DataController: GameControllerBase
    {
        #region Read-Only

        private readonly string saveFileName = "GameData.game";

        #endregion
        
        #region Singleton

        public static DataController Instance { get; private set; }

        #endregion

        #region Private Fields

        private SavedGameData savedGameData;

        private List<ISaveableData> saveableDatas;

        private FileDataHandler m_dataHandler;

        #endregion

        #region Unity Events

        private void Awake()
        {
            if (!Instance.IsNull())
            {
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            this.m_dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
            this.saveableDatas = FindAllSaveableDataObjects();
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        #endregion

        #region Class Implementation

        public void NewGame()
        {
            this.savedGameData = new SavedGameData();
        }

        public void LoadGame()
        {
            this.savedGameData = m_dataHandler.Load();
            
            if (this.savedGameData == null)
            {
                NewGame();
            }

            foreach (var data in saveableDatas)
            {
                data.LoadData(savedGameData);
            }

        }

        public void SaveGame()
        {
            foreach (var data in saveableDatas)
            {
                data.SaveData(ref savedGameData);
            }
            
            m_dataHandler.Save(savedGameData);
        }

        private List<ISaveableData> FindAllSaveableDataObjects()
        {
            IEnumerable<ISaveableData> datas = FindObjectsOfType<MonoBehaviour>().OfType<ISaveableData>();

            return new List<ISaveableData>(datas);
        }

        #endregion



    }
}