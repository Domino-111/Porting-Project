using UnityEngine;

public class EnemySpawn : Spawner, IReset
{
    protected override void Spawn()
    {
        //magic numbers here are based on the size of the Camera
        Vector3 spawnPosition = Random.insideUnitCircle.normalized * (7f * 2f * 0.9f);

        //spawn the enemy
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        //reduce the current delay between enemies
        spawnDelayCurrent -= 1f/60f;
    }

}
