using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShot : StraightShot
{
    public float rotationPerSecond; // the number of degrees that the missile rotates towards the target direction each second

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPathtoPlayer = PathtoPlayer(PlayerMove.activePlayerPos); // vector towards the active player position that the PlayerMove class stores

        facingDir = Vector3.RotateTowards(facingDir, currentPathtoPlayer, (rotationPerSecond * Mathf.Deg2Rad * Time.deltaTime), 0);

        float angle = Mathf.Atan2(facingDir.y, facingDir.x); // gets the angle of the new facingDir
        angle = angle * Mathf.Rad2Deg; // convert the angle from radians to degrees
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // sets the shot's rotation to the new facingDir angle

        myRb2D.velocity = facingDir * speed; // sets the velocity to match the new facingDir

        /* //Unfinished and now obsolete because I found out that RotateTowards exists, but keeping in case necessary later
        float angleDifference = Vector3.Angle(facingDir, currentPathtoPlayer); // the difference between where it's facing and where it should be facing (towards the player)

        float targetAngle = 0; // sets up the target angle that we'll redefine below before facing the shot towards it

        if (Mathf.Abs(angleDifference) <= rotationPerTick) // if the shot would rotate to the target angle in one rotationPerTick
        {
            targetAngle = Mathf.Atan2(facingDir.y, facingDir.x); // gets the angle the shot should face, but in radians
            targetAngle = targetAngle * Mathf.Rad2Deg; // convert the angle from radians to degrees

            facingDir = currentPathtoPlayer;

        }
        else if (angleDifference > 0)
        {
            targetAngle = (Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg) + rotationPerTick; // gets a new angle that is rotationPerTick closer to the path to
                                                                                                           // the player than the current facingDir
        }
        else if (angleDifference < 0)
        {
            targetAngle = (Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg) - rotationPerTick; // gets a new angle that is rotationPerTick closer to the path to
                                                                                                     // the player than the current facingDir
        } */
    }
}
