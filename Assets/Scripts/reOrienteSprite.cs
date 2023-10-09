using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reOrienteSprite : MonoBehaviour
{
    private Rigidbody2D rb;
    public float raycastDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, raycastDistance);
    }
}
