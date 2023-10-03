using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HittingWall : MonoBehaviour

{
    private Rigidbody2D rb;
    private bool isReorienting = false;

    public float rotationSpeed = 90f; // Adjust the speed of reorientation as needed.
    public float raycastDistance = 1f; // Adjust the raycast distance as needed.
    public LayerMask wallLayer; // Set the appropriate wall layer in the Inspector.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isReorienting)
        {
            // Perform a raycast to detect the wall in front of the sprite.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, raycastDistance, wallLayer);

            if (hit.collider != null)
            {
                // Calculate the desired rotation angle based on the hit normal.
                Vector2 hitNormal = hit.normal;
                float angle = Mathf.Atan2(hitNormal.y, hitNormal.x) * Mathf.Rad2Deg;

                // Gradually reorient the sprite.
                StartCoroutine(ReorientCoroutine(angle));
            }
        }
    }

    private IEnumerator ReorientCoroutine(float targetAngle)
    {
        isReorienting = true;

        while (Mathf.Abs(transform.eulerAngles.z - targetAngle) > 1f)
        {
            // Calculate the rotation step based on the desired rotation angle and time.
            float step = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * rotationSpeed);
            rb.MoveRotation(step);

            yield return null;
        }

        isReorienting = false;
    }
}
