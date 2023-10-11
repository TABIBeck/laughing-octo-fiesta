using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEnd : MonoBehaviour
{
    public int level = 0;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("levelDone", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            anim.SetBool("levelDone", true);
            Invoke("ChangeScene", 0.1f);
        }
    }
    void ChangeScene()
    {
        level++;
        
        SceneManager.LoadScene(level.ToString());
    }
}
