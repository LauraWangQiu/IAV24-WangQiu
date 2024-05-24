using UnityEngine;

public class ChangeBehavior : MonoBehaviour
{
    [SerializeField] private BehaviorExecutor first;
    [SerializeField] private BehaviorExecutor second;
    private Register register;

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

        register = GetComponent<Register>();
        if (register != null)
        {
            register.activeExecutor = first;
        }
        else
        {
            Debug.LogError("Register component not found.");
        }
    }

    void Update()
    {
        // Change behavior when pressing space key and the register is not in SIT state
        if (Input.GetKeyDown(KeyCode.Space) && first != null && second != null && 
            register != null && register.currentState != Register.State.SIT)
        {
            Debug.Log("Changing behavior...");
            first.enabled = !first.enabled;
            second.enabled = !second.enabled;
            register.activeExecutor = first.enabled ? first : second;
        }
    }
}
