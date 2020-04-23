using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    bool PlayerNearby;
    PlayerStats PlayerStats;
    public int HealthAmount;
    public GameObject ActionTextObject;
    Text ActionText;
    AudioSource SoundEffect;
    public float EatCoolDown;
    float Timer;

    void Awake() {
        PlayerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats>();
        ActionText = ActionTextObject.GetComponent<Text>();
        SoundEffect = GetComponent<AudioSource>();
        // Set Timer to EatCoolDown so there is no delay on first time
        Timer = EatCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerNearby) {
            Timer += Time.deltaTime;
            // Eat with E-key
            if(Input.GetButtonDown("Action") && Timer >= EatCoolDown) {
                SoundEffect.Play();
                PlayerStats.AddHealth(HealthAmount);
                Timer = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other) { 
        if(other.tag == "Player") {
            // Update Action Text
            ActionText.text = "Eat E (Y)";
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
