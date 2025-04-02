using System.Linq;
using UnityEngine;
using Data.DataSaving;

namespace Runtime.Utils
{
    public static class WeightUtils
    {
        
        #region Class Implementation

        public static T GetValueByWeight<T>(SerializableDictionary<int,T> _dictionary)
        {
            //default value
            var endValue = _dictionary.FirstOrDefault().Value;

            //get random weight value from room weight
            var totalWeight = 0;
            foreach (var kvPair in _dictionary)
            {
                totalWeight += kvPair.Key;
            }
            
            //+1 because max value is exclusive
            var randomValue = Random.Range(1, totalWeight + 1);

            var currentWeight = 0;
            foreach (var kvPair in _dictionary)
            {
                currentWeight += kvPair.Key;
                if (randomValue <= currentWeight)
                {
                    endValue = kvPair.Value;
                    break;
                }
            }

            return endValue;
        }

        #endregion
        
    }
}