using TMPro;
using UnityEngine;

public class TakeNote : MonoBehaviour
{
    [SerializeField] private string tagName = "Player";
    private TextMeshProUGUI money;
    private Register myRegister;
    public bool foodSet = false;

    private void Start()
    {
        myRegister = GetComponentInParent<Register>();

        GameObject moneyObj = GameObject.Find("Money");
        if (moneyObj != null)
        {
            money = moneyObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Money object not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTrigger(other);
    }

    private void OnTrigger(Collider other)
    {
        if (myRegister != null && other.CompareTag(tagName))
        {
            Register register = other.GetComponent<Register>();
            // Toma nota de la comida que el jugador ha recogido
            if (!foodSet && !myRegister.wishOnWait)
            {
                if (register != null && myRegister.wish != null &&
                    (myRegister.wish.tag == "Burger" ||
                     myRegister.wish.tag == "Cupcake" ||
                     myRegister.wish.tag == "Doughnut"))
                {
                    register.petitions.Add(new Petition(myRegister.wish, myRegister.GetComponent<ID>().id, myRegister.gameObject));
                }
                myRegister.wishOnWait = true;
            }
            else if (!foodSet)
            {
                Transform catchPosition = other.transform.Find("CatchPosition");
                if (catchPosition != null && myRegister.wish != null)
                {
                    foreach (Transform child in catchPosition)
                    {
                        if (child.CompareTag(myRegister.wish.tag) &&
                            child.GetComponent<ID>().id == myRegister.GetComponent<ID>().id)
                        {
                            myRegister.WishAccomplished();
                            register.toGive.Remove(myRegister.gameObject);
                            StartCoroutine(myRegister.SelectBehavior(Random.Range(2.0f, 5.0f)));
                            Destroy(child.gameObject);
                            foodSet = true;
                            break;
                        }
                    }
                }
            }

            // Si el cliente no ha pagado y quiere irse
            if (!myRegister.paid && myRegister.leave)
            {
                Register playerRegister = other.GetComponent<Register>();
                if (playerRegister != null)
                {
                    playerRegister.money += myRegister.owingMoney;
                    money.text = playerRegister.money.ToString();
                    myRegister.owingMoney = 0;
                    myRegister.leave = false;
                    myRegister.paid = true;
                    myRegister.wishOnWait = false;
                    myRegister.wishAccomplished = true;
                    myRegister.wish = null;
                    myRegister.wishSprite = null;
                }
            }
        }
    }
}
