using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject player; 

    GameObject bottom;

    // Start is called before the first frame update
    void Start()
    {
        bottom = GameObject.Find("World Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        //if ( player.transform.y <= bottom.transform.y) { }
    }
}
