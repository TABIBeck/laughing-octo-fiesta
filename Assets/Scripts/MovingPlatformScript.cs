using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{

    public GameObject point1;
    public GameObject point2;
    public float speed = 3.0f;
    private bool isMovingtoPoint2 = false;

    Vector3 point1pos;
    Vector3 point2pos;


    // Start is called before the first frame update
    void Start()
    {
        point1pos = point1.transform.position;
        transform.position = point1pos;
        point2pos = point2.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingtoPoint2)
        {
            float moveAmount = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point2pos, moveAmount);
            if (Vector3.Distance(transform.position, point2pos) < 0.05)
            {
                isMovingtoPoint2 = false;
            }
        }
        else
        {
            float moveAmount = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point1pos, moveAmount);
            if (Vector3.Distance(transform.position, point1pos) < 0.05)
            {
                isMovingtoPoint2 = true;
            }
        }
    }

}
