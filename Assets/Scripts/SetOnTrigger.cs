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
                register.seat = gameObject;
                register.SetState(Register.State.SIT);
            }
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            available = false;
            currentSitObject = other.gameObject;
            assignedObject = other.gameObject;
            Invoke("SelectBehavior", Random.Range(randomMin, randomMax));
        }
    }

    public void StandUp()
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
            if (register.activeExecutor.behavior.brickName ==
                "Assets/BehaviorBricks/Samples/QuickStartGuide/" +
                "Done/Resources/Behaviors/DoneAbortableClickAndGo.asset")
            {
                int index = register.activeExecutor.blackboard.vector3ParamsNames.IndexOf("selectedPosition");
                if (index != -1)
                {
                    register.activeExecutor.blackboard.vector3Params[index] = exit.transform.position;
                }
            }

            register.SetState(Register.State.IDLE);
            register.SetExitPosition(exit.transform.position);
        }
        NavMeshAgent agent = currentSitObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = true;
        }
        currentSitObject = null;
        available = true;
    }

    private void SelectBehavior()
    {
        if (!available && currentSitObject != null)
        {
            Debug.Log("A random behavior has been selected");
            WishManager wishManager = currentSitObject.GetComponent<WishManager>();
            if (wishManager != null)
            {
                wishManager.SelectRandomBehavior();
                BoxCollider collider = GetComponent<BoxCollider>();
                if (collider != null)
                {
                    collider.enabled = false;
                    StartCoroutine(ReactivateColliderAfterDelay(2.0f));
                }
            }
        }
    }

    IEnumerator ReactivateColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            collider.enabled = true;
            Debug.Log("Collider reactivated");
        }
    }
}
