using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public List<Petition> toCook = new List<Petition>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Register playerRegister = other.GetComponent<Register>();
            if (playerRegister != null)
            {
                foreach (Petition item in playerRegister.petitions)
                {
                    AddToCookList(item);
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
            }
            else
            {
                Debug.LogError("Spawn point not found");
            }
            toCook.Remove(item);
        }
    }
}
