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

            RegisterObject registerObject = GetComponent<RegisterObject>();
            if (registerObject != null)
            {
                registerObject.caught = true;
                Transform catchPosition = other.gameObject.transform.Find("CatchPosition");
                if (catchPosition != null)
                {
                    transform.SetPositionAndRotation(catchPosition.position, catchPosition.rotation);
                    transform.SetParent(catchPosition, true);
                }
                else
                {
                    Debug.LogError("CatchPosition not found in the client object");
                }
            }
            else
            {
                Debug.LogError("RegisterObject component not found");
            }
        }
    }
}
