using System.Collections.Generic;
using UnityEngine;

public class RestaurantRegister : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seats = new List<GameObject>();
    [SerializeField]
    private List<GameObject> clients = new List<GameObject>();

    private GameObject nextAvailableSeat;

    private void Awake()
    {
        GetNextAvailableSeat();
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
}
