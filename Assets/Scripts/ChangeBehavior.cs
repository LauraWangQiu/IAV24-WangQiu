using UnityEngine;

public class ChangeBehavior : MonoBehaviour
{
    [SerializeField] private BehaviorExecutor first;
    [SerializeField] private BehaviorExecutor second;

    void Start()
    {
        if (first != null && second != null)
        {
            first.enabled = true;
            second.enabled = false;
        }
        else
        {
            Debug.LogError("Please assign the BehaviorExecutor components to the fields in the inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Changing behavior...");
            first.enabled = !first.enabled;
            second.enabled = !second.enabled;
        }
    }
}
