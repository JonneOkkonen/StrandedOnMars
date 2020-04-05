using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeaconLocationController : MonoBehaviour
{
    public GameObject ActionTextObject;
    public GameObject Beacon;
    public GameObject BeaconPanel;
    public Vector3 BeaconOffset;
    PlayerStats PlayerStats;
    Text ActionText;
    public float GameEndTime;
    bool PlayerInArea = false;
    bool BeaconSet = false;
    float Timer;
    public int BeaconLocationHeight;

    void Awake() {
        ActionText = ActionTextObject.GetComponent<Text>();
        PlayerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player is in area and has beacon
        if(PlayerInArea && PlayerStats.PlayerHasBeacon) {
            if(Input.GetKeyDown(KeyCode.E) && BeaconSet == false) {
                BeaconSet = true;
                Instantiate(Beacon, transform.position - BeaconOffset, Quaternion.Euler(-90,0,0));
                BeaconPanel.SetActive(false);
            }
        }
        // If Area is suitable for beacon
        if(transform.position.y >= BeaconLocationHeight) {
            PlayerEntersArea();
        }else {
            PlayerLeftArea();
        }
    }

    void PlayerEntersArea() {
        if(BeaconSet) {
            // Disable Action Text
            ActionTextObject.SetActive(false);
        }else {
            // Update Action Text
            ActionText.text = "Set Beacon (E)";
            // Enable Action Text
            ActionTextObject.SetActive(true);
        }
        PlayerInArea = true;
    }

    void PlayerLeftArea() {
        // Disable Action Text
        ActionTextObject.SetActive(false);
        PlayerInArea = false;
    }
}
