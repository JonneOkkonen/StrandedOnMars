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
    Text BeaconBuyText;
    Text MagazineBuyText;
    public int BeaconPrize;
    public int MagazinePrize;
    bool BuyingBeacon = false;
    bool BuyingMagazine = false;
    float Timer;
    float Timer2;
    bool BeaconBought = false;
    public GameObject Fabricator;
    AudioSource FabricatorAudio;
    public bool FabricatorActive = false;

    void Awake()
    {
        // Get Both Cameras
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ActionText = ActionTextObject.GetComponent<Text>();
        PlayerStats = Player.GetComponent<PlayerStats>();
        BeaconBuyText = FabricatorUIObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        MagazineBuyText = FabricatorUIObject.transform.GetChild(7).gameObject.GetComponent<Text>();
        FabricatorAudio = Fabricator.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check that player is nearby
        if(PlayerNearby) {
            // Switch Cameras with E-key
            if(Input.GetButtonDown("Action")) {
                SwitchCameras();
            }
            // Buy beacon with holding B
            if(Input.GetButton("BuyRight") || Input.GetAxis("BuyRight") > 0.5) {
                BuyingBeacon = true;
                Timer += Time.deltaTime;
            }else {
                BuyingBeacon = false;
                Timer = 0;
            }
            // Buy Magazine with holding M
            if(Input.GetButton("BuyLeft") || Input.GetAxis("BuyLeft") < -0.5) {
                BuyingMagazine = true;
                Timer2 += Time.deltaTime;
            }else {
                MagazineBuyText.text = "Buy magazine by holding M (Left)-button";
                BuyingMagazine = false;
                Timer2 = 0;
            }
        }
        // Check that Beacon hasn't been bought
        if(!BeaconBought) {
            // Buy
            if(BuyingBeacon) {
                // Check that player has enough points
                if(PlayerStats.Points >= BeaconPrize) {
                    BeaconBuyText.text = "Buying Beacon...";
                    if(Timer >= 1) {
                        BuyingBeacon = false;
                        BuyBeacon();
                    }
                }else {
                    BeaconBuyText.text = "You don't have enough points";
                }
            }else if(BuyingMagazine) {
                // Check that player has enough points
                if(PlayerStats.Points >= MagazinePrize) {
                    MagazineBuyText.text = "Buying Magazine";
                    if(Timer2 >= 0.5) {
                        BuyMagazine();
                        Timer2 = 0;
                    }
                }else {
                    MagazineBuyText.text = "You don't have enough points";
                }
            }
            else {
                BeaconBuyText.text = "Buy Beacon by holding B (Right)-button";
                MagazineBuyText.text = "Buy magazine by holding M (Left)-button";
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            // Update Action Text
            ActionText.text = "Use Fabricator E (Y)";
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
            // Activate Fabricator
            FabricatorActive = true;
            // Play Welcome Message
            FabricatorAudio.Play();
            // Pause Player
            PlayerStats.Pause();
            // Disable Main Camera
            MainCamera.SetActive(false);
            // Enable Fabricator Camera
            FabricatorCamera.SetActive(true);
            // Disable Action Text
            ActionTextObject.SetActive(false);
            // Enable Fabricator UI
            FabricatorUIObject.SetActive(true);
        }else {
            // Disable Fabricator
            FabricatorActive = false;
            // Enable Player
            PlayerStats.Continue();
            // Disable Fabricator Camera
            FabricatorCamera.SetActive(false);
            // Enable Main Camera
            MainCamera.SetActive(true);
            // Disable Fabricator UI
            FabricatorUIObject.SetActive(false);
        }
    }

    // Buy Beacon
    public void BuyBeacon() {
        BeaconBought = true;
        BeaconBuyText.text = "You just bought a beacon";
        PlayerStats.UsePoints(BeaconPrize);
        PlayerStats.AddBeacon();
    }

    // Buy Magazine
    public void BuyMagazine() {
        PlayerStats.UsePoints(MagazinePrize);
        PlayerStats.AddAmmo(PlayerStats.MagazineSize);
    }
}
