using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace RMC.Optimizations.Shared
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        [Header("Basics")]
        [SerializeField]
        private Spawner _spawner;

        [SerializeField]
        private int _columns = 5;

        [SerializeField]
        private int _rows = 5;

        [SerializeField]
        private int _depth = 1;

        [SerializeField]
        private Vector3 _gap = new Vector3(0.01f, 0.01f, 0.01f);

        [Header("Settings")]
        [SerializeField]
        private GameObject _prefabA;

        [SerializeField]
        private GameObject _prefabB;

        [SerializeField]
        private bool _isUsingPrefabA = true;

        private List<GameObject> _instances = new List<GameObject>();
        private bool _isDestroying = false;
        
        //  Unity Methods ---------------------------------
        protected void Start()
        {
            //_spawner.GetComponent<Renderer>().enabled = false;
            Spawn();
        }

        //  Methods ---------------------------------------
        public async void Spawn()
        
        {
            
            GameObject prefabToUse = _prefabB;
            if (_isUsingPrefabA )
            {
                prefabToUse = _prefabA;
            }
            
            if (prefabToUse == null)
            {
                Debug.LogError("Prefab is not assigned.");
                return;
            }

            Vector3 prefabSize = prefabToUse.GetComponentInChildren<Collider>().bounds.size;
            float totalWidth = _columns * (prefabSize.x + _gap.x) - _gap.x;
            float startX = transform.position.x - totalWidth / 2;

            int spawnPerFrame = 100;
            for (int x = 0; x < _columns; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    for (int z = 0; z < _depth; z++)
                    {
                        Vector3 position = new Vector3(
                            startX + x * (prefabSize.x + _gap.x),
                            y * (prefabSize.y + _gap.y),
                            z * (prefabSize.z + _gap.z)
                        );
                        
                        if (_isDestroying)
                        {
                            return;
                        }

                        GameObject instance = Instantiate(prefabToUse, position, Quaternion.identity, transform);
                        instance.GetComponentInChildren<Collider>().enabled = false;
                        instance.GetComponent<Rigidbody>().useGravity = false;
                        _instances.Add(instance);

                        if (_instances.Count % spawnPerFrame == 0)
                        {
                            // Wait for the next frame
                            await Task.Yield(); 
                        }
                    }
                }
            }

            // Enable colliders after all prefabs are instantiated
            foreach (var instance in _instances)
            {
                instance.GetComponentInChildren<Collider>().enabled = true;
                instance.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        protected void OnDestroy()
        {
            _isDestroying = true;
        }

        //  Event Handlers --------------------------------
    }
}