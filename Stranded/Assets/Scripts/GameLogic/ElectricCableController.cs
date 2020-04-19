using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricCableController : MonoBehaviour
{
    public GameObject ActionTextObject;
    public GameObject Sparks;
    Text ActionText;
    public bool CableFixed = false;
    public float TimeToFix;
    bool PlayerNearby = false;
    float Timer;
    bool Repairing = false;
    PlayerStats PlayerStats;

    void Awake() {
        PlayerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats>();
        ActionText = ActionTextObject.GetComponent<Text>();
    }

    void Update() {
        if(PlayerNearby) {
            if(Input.GetButton("Action")) {
                Repairing = true;
                Timer += Time.deltaTime;
            }else {
                Repairing = false;
                Timer = 0;
            }
        }
        if(Repairing) {
            // Update Action Text
            ActionText.text = "Repairing...";
            if(Timer >= TimeToFix && !CableFixed) {
                Repairing = false;
                // Disable Action Text
                ActionTextObject.SetActive(false);
                // Disable Particle Effect
                Sparks.SetActive(false);
                CableFixed = true;
                // Give little electric shock to player
                PlayerStats.TakeDamage(5);
            }
        }else {
            // Update Action Text
            ActionText.text = "Repair Cable E (Y)";
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !CableFixed) {
            // Update Action Text
            ActionText.text = "Repair Cable E (Y)";
            // Enable Action Text
            ActionTextObject.SetActive(true);
            PlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            // Disable Action Text
            ActionTextObject.SetActive(false);
            PlayerNearby = false;
        }
    }
}
