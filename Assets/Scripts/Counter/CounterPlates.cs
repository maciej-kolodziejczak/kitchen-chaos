using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Counter
{
    public class CounterPlates : CounterBase
    {
        [SerializeField] private int maxPlateCount;
        [SerializeField] private float plateSpawnInterval;
        [SerializeField] private GameObject platePrefab;
        [SerializeField] private GameObject plateVisualPrefab;
    
        private readonly Stack<GameObject> _plates = new();
        private const float PlateHeight = 0.1f;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(SpawnPlates());
        }

        private IEnumerator SpawnPlates()
        {
            while (_plates.Count < maxPlateCount)
            {
                yield return new WaitForSeconds(plateSpawnInterval);
                SpawnPlate();
            }
        }
    
        private void SpawnPlate()
        {
            var newPlate = Instantiate(plateVisualPrefab, Holder.HoldPoint);
            newPlate.transform.localPosition = Vector3.up * (PlateHeight * _plates.Count);
            
            _plates.Push(newPlate);
        }

        public override void Interact(IHolder invoker)
        {
            if (invoker.IsHolding) return;
            if (_plates.Count == 0) return;
            
            var plate = Instantiate(platePrefab, invoker.HoldPoint).GetComponent<Plate>();
            invoker.Attach(plate);
        
            Destroy(_plates.Pop());
        
            StopAllCoroutines();
            StartCoroutine(SpawnPlates());
        }
    }
}