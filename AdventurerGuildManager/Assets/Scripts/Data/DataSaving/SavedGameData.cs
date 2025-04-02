using System.Collections.Generic;
using UnityEngine;

namespace Data.DataSaving
{
    
    [System.Serializable]
    public class SavedGameData
    {
        
        public int savedUpgradePoints;

        public int savedMapSelectionLevel;

        public string m_currentEventIdetifier;

        public int currentTournamentIndex;

        public int currentMatchIndex;

        public Vector3 m_lastPressedPOIpoisiton;
        
        public SavedGameData()
        {
            this.savedUpgradePoints = 0;
            this.savedMapSelectionLevel = 0;
            this.currentTournamentIndex = 0;
            this.currentMatchIndex = 0;
            this.m_currentEventIdetifier = "";
            this.m_lastPressedPOIpoisiton = Vector3.zero;
        }
    }
}