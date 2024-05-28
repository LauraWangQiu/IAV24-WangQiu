using UnityEngine;
using UnityEngine.UI;

public class AddEventCamera : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
