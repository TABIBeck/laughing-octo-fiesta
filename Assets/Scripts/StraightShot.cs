using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : MonoBehaviour
{
    public float lifetime; // the number of seconds that the shot remains active for before exploding
    public float speed; // the speed of the projectile

    protected Vector3 facingDir; // The direction that the shot is facing in and moving towards
    protected Rigidbody2D myRb2D;

    // Start is called before the first frame update
    protected void Start()
    {
        myRb2D = GetComponent<Rigidbody2D>();

        facingDir = PathtoPlayer(PlayerMove.activePlayerPos); // sets the facingDir of the shot towards the active player position that the PlayerMove class stores
        
        myRb2D.velocity = facingDir * speed;

        // rotates the shot to face in the facingDir
        float angle = Mathf.Atan2(facingDir.y, facingDir.x); // gets the angle the shot should face, but in radians
        angle = angle * Mathf.Rad2Deg; // convert the angle from radians to degrees
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // sets the shot's rotation to that angle around the z (forward) axis

        StartCoroutine(CountDown()); // starts a countdown to destroy the shot when it's lifetime runs out
    }

    protected void OnCollisionEnter2D(Collision2D collision) // LATER change the script it grabs to whatever actually handles health
    {
        PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>(); // grabs a reference to the PlayerMove script of the target it hits, so that it can check
            // if the target has that script (and thus is a player) and so that it can hurt them
        if (playerMove != null ) // if it hit the player
        {
            // LATER when you have a hurt() function call it on the grabbed script here
        }
        Explode(); // explodes because it hit something
    }

    protected Vector3 PathtoPlayer(Vector3 playerPos) // just a quick function that returns a normalized vector of the direction from the shot to the provided position of the player
    {
        return (playerPos - transform.position).normalized;
    }

    protected IEnumerator CountDown() // Calls Explode to blow up the shot when it reaches the end of its lifetime
    {
        yield return new WaitForSeconds(lifetime);
        Explode();
    }

    protected void Explode() // the shot explodes, either when it hits something or when its lifetime runs out
    {
        Destroy(gameObject);
    }
}
