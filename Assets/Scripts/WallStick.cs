using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) // when this makes contact with an object that has a RigidBody, it disables that object's gravity
        // and sets its velocity to zero, essentially making it stick to the surface
    {
        Rigidbody2D otherRb2d = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb2d)
        {
            otherRb2d.gravityScale = 0;
            otherRb2d.velocity = Vector3.zero;
        }
    }
    private void OnCollisionStay2D(Collision2D other) // if an object with a rigidbody that has collided with it starts moving, it's gravity is returned as it detaches
        // from the wall. May cause issues when launched at an angle very close to parallel to the wall if the wall doesn't have a high friction coefficient
    {
        Rigidbody2D otherRb2d = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb2d && otherRb2d.velocity != Vector2.zero)
        {
            otherRb2d.gravityScale = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D other) // as an object leaves the wall its gravity is returned as it detaches from the wall
    {
        Rigidbody2D otherRb2d = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb2d)
        {
            otherRb2d.gravityScale = 1;
        }
    }
}
