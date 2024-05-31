using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public List<Petition> toCook = new List<Petition>();

    [SerializeField] private Register playerRegister;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerRegister != null)
        {
            RestaurantRegister restaurantRegister = GameObject.FindObjectOfType<RestaurantRegister>();
            if (playerRegister != null && restaurantRegister != null)
            {
                foreach (Petition item in playerRegister.petitions)
                {
                    if (item != null)
                    {
                        AddToCookList(item);
                        List<GameObject> toRemove = new List<GameObject>();
                        if (restaurantRegister.orders.Count > 0)
                        {
                            foreach (GameObject food in restaurantRegister.orders)
                            {
                                if (food != null && restaurantRegister.orders.Contains(item.client))
                                {
                                    toRemove.Add(food);
                                }
                            }
                        }

                        foreach (GameObject removeItem in toRemove)
                        {
                            restaurantRegister.orders.Remove(removeItem);
                        }
                    }
                }
                playerRegister.petitions.Clear();
            }
        }
    }

    public void AddToCookList(Petition item)
    {
        Debug.Log("Adding to cook list");
        toCook.Add(item);
        StartCoroutine(CookItem(item));
    }

    IEnumerator CookItem(Petition item)
    {
        RegisterObject registerObject = item.obj.GetComponent<RegisterObject>();
        if (registerObject != null)
        {
            yield return new WaitForSeconds(registerObject.timeToCook);
            if (spawnPoint != null)
            {
                GameObject instantiated = Instantiate(item.obj, spawnPoint.transform.position, Quaternion.identity);
                instantiated.AddComponent<ID>().id = item.id;
                if (playerRegister != null)
                {
                    playerRegister.toGive.Add(item.client);
                }
            }
            else
            {
                Debug.LogError("Spawn point not found");
            }
            toCook.Remove(item);
        }
    }
}
