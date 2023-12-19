using System;
using UnityEngine;

namespace Product
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private ProductSO productSO;
        
        public ProductSO ProductSO => productSO;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetOrigin(Transform origin)
        {
            _transform.SetParent(origin);
            _transform.localPosition = Vector3.zero;
            _transform.localRotation = Quaternion.identity;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}