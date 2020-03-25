using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirlockPressurisationController : MonoBehaviour
{
    public GameObject InnerDoorObject;
    public GameObject OuterDoorObject;
    public GameObject Player;
    AirlockDoorController InnerDoor;
    AirlockDoorController OuterDoor;
    bool IsPressurized = false;
    bool PlayerNearby = false;
    bool Ready = false;
    public GameObject InformationTextObject;
    Text InformationText;
    PlayerStats PlayerStats;

    void Awake()
    {
        InnerDoor = InnerDoorObject.GetComponent<AirlockDoorController>();
        OuterDoor = OuterDoorObject.GetComponent<AirlockDoorController>();
        InformationText = InformationTextObject.GetComponent<Text>();
        PlayerStats = Player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Doors are closed Airlock is ready to be pressurized
        if(InnerDoor.IsOpen == false && OuterDoor.IsOpen == false) {
            Ready = true;
            if(IsPressurized) {
                InformationText.text = "Airlock Ready. Press F to depressurize.)";
            }else {
                InformationText.text = "Airlock Ready. Press F to pressurize.)";
            }
        }else {
            Ready = false;
            InformationText.text = "Airlock Not Ready. Close both doors.";
        }
        // If airlock is ready and player is nearby enable airlock control
        if(Ready && PlayerNearby) {
            if(Input.GetKeyDown(KeyCode.F)) {
                // Depressurize Airlock
                if(IsPressurized) {
                    IsPressurized = false;
                    InnerDoor.CanBeOpened = false;
                    OuterDoor.CanBeOpened = true;

                    // When Depressurized
                    // Enable Player Oxygen Usage
                    // Disable Oxygen Regen
                    PlayerStats.UsingOxygen = true;
                    PlayerStats.OxygenRegen = false;
                } // Pressurize Airlock
                else {
                    IsPressurized = true;
                    InnerDoor.CanBeOpened = true;
                    OuterDoor.CanBeOpened = false;

                    // When Pressurized
                    // Disable Player Oxygen Usage
                    // Enable Oxygen Regen
                    PlayerStats.UsingOxygen = false;
                    PlayerStats.OxygenRegen = true;
                }
            }
        }
    }

    // Player is in airlock
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            InformationTextObject.SetActive(true);
            PlayerNearby = true;
        }
    }

    // Player left airlock
    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            InformationTextObject.SetActive(false);
            PlayerNearby = false;
        }
    }
}
