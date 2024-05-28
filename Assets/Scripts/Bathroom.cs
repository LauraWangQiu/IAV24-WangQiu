using UnityEngine;

public class Bathroom : MonoBehaviour
{
    [SerializeField] private string tagName = "Client";
    public bool isAvailable = true;
    public GameObject assigned = null;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            isAvailable = true;
            assigned = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            isAvailable = false;
            assigned = other.gameObject;
        }
    }
}
