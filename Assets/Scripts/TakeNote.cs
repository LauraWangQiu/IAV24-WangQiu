using UnityEngine;

public class TakeNote : MonoBehaviour
{
    private Register myRegister;

    private void Start()
    {
        myRegister = GetComponentInParent<Register>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myRegister != null && other.CompareTag("Player"))
        {
            Register register = other.GetComponent<Register>();
            if (register != null && myRegister.wish != null &&
                (myRegister.wish.tag == "Burger" ||
                 myRegister.wish.tag == "Fries"  ||
                 myRegister.wish.tag == "Doughnut"))
            {
                register.petitions.Add(myRegister.wish);
                myRegister.WishServed();
            }
        }
    }
}
