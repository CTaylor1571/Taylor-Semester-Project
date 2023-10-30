using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PelletBehavior : MonoBehaviour
{

    float lifeTimer;
    float accuracyChange;
    public bool isCritical;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Random.Range(0,9) < StaticStats.numItem5)
        {
            isCritical = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else
        {
            isCritical = false;
        }
        lifeTimer = 0.8f;
        float rand;
        accuracyChange = Convert.ToSingle(StaticStats.numItem3);
        if (accuracyChange < 5)
        {
            rand = UnityEngine.Random.Range(-7.5f + accuracyChange*1.5f, 7.5f - accuracyChange*1.5f);
        }
        else
        {
            rand = 0;
        }
        
        transform.Rotate(new Vector3(0,0,rand));
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Translate(0.053f,0f,0f, gameObject.transform);
        gameObject.transform.Translate(47.5f*Time.deltaTime,0f,0f, gameObject.transform);
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
