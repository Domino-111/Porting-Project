using UnityEngine;

public class Missile : MonoBehaviour
{
    //This is set by the gun when the missile spawns, so we can hide it in the inspector
    [HideInInspector] public float speed;

    //This holds the rigidbody of the missile
    private Rigidbody2D rb;


    void Start()
    {
        //Get the rigidbody
        rb = GetComponent<Rigidbody2D>();

        //Destroy this missile in 5 seconds
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Add force in your facing direction
        rb.AddForce(transform.up * speed);
    }
}
