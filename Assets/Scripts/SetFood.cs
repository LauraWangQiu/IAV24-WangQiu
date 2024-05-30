using UnityEngine;

public class SetFood : MonoBehaviour
{
    [SerializeField] private string tagName = "Player";
    private BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
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
                    Debug.LogError("CatchPosition not found in the player object");
                }
            }
            else
            {
                Debug.LogError("RegisterObject component not found");
            }
        }
    }
}
