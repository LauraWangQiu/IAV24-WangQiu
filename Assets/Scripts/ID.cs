using UnityEngine;

public class ID : MonoBehaviour
{
    public int id;

    RestaurantRegister register;

    private void Awake()
    {
        register = FindObjectOfType<RestaurantRegister>();
        if (register != null)
        {
            id = register.GetNextClientId();
        }
    }
}
