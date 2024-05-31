using System.Collections.Generic;
using UnityEngine;

public class FoodPoint : MonoBehaviour
{
    public List<GameObject> foodList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTrigger(Collider other)
    {
        bool playerCleared = false;
        if (other.CompareTag("Player"))
        {
            foodList.Clear();
            playerCleared = true;
        }
        if (!playerCleared && other.CompareTag("Burger") || other.CompareTag("Doughnut") || other.CompareTag("Cupcake"))
        {
            if (!foodList.Contains(other.gameObject))
            {
                foodList.Add(other.gameObject);
            }
        }
        
    }
}
