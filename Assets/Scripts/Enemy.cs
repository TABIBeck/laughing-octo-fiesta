using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 firingPos = Vector3.zero; // the position relative to the center of the enemy that the enemy spawns their bullets at, defaults to 0
    public float shotDelay; // delay between shots in seconds, will be randomly offset a bit
    public GameObject[] attackPatterns; // The different attack game objects that the enemy can spawn

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    protected IEnumerator Attack() // Executes a custom attack or series of attacks using FireShot()
    {
        yield return new WaitForSeconds(shotDelay * 2); // pause so that it doesn't attack immediately on spawn

        while (true) // attacks as long as the enemy is active
        {
            FireShot();
            yield return new WaitForSeconds(shotDelay + Random.Range((shotDelay / -10), (shotDelay / 10))); // waits before shooting again (+/- 10% of delay)
        }
    }

    protected void FireShot()
    {
        if(attackPatterns.Length > 0)
        {
            Instantiate(attackPatterns[Random.Range(0, attackPatterns.Length)], transform.position + firingPos, Quaternion.identity); // creates a
                // random one of its attackpatterns at its firing position (center + firingpos)
        } else
        {
            Debug.Log("No attack patterns are assigned for the enemy");
        }
    }
}
