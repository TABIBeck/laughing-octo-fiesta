using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHeart : MonoBehaviour
{
    private UnityEngine.UI.Image myImage;

    [SerializeField] private Sprite[] sprites = new Sprite[5];
    [SerializeField] private float animLength; // how long the animation lasts in seconds

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void LoseHeart() // called by UIManager to make the heart empty animation play out
    {
        StartCoroutine(Empty());
    }

    IEnumerator Empty()
    {
        if (sprites.Length > 0)
        {
            float interval = animLength / sprites.Length;
            for (int i = 0; i < sprites.Length; i++)
            {
                myImage.sprite = sprites[i];
                yield return new WaitForSeconds(interval);
            }
        } else
        {
            Debug.Log("Sprite list is empty");
        }
    }
}
