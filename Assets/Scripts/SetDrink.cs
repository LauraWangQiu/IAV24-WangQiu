using UnityEngine;

public class SetDrink : MonoBehaviour
{
    BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Register register = other.GetComponent<Register>();
        if (register != null && register.wish == gameObject)
        {
            Debug.Log("Drink set");
            if (col != null)
            {
                col.enabled = false;
            }

            Transform catchPosition = other.gameObject.transform.Find("CatchPosition");
            transform.SetPositionAndRotation(catchPosition.position, catchPosition.rotation);
        }
    }
}
