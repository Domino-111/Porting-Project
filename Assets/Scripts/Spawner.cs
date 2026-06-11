using UnityEngine;

public abstract class Spawner : MonoBehaviour, IReset
{
    [Tooltip("The maximum length to wait between spawns")]
    [SerializeField] protected float spawnDelayMax = 3f;
    [Tooltip("What prefab to spawn")]
    [SerializeField] protected GameObject prefabToSpawn;

    //The current length to wait between spawns
    protected float spawnDelayCurrent;

    //The time of the last spawn
    protected float timeLastSpawn;

    // Update is called once per frame
    void Update()
    {
        //Do nothing if the game is not playing
        if (!Game.IsPlaying)
            return;

        //If current time has surpassed the last spawned time plus our delay, we should act
        if (Time.time > timeLastSpawn + spawnDelayCurrent)
        {
            //Set the time last spawned
            timeLastSpawn = Time.time;

            //Spawn something
            Spawn();
        }
    }

    /// <summary>
    /// Define how this Spawner should spawn its prefab.
    /// </summary>
    protected abstract void Spawn();

    public void Reset()
    {
        //Reset to defaults
        timeLastSpawn = Time.time;
        spawnDelayCurrent = spawnDelayMax;
        Spawn();
    }
}
