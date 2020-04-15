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

    void Awake() {
        ActionText = ActionTextObject.GetComponent<Text>();
    }

    void Update() {
        if(PlayerNearby) {
            if(Input.GetKey(KeyCode.E)) {
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
            if(Timer >= TimeToFix) {
                Repairing = false;
                // Disable Action Text
                ActionTextObject.SetActive(false);
                // Disable Particle Effect
                Sparks.SetActive(false);
                CableFixed = true;
            }
        }else {
            // Update Action Text
            ActionText.text = "Repair Cable (E)";
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !CableFixed) {
            // Update Action Text
            ActionText.text = "Repair Cable (E)";
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
