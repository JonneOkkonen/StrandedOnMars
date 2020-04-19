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
    public int currentHealth;
    public int startingHealth = 100;
	public GameObject HealthBarObject;
	Slider HealthBar;
	Text HealthBarText;
	public GameObject StaminaBarObject;
    public float MaxStamina;
	float PlayerStamina;
	Slider StaminaBar;
    public float StaminaRegenSpeed;
    public int Ammo;
    public int MagazineSize;
    public GameObject AmmoTextObject;
    Text AmmoText;

    void Awake()
    {
        OxygenBar = OxygenBarObject.GetComponent<Slider>();
        OxygenBarText = OxygenBarObject.GetComponentInChildren(typeof(Text), true) as Text;
		
		HealthBar = HealthBarObject.GetComponent<Slider>();
		HealthBarText = HealthBarObject.GetComponentInChildren(typeof(Text), true) as Text;
		
		StaminaBar = StaminaBarObject.GetComponent<Slider>();	
        PlayerController = GetComponent<RigidbodyFirstPersonController>();	
        
        PlayerActionController = GetComponent<PlayerActionController>();
        PointsText = PointsTextObject.GetComponent<Text>();
        BeaconController = GetComponent<BeaconLocationController>();
        AmmoText = AmmoTextObject.GetComponent<Text>();

        // Set Oxygen to Max
        Oxygen = MaxOxygen;

        // Set Oxygen Slider Max Value
        OxygenBar.maxValue = MaxOxygen;

        // Set Health Slider Max Value
        HealthBar.maxValue = startingHealth;

        // Set Stamina Slider Max Value
        StaminaBar.maxValue = MaxStamina;

        // Set Points Text
        SetPointsText(Points);

        // Start OxygenTick
        Invoke("OxygenTick", 1f);
		
		// Initialize Player Health
		currentHealth = startingHealth;

        // Initialize Stamina
        PlayerStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // Update OxygenBar
        OxygenBar.value = Oxygen;
        OxygenBarText.text = $"Oxygen: {Oxygen} s";
		
        // Update StaminaBar
		StaminaBar.value = PlayerStamina;

        // Update HealthBar
		if(currentHealth < 0) 
		{
			HealthBar.value = currentHealth;
			HealthBarText.text = $"Health: 0 HP";
		} else
		{
			HealthBar.value = currentHealth;
			HealthBarText.text = $"Health: {currentHealth} HP";
		}

        // Respawn after Death
        if(IsDead) {
            if(Input.GetButtonDown("Respawn")) {
                SceneManager.LoadScene("Mars");
            }
        }

        // Change Run Multiplier
        if(PlayerStamina > 0) {
            RigidbodyFirstPersonController.AllowedToRun = true;
        }else {
            RigidbodyFirstPersonController.AllowedToRun = false;
        }

        // Use Stamina when running
        if(Input.GetButton("Run") || Input.GetAxis("Run") > 0.5) {
            if(PlayerStamina > 0) {
                PlayerStamina -= Time.deltaTime;
                // Don't go under 0
                if(PlayerStamina < 0) {
                    PlayerStamina = 0;
                }
                // Update Stamina Bar
                StaminaBar.value = PlayerStamina;
            }
        }else {
            // Regen Stamina when not running
            if(PlayerStamina < MaxStamina) {
                PlayerStamina += Time.deltaTime * StaminaRegenSpeed;
                if(PlayerStamina > MaxStamina) {
                    PlayerStamina = MaxStamina;
                }
            }
        }
    }

    // Oxygen Tick
    private void OxygenTick()  {
        if(Oxygen > 0) {
            // Use Oxygen
            if(UsingOxygen && !IsDead) {
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
	
	// Player takes damage
	public void TakeDamage(int amount) 
	{	
		// Substract amount of damage taken
		currentHealth -= amount;
		
		// If the player dies, call Die();
		if(currentHealth <= 0 && !IsDead)
        {
			Debug.Log("No health left");
            Die();
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

    // Add Health
    public void AddHealth(int amount) {
        if(currentHealth < startingHealth) {
            currentHealth += amount;
        }
    }

    // Set PlayerHasBeacon to true
    public void AddBeacon() {
        BeaconPanel.SetActive(true);
        PlayerHasBeacon = true;
        BeaconController.enabled = true;
    }

    // Disable Player Controll temporarily
    public void Pause() {
        // Disable Player Movement
        PlayerController.enabled = false;
        // Disable Gun
        PlayerActionController.enabled = false;
    }

    // Enable Player Controll
    public void Continue() {
        // Disable Player Movement
        PlayerController.enabled = true;
        // Disable Gun
        PlayerActionController.enabled = true;
    }

    // Add Ammo
    public void AddAmmo(int amount) {
        // Add Ammo
        Ammo += amount;
        // Update AmmoText
        AmmoText.text = $"(0) {Ammo.ToString()}";
    }
}
