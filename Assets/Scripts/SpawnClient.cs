using UnityEngine;

public class SpawnClient : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private GameObject spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && clientPrefab != null && spawnPoint != null)
        {
            Instantiate(clientPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
