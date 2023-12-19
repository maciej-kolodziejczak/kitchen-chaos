using System.Collections;
using System.Collections.Generic;
using Product;
using UnityEngine;

namespace Counter
{
    [RequireComponent(typeof(ProductHandler))]
    public class PlatesCounter : BaseCounter
    {
        [SerializeField] private int maxPlates;
        [SerializeField] private float spawnInterval;
        [SerializeField] private GameObject plateVisualPrefab;
        
        private ProductHandler _productHandler;
        private readonly Stack<GameObject> _plates = new();
        private const float PlateHeight = 0.1f;

        private void Awake()
        {
            _productHandler = GetComponent<ProductHandler>();
            
            StartCoroutine(SpawnPlates());
        }

        private IEnumerator SpawnPlates()
        {
            while (_plates.Count < maxPlates)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnPlate();
            }
        }

        private void SpawnPlate()
        {
            var plateVisual = Instantiate(plateVisualPrefab, _productHandler.ProductOrigin);
            plateVisual.transform.localPosition = Vector3.up * (PlateHeight * _plates.Count);
            
            _plates.Push(plateVisual);
        }

        public override void Interact(ProductHandler invoker)
        {
            if (_plates.Count == 0) return;
            
            var plate = _plates.Pop();
            Destroy(plate);
            
            StopAllCoroutines();
            StartCoroutine(SpawnPlates());
        }
    }
}