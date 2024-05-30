using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishManager : MonoBehaviour
{

    [SerializeField] private Register register;

    [SerializeField] private List<BehaviorExecutor> behaviors;

    [SerializeField] private Image image;

    [SerializeField] private TakeNote takeNote;

    void Start()
    {
        if (register == null)
        {
            Debug.LogError("Register is not set");
        }
        if (takeNote == null)
        {
            Debug.LogError("TakeNote is not set");
        }
    }

    void Update()
    {
        if (register != null)
        {
            if (register.wishSprite != null)
            {
                image.sprite = register.wishSprite;
            }
            else
            {
                image.sprite = null;
            }
        }
    }

    public void SelectRandomBehavior()
    {
        if (behaviors.Count == 0)
        {
            return;
        }
        int selected = Random.Range(0, behaviors.Count);
        register.activeExecutor = behaviors[selected];
        Debug.Log("Selected Behavior: " + register.activeExecutor.behavior.brickName);
        register.wishAccomplished = false;

        if (register.seat != null)
        {
            SetOnTrigger seat = register.seat.GetComponent<SetOnTrigger>();
            if (seat != null)
            {
                if (register.activeExecutor.behavior.brickName == "Assets/Behaviors/OrderFood.asset")
                {
                    takeNote.foodSet = false;
                    register.ActivateBehavior();
                }
                // Se levanta si no es pedir comida
                else
                {
                    if (register.activeExecutor.behavior.brickName == "Assets/Behaviors/AskForBill.asset")
                    {
                        register.leave = true;
                    }
                    register.StandUp();
                }
            }
        }
    }
}
