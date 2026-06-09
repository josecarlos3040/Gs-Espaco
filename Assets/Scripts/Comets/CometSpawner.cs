using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{

    public int attempts = 0;
    [Header("Prefabs")]
    [SerializeField] GameObject[] cometPrefabs;

    [Header("Spawn")]
    [SerializeField] int cometAmount = 20;

    [SerializeField] float spawnX = -100f;

    [SerializeField] float minY = 50f;
    [SerializeField] float maxY = 250f;

    [SerializeField] float minZ = 80f;
    [SerializeField] float maxZ = 180f;

    [Header("Spacing")]
    [SerializeField] float minDistanceBetweenComets = 20f;

    List<Vector3> spawnedPositions = new();

    [SerializeField] PlayerMove playerMove;
    [SerializeField] bool firstSpawned = false;

    private void Update()
    {
        if(playerMove.fuelMoonComplete && playerMove.sellMoonComplete && firstSpawned == false)
        {
            firstSpawned = true;
            SpawnComets();
        }
    }
    public void SpawnComets()
    {
        attempts = 0;
        int secondPrefabCount = 0;

        while (spawnedPositions.Count < cometAmount && attempts < 1000)
        {
            attempts++;

            Vector3 spawnPos = new Vector3(
                spawnX,
                Random.Range(minY, maxY),
                Random.Range(minZ, maxZ)
            );

            bool validPosition = true;

            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(pos, spawnPos) < minDistanceBetweenComets)
                {
                    validPosition = false;
                    break;
                }
            }

            if (!validPosition)
                continue;

            int randomIndex;
            if (secondPrefabCount >= 3)
            {
                do
                {
                    randomIndex = Random.Range(0, cometPrefabs.Length);
                }
                while (randomIndex == 1);
            }
            else
            {
                randomIndex = Random.Range(0, cometPrefabs.Length);
            }

            GameObject randomComet = cometPrefabs[randomIndex];

            if (randomIndex == 1)
                secondPrefabCount++;

            Instantiate(randomComet, spawnPos, Quaternion.identity, transform);

            spawnedPositions.Add(spawnPos);
        }
    }

    public void DestroyAllComets()
    {
        attempts = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            
        }
        // Limpa as posições registradas para permitir novo spawn
        spawnedPositions.Clear();
    }
}