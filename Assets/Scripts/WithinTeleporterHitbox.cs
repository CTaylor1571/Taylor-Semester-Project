using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.WSA;

public class WithinTeleporterHitbox : MonoBehaviour
{

    [NonSerialized]
    GameObject openChestText;

    [SerializeField]
    GameObject chargingRadius;

    [NonSerialized]
    GameManagerScript gmScript;

    bool activatable;
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        chargingRadius.SetActive(false);
        gmScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        openChestText = GameObject.Find("OpenChestText");
        activatable = false;
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activatable && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Starting Teleporter Event");
            StaticStats.stageNum++;
            StaticStats.itemsArr = gmScript.GetArr();
            BeginSequence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("Player") && !activated)
        {
            activatable = true;
            openChestText.GetComponent<Text>().color = Color.white;
            openChestText.GetComponent<Text>().text = "E - Begin Charging Sequence";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Equals("Player") && !activated)
        {
            activatable = false;
            openChestText.GetComponent<Text>().color = Color.clear;
        }
    }

    private void BeginSequence()
    {
        StaticStats.teleporterActive = true;
        activated = true;
        activatable = false;
        openChestText.GetComponent<Text>().color = Color.clear;
        chargingRadius.SetActive(true);
        chargingRadius.GetComponent<WithinChargingRadius>().StartCharging();

    }
    public void EndSequence()
    {
        activated = false;
        StaticStats.teleporterActive = false;
        SceneManager.LoadScene(sceneBuildIndex: StaticStats.stageNum);
    }


}
