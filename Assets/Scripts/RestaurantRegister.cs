using System.Collections.Generic;
using UnityEngine;

public class RestaurantRegister : MonoBehaviour
{
    public List<GameObject> seats = new List<GameObject>();
    public List<GameObject> bathrooms = new List<GameObject>();
    public List<GameObject> clients = new List<GameObject>();
    public int currentClientId = 0;

    private GameObject nextAvailableSeat;

    private void Awake()
    {
        GetNextAvailableSeat();
    }

    public void AddClient(GameObject client)
    {
        clients.Add(client);
    }

    public GameObject GetNextAvailableSeat()
    {
        foreach (GameObject seat in seats)
        {
            SetOnTrigger set = seat.GetComponent<SetOnTrigger>();
            if (set != null && set.available)
            {
                nextAvailableSeat = seat;
                return nextAvailableSeat;
            }
        }
        nextAvailableSeat = null;
        return null;
    }

    public void SetSeatAvailable(GameObject seat, bool available)
    {
        SetOnTrigger set = seat.GetComponent<SetOnTrigger>();
        if (set != null)
        {
            set.available = available;
        }
    }

    public int GetNextClientId()
    {
        return currentClientId++;
    }
}
