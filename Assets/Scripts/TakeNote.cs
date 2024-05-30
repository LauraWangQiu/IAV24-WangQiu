using UnityEngine;

public class TakeNote : MonoBehaviour
{
    [SerializeField] private string tagName = "Player";
    private Register myRegister;
    public bool foodSet = false;

    private void Start()
    {
        myRegister = GetComponentInParent<Register>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!foodSet && myRegister != null && other.CompareTag(tagName))
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
    }
}
