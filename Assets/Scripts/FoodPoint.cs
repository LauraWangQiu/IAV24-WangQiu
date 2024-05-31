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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burger") || other.CompareTag("Doughnut") || other.CompareTag("Cupcake"))
        {
            if (foodList.Contains(other.gameObject))
            {
                foodList.Remove(other.gameObject);
            }
        }
    }

    private void OnTrigger(Collider other)
    {
        if (other.CompareTag("Burger") || other.CompareTag("Doughnut") || other.CompareTag("Cupcake"))
        {
            if (!foodList.Contains(other.gameObject))
            {
                foodList.Add(other.gameObject);
            }
        }
        else if (other.CompareTag("Player"))
        {
            Register register = other.GetComponent<Register>();
            if (register != null)
            {
                foreach (Petition food in register.petitions)
                {
                    if (food != null)
                    {
                        Register clientRegister = food.client.GetComponent<Register>();
                        if (clientRegister != null)
                        {
                            clientRegister.WishAssisted();
                        }
                    }
                }
            }
            foodList.Clear();
        }
    }
}
