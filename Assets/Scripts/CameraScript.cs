using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    GameObject player; 
    GameObject bottom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bottom = GameObject.Find("World Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        if ( player.transform.position.y <= bottom.transform.position.y) 
        {
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, bottom.transform.position.y,-10), Quaternion.identity);
        }
    }
}
