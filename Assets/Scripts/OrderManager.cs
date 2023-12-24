using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Recipe;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private int minimumSpawnTime = 2;
    [SerializeField] private int maximumSpawnTime = 10;
    [SerializeField] private int maximumOrdersAtOnce = 5;
    
    public event Action<RecipeSO> OrderReceived;
    public event Action<RecipeSO> OrderCompleted;
    public static OrderManager Instance { get; private set; }
    
    private readonly List<RecipeSO> _orders = new ();
    private List<RecipeSO> _recipes;

    public bool TryFulfillOrder(RecipeSO recipe)
    {
        var order = _orders.FirstOrDefault(order => 
            order.ingredients.Count == recipe.ingredients.Count && 
            order.ingredients.All(recipe.ingredients.Contains));
        
        if (order == null) return false;
        
        OrderCompleted?.Invoke(order);
        _orders.Remove(order);
        
        StopAllCoroutines();
        StartCoroutine(OrderGenerator());
        
        return true;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        
        _recipes = RecipeManager.Instance.Recipes;
    }

    private void Start()
    {
        StartCoroutine(OrderGenerator());
    }

    private IEnumerator OrderGenerator()
    {
        while (_orders.Count < maximumOrdersAtOnce)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minimumSpawnTime, maximumSpawnTime));
            GenerateOrder();
        }
    }

    private void GenerateOrder()
    {
        var order = _recipes[UnityEngine.Random.Range(0, _recipes.Count)];
        
        _orders.Insert(0, order);
        OrderReceived?.Invoke(order);
    }
}