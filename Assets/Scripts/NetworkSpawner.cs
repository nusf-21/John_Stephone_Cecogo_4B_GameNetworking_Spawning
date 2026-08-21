using Unity.Netcode;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour
{
    public GameObject networkPrefab;

    private NetworkObject spawnedObject;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Invoke(nameof(SpawnObject), 2f);
        }
    }

    private void SpawnObject()
    {
        GameObject obj = Instantiate(
            networkPrefab,
            new Vector3(0, 1, 0),
            Quaternion.identity
        );

        spawnedObject = obj.GetComponent<NetworkObject>();

        spawnedObject.Spawn();

        Invoke(nameof(DespawnObject), 5f);
    }

    private void DespawnObject()
    {
        if (spawnedObject != null && spawnedObject.IsSpawned)
        {
            spawnedObject.Despawn();
        }
    }
}