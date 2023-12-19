using System;
using Player;
using Product;
using UnityEngine;

namespace Counter
{
    public class BaseCounter : MonoBehaviour
    {
        [SerializeField] private GameObject focusPrefab;
        
        public virtual void Interact(ProductHandler invoker)
        {}
        
        public virtual void Use()
        {}
        
        public void Focus()
        {
            focusPrefab.SetActive(true);
        }
        
        public void Blur()
        {
            focusPrefab.SetActive(false);
        }
    }
}