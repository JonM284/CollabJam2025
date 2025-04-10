﻿using UnityEngine;

namespace Project.Scripts.Utils
{
    public static class GameObjectUtils
    {

        public static GameObject Clone(this GameObject objectToClone, Transform parent = null)
        {
            var clonedObject = GameObject.Instantiate(objectToClone, parent);
            return clonedObject;
        }

    }
}