using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    // Waiter & Clients
    public BehaviorExecutor activeExecutor;
    public enum State { IDLE, MOVING, SIT };
    public State currentState;

    // Waiter
    public List<GameObject> petitions = new List<GameObject>();

    // Clients
    public GameObject seat;
    private Vector3 exitPosition;

    [SerializeField] private Wishes wishes;
    public GameObject wish;
    public Sprite wishSprite;
    public bool wishAccomplished = true;
    public float owingMoney = 0;

    private void Start()
    {
        if (activeExecutor == null)
        {
            Debug.LogWarning("BehaviorExecutor not found in " + gameObject.name);
        }
        currentState = State.MOVING;
    }

    private void Update()
    {
        if (wishes != null)
        {
            wishSprite = wishes.GetWishSprite(wish);
        }
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public Vector3 GetExitPosition()
    {
        return exitPosition;
    }

    public void SetExitPosition(Vector3 position)
    {
        exitPosition = position;
    }

    public void WishAccomplished()
    {
        if (wishes != null && wish != null)
        {
            owingMoney += wishes.GetWishCost(wish);
        }
        WishServed();
        wishAccomplished = true;
    }

    public void WishServed()
    {
        wishSprite = null;
        wish = null;
    }
}
