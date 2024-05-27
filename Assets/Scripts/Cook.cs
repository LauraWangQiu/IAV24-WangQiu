using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public List<GameObject> toCook = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Register playerRegister = other.GetComponent<Register>();
            if (playerRegister != null)
            {
                foreach (GameObject item in playerRegister.petitions)
                {
                    AddToCookList(item);
                }
                playerRegister.petitions.Clear();
            }
        }
    }

    public void AddToCookList(GameObject item)
    {
        if (!toCook.Contains(item))
        {
            Debug.Log("Adding to cook list");
            toCook.Add(item);
            StartCoroutine(CookItem(item));
        }
    }

    IEnumerator CookItem(GameObject item)
    {
        RegisterObject registerObject = item.GetComponent<RegisterObject>();
        if (registerObject != null)
        {
            yield return new WaitForSeconds(registerObject.timeToCook);
            if (spawnPoint != null)
            {
                Instantiate(item, spawnPoint.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Spawn point not found");
            }
            toCook.Remove(item);
        }
    }
}
