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
                register.SetState(Register.State.SIT);
                if (assignedObject == null)
                {
                    register.seat = gameObject;
                    register.SetExit(exit);
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

    //private void SelectBehavior()
    //{
    //    Register register = currentSitObject.GetComponent<Register>();
    //    if (!available && currentSitObject != null && register.wishAccomplished)
    //    {
    //        Transform catchPosition = currentSitObject.transform.Find("CatchPosition");
    //        if (catchPosition != null)
    //        {
    //            foreach (Transform child in catchPosition)
    //            {
    //                Destroy(child.gameObject);
    //            }
    //        }

    //        Debug.Log("A random behavior has been selected");
    //        WishManager wishManager = currentSitObject.GetComponent<WishManager>();
    //        if (wishManager != null)
    //        {
    //            wishManager.SelectRandomBehavior();
    //        }
    //    }
    //}

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

    //public void ActivateBehavior()
    //{
    //    Register register = currentSitObject.GetComponent<Register>();
    //    if (register != null && register.activeExecutor != null)
    //    {
    //        register.activeExecutor.enabled = true;
    //    }
    //    NavMeshAgent agent = currentSitObject.GetComponent<NavMeshAgent>();
    //    if (agent != null)
    //    {
    //        agent.enabled = true;
    //        agent.isStopped = true;
    //    }
    //}
}
