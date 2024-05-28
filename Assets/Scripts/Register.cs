using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private GameObject exit;
    public GameObject bathroom;

    [SerializeField] private Wishes wishes;
    public GameObject wish;
    public Sprite wishSprite;
    public bool wishOnWait = false;
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

    public void SetExit(GameObject exitObj)
    {
        exit = exitObj;
    }

    public Vector3 GetExitPosition()
    {
        if (exit != null)
        {
            return exit.transform.position;
        }
        return Vector3.zero;
    }

    public void SelectBehavior()
    {
        if (wishAccomplished)
        {
            Transform catchPosition = transform.Find("CatchPosition");
            if (catchPosition != null)
            {
                foreach (Transform child in catchPosition)
                {
                    Destroy(child.gameObject);
                }
            }

            Debug.Log("A random behavior has been selected");
            WishManager wishManager = GetComponent<WishManager>();
            if (wishManager != null)
            {
                wishManager.SelectRandomBehavior();
            }
        }
    }

    public IEnumerator SelectBehavior(float time)
    {
        yield return new WaitForSeconds(time);
        SelectBehavior();
    }

    public void ActivateBehavior()
    {
        if (activeExecutor != null)
        {
            activeExecutor.enabled = true;
        }
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = true;
        }
    }

    public void StandUp()
    {
        transform.SetPositionAndRotation(exit.transform.position, exit.transform.rotation);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (activeExecutor != null)
        {
            SetState(Register.State.IDLE);
            ActivateBehavior();
        }

        if (seat != null)
        {
            SetOnTrigger setOnTrigger = seat.GetComponent<SetOnTrigger>();
            if (setOnTrigger != null)
            {
                setOnTrigger.StandUp();
            }
        }
    }

    public void WishAccomplished()
    {
        if (wishes != null && wish != null)
        {
            owingMoney += wishes.GetWishCost(wish);
        }
        WishServed();
        wishAccomplished = true;
        wishOnWait = false;
        bathroom = null;
    }

    public void WishServed()
    {
        wishSprite = null;
        wish = null;
    }
}
