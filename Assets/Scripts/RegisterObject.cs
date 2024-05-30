using UnityEngine;

public class RegisterObject : MonoBehaviour
{
    public bool caught { get; set; } = false;

    public GameObject client { get; set; } = null;

    public float timeToCook = 1.0f;

    public float timeToCoolDown = 10.0f;
}
