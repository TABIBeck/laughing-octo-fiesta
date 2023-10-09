using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //public float crazy_meter = 1;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // locks camera's y
    void LateUpdate() // we move the camera on late update so that it always has the up to date version of activePlayerPos
    {
        Vector3 pos = transform.position;
        pos.y = PlayerMove.activePlayerPos.y + offsetY;
        if (PlayerMove.activePlayerPos.y >= offsetY)
        {
            transform.position = pos;
        }
    }

    //public void goCrazy()
    //{
    //    float newz = transform.rotation.eulerAngles.z + crazy_meter; 
    //    Quaternion quat = Quaternion.AngleAxis(newz, Vector3.forward);
    //    transform.rotation = quat;
        
    //    //StartCoroutine(yaaaa());
    //}
    //IEnumerator yaaaa()
    //{
    //    transform.position = new Vector3(transform.position.x, 1.15f, transform.position.z + crazy_meter);
    //        yield return new WaitForSeconds(.1f);
        
    //}

}
