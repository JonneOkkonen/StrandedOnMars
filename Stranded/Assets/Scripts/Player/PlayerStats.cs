using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using System.Globalization;

public class PlayerStats : MonoBehaviour
{
    public float Oxygen;
    public float MaxOxygen;
    public int OxygenRegenSpeed;
    public GameObject OxygenBarObject;
    Slider OxygenBar;
    Text OxygenBarText;
    public bool UsingOxygen = true;
    public bool OxygenRegen = false;
    RigidbodyFirstPersonController PlayerController;
    PlayerActionController PlayerActionController;
    public bool IsDead = false;
    public GameObject DeadScreen;
    public int Points;
    public GameObject PointsTextObject;
    Text PointsText;
    public bool PlayerHasBeacon = false;
    public GameObject BeaconPanel;
    BeaconLocationController BeaconController;

    void Awake()
    {
        OxygenBar = OxygenBarObject.GetComponent<Slider>();
        OxygenBarText = OxygenBarObject.GetComponentInChildren(typeof(Text), true) as Text;
        PlayerController = GetComponent<RigidbodyFirstPersonController>();
        PlayerActionController = GetComponent<PlayerActionController>();
        PointsText = PointsTextObject.GetComponent<Text>();
        BeaconController = GetComponent<BeaconLocationController>();

        // Set Oxygen to Max
        Oxygen = MaxOxygen;

        // Set Slider Max Value
        OxygenBar.maxValue = MaxOxygen;

        // Set Points Text
        SetPointsText(Points);

        // Start OxygenTick
        Invoke("OxygenTick", 1f);
		
		currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        OxygenBar.value = Oxygen;
        OxygenBarText.text = $"Oxygen: {Oxygen}s";

        // Respawn after Death
        if(IsDead) {
            if(Input.GetKeyDown(KeyCode.X)) {
                SceneManager.LoadScene("Mars");
            }
        }
    }

    // Oxygen Tick
    private void OxygenTick()  {
        if(Oxygen > 0) {
            // Use Oxygen
            if(UsingOxygen) {
                Oxygen--;
            }
            // Regen Oxygen Supply
            if(OxygenRegen) {
                // If Oxygen is lower than max then regen
                if(Oxygen < MaxOxygen) {
                    Oxygen += OxygenRegenSpeed;
                    // Check that Oxygen is not higher than max
                    if(Oxygen > MaxOxygen) {
                        Oxygen = MaxOxygen;
                    }
                }
            }
        }else {
            Debug.Log("No Oxygen Left");
            // Die
            Die();
        }
        Invoke("OxygenTick", 1f);
    }
	
	public void takeDamage(int amount) 
	{
		//damaged = true;
		currentHealth -= amount;
		if(currentHealth <= 0 && !isDead)
        {
            Die ();
        }
	}

    // Player Died
    private void Die() {
        IsDead = true;
        DeadScreen.SetActive(true);
        PlayerController.enabled = false;
        PlayerActionController.enabled = false;
        Debug.Log("Player has died");
    }

    // Use Points
    public void UsePoints(int amount) {
        Points -= amount;
        // Update PointsText
        SetPointsText(Points);
    }

    // Get Points
    public void GetPoints(int amount) {
        Points += amount;
        // Update PointsText
        SetPointsText(Points);
    }

    // Set Points Text
    private void SetPointsText(int points) {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        PointsText.text = "Points: " + points.ToString("#,0", nfi);
    }

    // Set PlayerHasBeacon to true
    public void AddBeacon() {
        BeaconPanel.SetActive(true);
        PlayerHasBeacon = true;
        BeaconController.enabled = true;
    }
}
