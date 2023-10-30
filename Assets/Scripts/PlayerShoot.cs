using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    GameObject pellet;

    float timer;
    float secondaryTimer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        secondaryTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && secondaryTimer == 0)
        {
            SpawnBurst();
        } 
        else if (Input.GetMouseButton(0) && timer ==0)
        {
            SpawnPellet();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (secondaryTimer > 0)
        {
            secondaryTimer -= Time.deltaTime;
        }
        else
        {
            secondaryTimer = 0;
        }

        
    }
    
    void SpawnPellet()
    {
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        timer = 0.3f - (StaticStats.numItem4 * 0.025f);
        if (timer < 0.1f)
        {
            timer = 0.1f;
        }
    }

    void SpawnBurst()
    {
        transform.Rotate(new Vector3(0, 0, 2));
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        transform.Rotate(new Vector3(0, 0, -1));
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        transform.Rotate(new Vector3(0, 0, -2));
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        transform.Rotate(new Vector3(0, 0, -1));
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        transform.Rotate(new Vector3(0, 0, 2));
        Instantiate(pellet, gameObject.transform.position, gameObject.transform.rotation);
        timer = 0.5f;
        secondaryTimer = 5f;
    }
}
