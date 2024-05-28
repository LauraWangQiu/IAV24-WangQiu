using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private GameObject spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && clientPrefab != null && spawnPoint != null)
        {
            GameObject client = Instantiate(clientPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            RestaurantRegister restaurantRegister = GetComponent<RestaurantRegister>();
            if (restaurantRegister != null)
            {
                restaurantRegister.AddClient(client);
            }
        }
    }
}
