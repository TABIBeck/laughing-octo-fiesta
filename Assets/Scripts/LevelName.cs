using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelName : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start() // grabs the attached textmesh and changes its text to be the name of the level its in
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        textMesh.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
