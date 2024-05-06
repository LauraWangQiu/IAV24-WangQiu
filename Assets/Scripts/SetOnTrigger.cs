using UnityEngine;

public class SetOnTrigger : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private string setName;
    [SerializeField] private GameObject set;
    public bool available = true;

    void Start()
    {
        if (set == null)
        {
            Debug.LogWarning("Set not found");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (set != null && other.CompareTag(tagName))
        {
            Debug.Log("Setting global position and rotation");
            other.transform.SetPositionAndRotation(set.transform.position, set.transform.rotation);

            available = false;
        }
    }
}
