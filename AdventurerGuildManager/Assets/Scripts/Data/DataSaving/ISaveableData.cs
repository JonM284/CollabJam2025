namespace Data.DataSaving
{
    public interface ISaveableData
    {

        #region Interface Methods

        void LoadData(SavedGameData _savedGameData);

        void SaveData(ref SavedGameData _savedGameData);

        #endregion


    }
}