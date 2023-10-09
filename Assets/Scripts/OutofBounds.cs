using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OutofBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -14 || transform.position.x < -9.5 || transform.position.x > 9.5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   
    }
}
