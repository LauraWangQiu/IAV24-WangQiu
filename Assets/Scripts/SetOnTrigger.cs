using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SetOnTrigger : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private GameObject set;
    [SerializeField] private GameObject exit;
    [SerializeField] private float randomMin = 2.0f;
    [SerializeField] private float randomMax = 5.0f;
    private GameObject currentSitObject;
    private GameObject assignedObject;
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
        if (set != null && other.CompareTag(tagName) && (assignedObject == null || assignedObject == other.gameObject))
        {
            Register register = other.GetComponent<Register>();
            // Si ya tiene silla asignada
            if (register != null && register.seat != null && assignedObject != other.gameObject)
            {
                return;
            }

            other.transform.SetPositionAndRotation(set.transform.position, set.transform.rotation);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            if (register != null && register.activeExecutor != null)
            {
                register.activeExecutor.enabled = false;
                register.SetState(Register.State.SIT);
                if (assignedObject == null)
                {
                    register.seat = gameObject;
                    register.SetExit(exit);
                }
                if ((register.wish != null && 
                    (register.wish.tag == "Soda" 
                    || register.wish.tag == "Coke" 
                    || register.wish.tag == "Lemonade")) 
                    || (register.bathroom != null))
                {
                    register.WishAccomplished();
                }
            }
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            available = false;
            currentSitObject = other.gameObject;
            assignedObject = other.gameObject;
            StartCoroutine(SelectBehavior(Random.Range(randomMin, randomMax)));
        }
    }

    public void StandUp()
    {
        currentSitObject = null;
        available = true;

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = false;
            StartCoroutine(ReactivateColliderAfterDelay(2.0f));
        }
    }

    IEnumerator SelectBehavior(float time)
    {
        yield return new WaitForSeconds(time);
        Register register = currentSitObject.GetComponent<Register>();
        if (register != null)
        {
            register.SelectBehavior();
        }
    }

    IEnumerator ReactivateColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = true;
        }
    }
}
