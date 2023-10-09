using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Collider c_Collider;
    // Start is called before the first frame update
    void Start()
    {
        /*
        //c_Collider = GetComponent<Collider2D>();
        //c_Collider.enabled = !c_Collider.enabled;
        //Destroy(GetComponent<Collider2D>());
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Schmoney money = other.gameObject.GetComponent<Schmoney>();
        money.schmoneyRecieved();
        
        Destroy(gameObject);
        
    }
}
