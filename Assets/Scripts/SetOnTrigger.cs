using UnityEngine;
using UnityEngine.AI;

public class SetOnTrigger : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private GameObject set;
    [SerializeField] private GameObject exit;
    [SerializeField] private float randomMax = 1000.0f;
    private GameObject currentSitObject;
    public bool available = true;
    public bool assigned = false;

    void Start()
    {
        if (set == null)
        {
            Debug.LogWarning("Set not found");
        }
    }

    void Update()
    {
        if (!available && currentSitObject != null)
        {
            Invoke("SelectRandomBehavior", Random.Range(0, randomMax));
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
                register.seat = gameObject;
                register.SetState(Register.State.SIT);
                register.WishAccomplished();
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
        available = true;
        currentSitObject = null;
    }

    private void SelectRandomBehavior()
    {
        Debug.Log("A random behavior has been selected");
        WishManager wishManager = currentSitObject.GetComponent<WishManager>();
        if (wishManager != null)
        {
            wishManager.SelectRandomBehavior();
        }
    }
}
