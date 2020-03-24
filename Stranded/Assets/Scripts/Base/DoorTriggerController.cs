using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTriggerController : MonoBehaviour
{
    public GameObject Door;
    AirlockDoorController AirLock;
    public GameObject ActionTextObject;
    Text ActionText;

    void Awake() {
        AirLock = Door.GetComponent<AirlockDoorController>();
        ActionText = ActionTextObject.GetComponent<Text>();
    }

    void Update() {
        // Update Action Text
        if(AirLock.IsOpen) {
            ActionText.text = "Close (E)";
        }else {
            ActionText.text = "Open (E)";
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            // Enable Action Text
            ActionTextObject.SetActive(true);
            AirLock.PlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            // Disable Action Text
            ActionTextObject.SetActive(false);
            AirLock.PlayerNearby = false;
        }
    }
}
