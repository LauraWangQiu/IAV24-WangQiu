using UnityEngine;

public class Register : MonoBehaviour
{
    public BehaviorExecutor activeExecutor;
    public enum State { IDLE, MOVING, SIT };

    public State currentState;

    private Vector3 exitPosition;

    void Start()
    {
        if (activeExecutor == null)
        {
            Debug.LogWarning("BehaviorExecutor not found in " + gameObject.name);
        }
        currentState = State.MOVING;
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public Vector3 GetExitPosition()
    {
        return exitPosition;
    }

    public void SetExitPosition(Vector3 position)
    {
        exitPosition = position;
    }
}
