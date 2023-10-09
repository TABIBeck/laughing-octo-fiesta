using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static int maxHealth = 3;
    private int currentHealth = maxHealth; // each player starts at full health

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static int CheckMaxHealth() { return maxHealth; } // read only accessor for maxHealth

    public void Hurt(int damage) // takes the damage of an attack as an integer and lowers the player's health by that much
    {
        for (int i = 0; i < damage; i++) // we need to deal each tick of damage individually for the UI to be able to disable the hearts, though
            // the heart disabling animations can run at the same time
        {
            currentHealth -= 1;
            if (currentHealth >= 0) // dipping into negatives is fine, but we don't want the UI trying to disable a negative heart
            {
                UIManager.LoseHeart(currentHealth); // lose a heart in the UI, since the health just went down the heart's ID should be equal to the health now
            }
        }
        if (currentHealth <= 0 ) // if the player's health hits zero, they die
        {
            Die();
        }
    }

    private void Die() // the player dies
    {
        GameManager.state = GameState.Dead;
        UIManager.ShowRestartText();
        Destroy(this.gameObject);
    }
}
