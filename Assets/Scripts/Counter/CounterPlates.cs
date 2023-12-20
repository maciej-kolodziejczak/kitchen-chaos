using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Counter
{
    public class CounterPlates : CounterBase
    {
        [SerializeField] private int maxPlateCount;
        [SerializeField] private GameObject platePrefab;
        [SerializeField] private float plateSpawnInterval;
    
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
            var newPlate = Instantiate(platePrefab, Holder.HoldPoint);
            newPlate.transform.localPosition = Vector3.up * (PlateHeight * _plates.Count);
            
            _plates.Push(newPlate);
        }

        public override void Interact(IHolder invoker)
        {
            if (invoker.IsHolding) return;

            var plate = _plates.Pop();
        
            // temp
            Destroy(plate);
        
            StopAllCoroutines();
            StartCoroutine(SpawnPlates());
        }
    }
}