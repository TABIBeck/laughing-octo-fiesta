using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ScrollClouds : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f; // the speed at which the clouds scroll

    private RectTransform myRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRectTransform.position += new Vector3(scrollSpeed * Time.deltaTime, 0f);

        Vector3 screenSize = new Vector3(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight);
        if (myRectTransform.position.x >= screenSize.x * 1.5) // when it leaves the view of the camera, sets its position to be just
              // off screen to the right (assumes that it is the same size as the camera)
        {
            myRectTransform.position = new Vector3(screenSize.x / -2, screenSize.y / 2);
        }
        Debug.Log(screenSize);
    }
}
