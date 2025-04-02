using System;
using System.IO;
using UnityEngine;

namespace Data.DataSaving
{
    public class FileDataHandler
    {
        #region Private Fields

        private string dataDirPath = "";

        private string dataFileName = "";

        #endregion

        #region Class Implementation

        public FileDataHandler(string dataDirPath, string dataFileName)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
        }

        public SavedGameData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);

            SavedGameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    loadedData = JsonUtility.FromJson<SavedGameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error during loading file");
                }
            }

            return loadedData;
        }

        public void Save(SavedGameData _savedGameData)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonUtility.ToJson(_savedGameData, true);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch
            {
                Debug.LogError("Error during saving to data file");
            }
        }

        #endregion
    }
}