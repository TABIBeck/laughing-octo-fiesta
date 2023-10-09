using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float curse; // Initial speed of the player in units per second after a max distance launch
    // in regards to the variable name of the above, I'm sorry Benno but it just wasn't working until I changed
    // the name to this. That's the only thing that changed. Just Visual Studio's rename thing. And then it worked. I can't argue with that
    [SerializeField] private float dragLimit; // The furthest out you can drag the mouse to increase the slime's launch power
    //The force that the player is launched at is launchForce * the percentage of this distance that the mouse is dragged away from the player

    private Rigidbody2D rb2d; // the object's rigidbody component

    [SerializeField] private LineRenderer launchRenderer; // a line showing the direction the player will be launched in

    [SerializeField] private Animator anim; // the animator for the player

    public static Vector3 activePlayerPos; // the position of the active player, stored by the class as a whole. As long as there isn't more than
                                           // one player in the scene at a time, this shouldn't cause any problems

    private void Awake()
    {
        activePlayerPos = transform.position; // sets the active player position to this object's position
        Time.timeScale = 3.0f; // because of the quick patch speed thing
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        launchRenderer = Instantiate(launchRenderer, Vector3.zero, transform.rotation); // Makes its own personal copy of the line renderer and holds onto that
        launchRenderer.enabled = false; // makes sure the line doesn't show up right away
    }

    // Update is called once per frame
    void Update()
    {
        activePlayerPos = transform.position; // sets the active player position to this object's position

        if (rb2d.velocity == Vector2.zero)
        {
            anim.SetBool("Airborne", false); // if its stopped it must not be airborne, so we'll tell the animation that
        }

        if (GameManager.state == GameState.Playing) // only allows for aiming and launching player if game is not paused or dead
        {
            if (Input.GetMouseButton(0)) // Checks if left mouse button is held down
            {
                CalculateLaunchLine(GetMouseWorldPosition()); // Sets the attached line renderer to draw a line showing the current launch direction
                launchRenderer.enabled = true;
                if (rb2d.velocity == Vector2.zero) // the line is transparent if it is not valid for launch, and solid if it is
                {
                    launchRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
                    anim.SetBool("Charging", true); // sets the animator to the charging animation
                }
                else
                {
                    launchRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
                }
            }
            if (Input.GetMouseButtonUp(0)) // Checks if left mouse button is released
            {
                launchRenderer.enabled = false;
                if (rb2d.velocity == Vector2.zero) // only launches if the player is not moving (which only happens when the player is on the ground)
                {
                    Launch(GetMouseWorldPosition()); // call Launch to launch the player towards the mouse position
                    anim.SetBool("Airborne", true);
                    anim.SetBool("Charging", false);
                }
            }
        }
    }

    /// <summary>
    /// Grabs the world position of the mouse with z = 0, stolen from the balloon popping soldier script
    /// </summary>
    /// <returns>World position of mouse as Vector3</returns>
    Vector3 GetMouseWorldPosition()
    {
        // this gets the current mouse position (in screen coordinates) and transforms it into world coordinates
        Vector3 mouseWorldPos = Vector3.zero; // starts at zero, because we need it defined
        if ((Input.mousePosition.x > ((Camera.main.pixelWidth / 2) - (Camera.main.pixelWidth / 6)))
            & (Input.mousePosition.x < ((Camera.main.pixelWidth / 2) + (Camera.main.pixelWidth / 6)))) // if mouse is in the center third of the screen (not on UI)
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            mouseWorldPos = transform.position;
        }
            
        // the camera is on z = -10, so all screen coordinates are on z = -10. To be on the same plane as the game, we need to set z to 0
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }

    void CalculateLaunchLine(Vector3 mouseWorldPosition)
    {
        Vector3 lineEnd = mouseWorldPosition;
        if (Mathf.Abs(Vector3.Distance(lineEnd, transform.position)) > dragLimit) { // if the mouse is dragged further away than the max dragLimit, only draw the line up to it
            Vector3 correctedDistance = (lineEnd - transform.position).normalized * dragLimit;
            lineEnd = transform.position + correctedDistance; // resets the line end to be dragLimit distance away from the player in the same direction as before
        }

        launchRenderer.positionCount = 2;
        Vector3[] positions = { transform.position, lineEnd };
        launchRenderer.SetPositions(positions);
    }

    void Launch(Vector3 mouseWorldPosition)
    {
        Vector3 launchVector = mouseWorldPosition - transform.position;
        if (launchVector.magnitude > dragLimit) // if it's stretched past the drag limit, reduce it to the dragLimit
        {
            launchVector = launchVector.normalized * dragLimit;
        }
        launchVector = launchVector.normalized * curse * (launchVector.magnitude / dragLimit); // sets its magnitude to the speed multiplied by the distance the mouse
            // was dragged divided by the dragLimit. So if it's dragged all the way to the limit, the magnitude of the resulting vector equals launchForce
        rb2d.velocity = launchVector; // set the rigidbody velocity equal to the calculated vector
        //StartCoroutine(Wiggle());
    }

    IEnumerator Wiggle() // sin function to change the scale of the player when it launches as the force ripples through it
    {
        float wiggleLength = 0.5f; // the number of seconds that it wiggles for
        float timer = 0f;
        float size; // target scale for this frame
        while (timer <= wiggleLength)
        {
            size = Mathf.Sin(timer * (24 * wiggleLength)) / 4;
            transform.localScale = new Vector3(1 + size, 1 + size, 1);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime); // runs again next frame
        }
    }



    /* //Horrible easing failure that lets you clip through the stage
    private float InElastic(float t) => 1 - OutElastic(1 - t);
    private float OutElastic(float t)
    {
        float p = 0.3f;
        return (float)Mathf.Pow(2, -10 * t) * (float)Mathf.Sin((t - p / 4) * (2 * Mathf.PI) / p) + 1;
    }
    private float InOutElastic(float t)
    {
        if (t < 0.5) return InElastic(t * 2) / 2;
        return 1 - InElastic((1 - t) * 2) / 2;
    }

    IEnumerator Wiggle() // easing function to change the scale of the player when it launches as the force ripples through it
    {
        float wiggleLength = 1; // the number of seconds that it wiggles for
        float size; // target scale for this frame
        while (wiggleLength > 0f)
        {
            size = InOutElastic(wiggleLength);
            transform.localScale = new Vector3(size, size, 1);
            wiggleLength -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime); // runs again next frame
        }
        while (wiggleLength < 1f)
        {
            size = InOutElastic(wiggleLength);
            transform.localScale = new Vector3(size, size, 1);
            wiggleLength += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime); // runs again next frame
        }
    } */
}
