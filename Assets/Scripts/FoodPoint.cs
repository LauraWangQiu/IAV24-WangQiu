using System.Collections.Generic;
using UnityEngine;

public class FoodPoint : MonoBehaviour
{
    public bool isThereFood = false;

    public List<GameObject> foodList = new List<GameObject>();

    private void Update()
    {
        foreach (GameObject food in foodList)
        {
            if (food == null)
            {
                foodList.Remove(food);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burger") || other.CompareTag("Doughnut") || other.CompareTag("Cupcake"))
        {
            isThereFood = true;
            if (!foodList.Contains(other.gameObject))
            {
                foodList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burger") || other.CompareTag("Doughnut") || other.CompareTag("Cupcake"))
        {
            isThereFood = false;
            if (foodList.Contains(other.gameObject))
            {
                foodList.Remove(other.gameObject);
            }
        }
    }
}
