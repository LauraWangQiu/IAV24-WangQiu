using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ChangeBehavior2 : MonoBehaviour
{
    [SerializeField] private BehaviorExecutor executor;
    [SerializeField] private BrickAsset[] assets;

    void Start()
    {
        if (executor != null)
        {
            executor.enabled = true;
        }
        else
        {
            Debug.LogError("Please assign the BehaviorExecutor component to the field in the inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Changing behavior...");
            BrickAsset asset = ChooseRandomBrickAsset();
            if (asset == null) return;
            executor.behavior = asset;
        }
    }

    BrickAsset ChooseRandomBrickAsset()
    {
        if (assets.Length == 0)
        {
            Debug.LogError("Please assign the BrickAsset components to the field in the inspector.");
            return null;
        }

        BrickAsset asset;
        do
        {
            asset = assets[Random.Range(0, assets.Length)];
        } while (asset == executor.behavior);

        return asset;
    }
}
