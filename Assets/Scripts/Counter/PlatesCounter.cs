using System.Collections;
using Product;
using Unity.VisualScripting;
using UnityEngine;

namespace Counter
{
    public class PlatesCounter : BaseCounter
    {
        [SerializeField] private int maxPlates;
        [SerializeField] private float spawnInterval;
        
        private int _platesCount;

        private void Awake()
        {
            StartCoroutine(SpawnPlates());
        }

        private IEnumerator SpawnPlates()
        {
            while (_platesCount < maxPlates)
            {
                yield return new WaitForSeconds(spawnInterval);
                
                _platesCount++;
                Debug.Log("plate spawned");
            }
        }

        public override void Interact(ProductHandler invoker)
        {
            Debug.Log("plate taken");
            _platesCount--;
            
            StopAllCoroutines();
            StartCoroutine(SpawnPlates());
        }
    }
}