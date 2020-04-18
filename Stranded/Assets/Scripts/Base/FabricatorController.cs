using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabricatorController : MonoBehaviour
{
    GameObject MainCamera;
    public GameObject FabricatorCamera;
    public GameObject Player;
    bool PlayerNearby = false;
    public GameObject ActionTextObject;
    Text ActionText;
    public GameObject FabricatorUIObject;
    PlayerStats PlayerStats;
    Text BuyText;
    public int BeaconPrize;
    bool Buying = false;
    float Timer;
    bool BeaconBought = false;
    public GameObject Fabricator;
    AudioSource FabricatorAudio;

    void Awake()
    {
        // Get Both Cameras
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ActionText = ActionTextObject.GetComponent<Text>();
        PlayerStats = Player.GetComponent<PlayerStats>();
        BuyText = FabricatorUIObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        FabricatorAudio = Fabricator.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check that player is nearby
        if(PlayerNearby) {
            // Switch Cameras with E-key
            if(Input.GetKeyDown(KeyCode.E)) {
                SwitchCameras();
            }
            // Buy beacon with holding B
            if(Input.GetKey(KeyCode.B)) {
                Buying = true;
                Timer += Time.deltaTime;
            }else {
                Buying = false;
                Timer = 0;
            }
        }
        // Check that Beacon hasn't been bought
        if(!BeaconBought) {
            // Buy
            if(Buying) {
                // Check that player has enough points
                if(PlayerStats.Points >= BeaconPrize) {
                    BuyText.text = "Buying Beacon...";
                    if(Timer >= 1) {
                        Buying = false;
                        BuyBeacon();
                    }
                }else {
                    BuyText.text = "You don't have enough points";
                }
            }else {
                BuyText.text = "Buy Beacon by holding B-key";
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
            // Stpp Welcome Message
            FabricatorAudio.Stop();
            // Disable Action Text
            ActionTextObject.SetActive(false);
            PlayerNearby = false;
        }
    }

    // Switch Cameras
    void SwitchCameras() {
        print("Switching cameras" + MainCamera.activeSelf);
        if(MainCamera.activeSelf) {
            // Play Welcome Message
            FabricatorAudio.Play();
            Player.SetActive(false);
            MainCamera.SetActive(false);
            FabricatorCamera.SetActive(true);
            // Disable Action Text
            ActionTextObject.SetActive(false);
            // Enable Fabricator UI
            FabricatorUIObject.SetActive(true);
        }else {
            Player.SetActive(true);
            FabricatorCamera.SetActive(false);
            MainCamera.SetActive(true);
            // Disable Fabricator UI
            FabricatorUIObject.SetActive(false);
        }
    }

    // Buy Beacon
    public void BuyBeacon() {
        BeaconBought = true;
        BuyText.text = "You just bought a beacon";
        PlayerStats.UsePoints(BeaconPrize);
        PlayerStats.AddBeacon();
    }
}
