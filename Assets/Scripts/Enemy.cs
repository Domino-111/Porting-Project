using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IStop, IReset
{
    [Tooltip("How fast the enemy should move per sec")]
    [SerializeField] private float speed;

    /// <summary>
    /// Called when any enemy is destroyed.
    /// </summary>
    public static Action onEnemyDestroy;

    void Update()
    {
        //Make sure we're looking towards the centre of the screen
        transform.up = -transform.position.normalized;

        //Move towards the centre of the screen
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If we get hit by a missile
        if (other.TryGetComponent<Missile>(out Missile missile))
        {
            //Destroy ourselves and the missile
            Destroy(other.gameObject);
            Destroy(gameObject);

            //Add a random amount of scrap to our total
            Scrap.Add(UnityEngine.Random.Range(1, 5));
        }
    }

    public void Stop()
    {
        //Stop moving
        speed = 0f;
    }

    public void Reset()
    {
        //Remove yourself on a new game
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        //Only invoke the destruction event if we're currently in game
        if (Game.IsPlaying)
            onEnemyDestroy?.Invoke();
    }
}
