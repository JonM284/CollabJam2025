using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Scripts.Utils;
using UnityEngine;

namespace Runtime.GameControllers
{
    public class ObjectPoolController: GameControllerBase
    {
        
        #region Instance

        public static ObjectPoolController Instance { get; private set; }

        #endregion

        #region Nested Classes

        [Serializable]
        public class ObjectPool
        {
            public string poolName;
            public GameObject clonedObject;
            public List<GameObject> pooledObjects = new List<GameObject>();
            
            public ObjectPool(string _newPoolName, GameObject _newObject, GameObject _firstObject)
            {
                poolName = _newPoolName;
                clonedObject = _newObject;
                pooledObjects.Add(_firstObject);
            }

            public void ForceCreateNewItem(Transform _parent)
            {
                Instantiate(clonedObject, _parent);
            }

            public GameObject GetItem()
            {
                if (pooledObjects.Count == 0)
                {
                    return Instantiate(clonedObject);
                }

                var _item = pooledObjects[0];
                pooledObjects.Remove(_item);
                return _item;
            }

            public void ReturnItem(GameObject _returnedItem)
            {
                pooledObjects.Add(_returnedItem);
            }
        }

        #endregion

        #region Private Fields

        private List<ObjectPool> m_objectPools = new List<ObjectPool>();

        private Transform m_cachedObjectPoolParent;
        
        #endregion

        #region Accessors

        public Transform cachedObjectPoolParent =>
            CommonUtils.GetRequiredComponent(ref m_cachedObjectPoolParent, 
                ()=> TransformUtils.CreatePool(this.transform, false));

        #endregion

        #region GameControllerBase Inherited Methods

        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            base.Initialize();
        }

        #endregion

        #region Class Implementation

        public void ReturnToPool(string _poolName, GameObject _returnedObject)
        {
            _returnedObject.transform.parent = m_cachedObjectPoolParent;
            GetPool(_poolName).ReturnItem(_returnedObject);
        }

        public async UniTask T_PreCreateObject(string _poolName, GameObject _reference)
        {
            if (m_objectPools.Count == 0 || !ContainsAddressablePool(_poolName))
            {
                await T_CreateNewPool(_poolName, _reference);
            }

            GetPool(_poolName).ForceCreateNewItem(m_cachedObjectPoolParent);
        }
        
        public async UniTask<GameObject> T_CreateObject(string _poolName, GameObject _reference, Vector3 _position)
        {
            GameObject _returnedObject = null;
            if (m_objectPools.Count == 0 || !ContainsAddressablePool(_poolName))
            {
                var _newPool = await T_CreateNewPool(_poolName, _reference);
                _returnedObject = _newPool.GetItem();
                _returnedObject.transform.position = _position;
                _returnedObject.transform.parent = null;
                return _returnedObject;
            }

            _returnedObject = GetPool(_poolName).GetItem();
            _returnedObject.transform.position = _position;
            _returnedObject.transform.parent = null;
            return _returnedObject;
        }

        public async UniTask<GameObject> T_CreateParentedObject(string _poolName, GameObject _reference,
            Transform _parent)
        {
            GameObject _returnedObject = null;
            if (m_objectPools.Count == 0 || !ContainsAddressablePool(_poolName))
            {
                var _newPool = await T_CreateNewPool(_poolName, _reference);
                _returnedObject = _newPool.GetItem();
                _returnedObject.transform.parent = _parent;
                return _returnedObject;
            }

            _returnedObject = GetPool(_poolName).GetItem();
            _returnedObject.transform.parent = _parent;
            return _returnedObject;
        }
        
        private async UniTask<ObjectPool> T_CreateNewPool(string _poolName, GameObject _reference)
        {
            var _clonedObject = Instantiate(_reference, cachedObjectPoolParent);
            
            var _firstObject = Instantiate(_reference, cachedObjectPoolParent);

            var _newPool = new ObjectPool(_poolName ,_clonedObject, _firstObject);
            
            m_objectPools.Add(_newPool);

            return _newPool;
        }

        private ObjectPool GetPool(string _poolName)
        {
            return m_objectPools.FirstOrDefault(pba => pba.poolName == _poolName);
        }
        
        private bool ContainsAddressablePool(string _poolName)
        {
            return !m_objectPools.FirstOrDefault(pba => pba.poolName == _poolName).IsNull();
        }

        private void DeleteAllPools()
        {
            if (m_objectPools.Count == 0)
            {
                return;
            }
            
            foreach (var _pool in m_objectPools)
            {
                Destroy(_pool.clonedObject);

                if (_pool.pooledObjects.Count == 0)
                {
                    continue;
                }

                foreach (var _pooledObject in _pool.pooledObjects)
                {
                    Destroy(_pooledObject);
                }
            }
            
            m_objectPools.Clear();
        }

        #endregion
        
    }
}