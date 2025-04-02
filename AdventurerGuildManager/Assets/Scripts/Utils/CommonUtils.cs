using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Utils
{
    public static class CommonUtils
    {
        private static Random rng;

        #region Class Implementation

        public static T GetRequiredComponent<T>(ref T reference, Func<T> func)
        {
            if (reference.IsNull() && !func.IsNull())
            {
                reference = func();
            }

            return reference;
        }

        public static List<T> SelectNotNull<T>(this List<T> checkList)
        {
            return checkList.Where(c => c != null).ToNewList();
        }
        
        public static bool IsNull(this object obj) {
            var isObjectNull = obj == null || obj.Equals(null);
            if (isObjectNull) {
                return true;
            }

            if (obj is GameObject gameObject) {
                return gameObject == null || gameObject.Equals(null) || gameObject.name.Equals("null");
            }

            if (obj is Component component) {
                return component.gameObject == null || component.gameObject.Equals(null) 
                                                    || component.gameObject.name.Equals("null");
            }
            
            return false;
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            if (rng.IsNull())
            {
                rng = new Random();
            }

            int _count = list.Count;
            while (_count > 1)
            {
                _count--;
                int _index = rng.Next(_count + 1);
                (list[_index], list[_count]) = (list[_count], list[_index]);
            }

            return list;
        }

        public static List<T> ToNewList<T>(this IEnumerable<T> list)
        {
            var newList = new List<T>();
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is T item)) {
                    continue;
                }
                newList.Add(item);
            }

            return newList;
        }

        public static bool IsApproximately(float _a, float _b, float _threshold = 0.1f)
        {
            return Mathf.Abs(_a - _b) < _threshold;
        }

        public static float GetDistanceFromSpeedAndTime(float _speed, float _time)
        {
            return _speed * _time;
        }

        public static float GetSpeedFromDistanceAndTime(float _distance, float _time)
        {
            return _distance / _time;
        }

        public static float GetTimeFromDistanceAndSpeed(float _distance, float _speed)
        {
            return _distance / _speed;
        }

        #endregion


    }
}