using UnityEngine;

public class Register : MonoBehaviour
{
    public BehaviorExecutor activeExecutor;

    void Start()
    {
        if (activeExecutor == null)
        {
            Debug.LogWarning("BehaviorExecutor not found in " + gameObject.name);
        }
    }

    void Update()
    {
        
    }
}
