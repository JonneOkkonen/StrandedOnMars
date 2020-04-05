using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabricatorController : MonoBehaviour
{
    GameObject MainCamera;
    GameObject FabricatorCamera;
    public GameObject Player;
    bool PlayerNearby = false;
    public GameObject ActionTextObject;
    Text ActionText;

    void Awake()
    {
        // Get Both Cameras
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        FabricatorCamera = GameObject.FindGameObjectWithTag("FabricatorCamera");
        ActionText = ActionTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerNearby) {
            if(Input.GetKeyDown(KeyCode.E)) {
                SwitchCameras();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            // Update Action Text
            ActionText.text = "Use Fabricator (E)";
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

    // Switch Cameras
    void SwitchCameras() {
        print("Switching cameras" + MainCamera.activeSelf);
        if(MainCamera.activeSelf) {
            Player.SetActive(false);
            MainCamera.SetActive(false);
            FabricatorCamera.SetActive(true);
            // Disable Action Text
            ActionTextObject.SetActive(false);
        }else {
            Player.SetActive(true);
            FabricatorCamera.SetActive(false);
            MainCamera.SetActive(true);
        }
    }
}
