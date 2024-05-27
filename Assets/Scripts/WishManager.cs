using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishManager : MonoBehaviour
{

    [SerializeField] private Register register;

    [SerializeField] private List<BehaviorExecutor> behaviors;

    [SerializeField] private Image image;

    void Start()
    {
        if (register == null)
        {
            Debug.LogError("Register is not set");
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
        register.wishAccomplished = false;

        if (register.seat != null)
        {
            SetOnTrigger seat = register.seat.GetComponent<SetOnTrigger>();
            if (seat != null)
            {
                if (register.activeExecutor.behavior.brickName == "Assets/Behaviors/OrderFood.asset" ||
                    register.activeExecutor.behavior.brickName == "Assets/Behaviors/AskForBill.asset")
                {
                    seat.ActivateBehavior();
                }
                // Se levanta si no es pedir comida o pedir la cuenta
                else
                {
                    seat.StandUp();
                }
            }
        }
    }
}
