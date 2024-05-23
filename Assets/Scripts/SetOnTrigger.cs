using UnityEngine;
using UnityEngine.AI;

public class SetOnTrigger : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private GameObject set;
    [SerializeField] private GameObject exit;
    public bool available = true;

    private GameObject currentSitObject;

    void Start()
    {
        if (set == null)
        {
            Debug.LogWarning("Set not found");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentSitObject == null)
            {
                return;
            }

            currentSitObject.transform.SetPositionAndRotation(exit.transform.position, exit.transform.rotation);
            Rigidbody rb = currentSitObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            Register register = currentSitObject.GetComponent<Register>();
            if (register != null && register.activeExecutor != null)
            {
                register.activeExecutor.enabled = true;
            }
            NavMeshAgent agent = currentSitObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = true;
            }
            available = true;
            currentSitObject = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (set != null && other.CompareTag(tagName))
        {
            other.transform.SetPositionAndRotation(set.transform.position, set.transform.rotation);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            Register register = other.GetComponent<Register>();
            if (register != null && register.activeExecutor != null)
            {
                register.activeExecutor.enabled = false;
            }
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            available = false;
            currentSitObject = other.gameObject;
        }
    }
}
