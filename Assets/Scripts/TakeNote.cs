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
        if (myRegister != null && other.CompareTag(tagName))
        {
            if (!foodSet)
            {
                // Toma nota de la comida que el jugador ha recogido
                if (!myRegister.wishOnWait)
                {
                    Register register = other.GetComponent<Register>();
                    if (register != null && myRegister.wish != null &&
                        (myRegister.wish.tag == "Burger" ||
                         myRegister.wish.tag == "Cupcake" ||
                         myRegister.wish.tag == "Doughnut"))
                    {
                        register.petitions.Add(myRegister.wish);
                    }
                    myRegister.wishOnWait = true;
                }
                else
                {
                    Transform catchPosition = other.transform.Find("CatchPosition");
                    if (myRegister.wish != null && catchPosition != null)
                    {
                        foreach (Transform child in catchPosition)
                        {
                            if (child.CompareTag(myRegister.wish.tag))
                            {
                                myRegister.WishAccomplished();
                                StartCoroutine(myRegister.SelectBehavior(Random.Range(2.0f, 5.0f)));
                                Destroy(child.gameObject);
                                foodSet = true;
                                break;
                            }
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
