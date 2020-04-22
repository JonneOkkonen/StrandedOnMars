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
    public bool IsPressurized = false;
    bool Pressurizing = false;
    bool PlayerNearby = false;
    bool Ready = false;
    public GameObject InformationTextObject;
    Text InformationText;
    PlayerStats PlayerStats;
    AudioSource SoundEffect;
    public float PressurizationTime;
    float Timer;

    void Awake()
    {
        InnerDoor = InnerDoorObject.GetComponent<AirlockDoorController>();
        OuterDoor = OuterDoorObject.GetComponent<AirlockDoorController>();
        InformationText = InformationTextObject.GetComponent<Text>();
        PlayerStats = Player.GetComponent<PlayerStats>();
        SoundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Doors are closed Airlock is ready to be pressurized
        if(InnerDoor.IsOpen == false && OuterDoor.IsOpen == false && !Pressurizing) {
            Ready = true;
            if(IsPressurized) {
                InformationText.text = "Airlock Ready. Press F (B) to depressurize.)";
            }else {
                InformationText.text = "Airlock Ready. Press F (B) to pressurize.)";
            }
        }else {
            Ready = false;
            InformationText.text = "Airlock Not Ready. Close both doors.";
        }
        // If airlock is ready and player is nearby enable airlock control
        if(Ready && PlayerNearby) {
            if(Input.GetButtonDown("Action2") && !Pressurizing) {
                Pressurizing = true;
                if(IsPressurized) {
                    InnerDoor.CanBeOpened = false;
                }else {
                    OuterDoor.CanBeOpened = false;
                }
                SoundEffect.Play();
                Timer = 0;
            }
        }

        // Pressurizing
        if(Pressurizing) {
            InformationText.text = "Airlock Pressurizing...";
            Timer += Time.deltaTime;
            if(Timer >= PressurizationTime) {
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
                Pressurizing = false;
                Timer = 0;
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
