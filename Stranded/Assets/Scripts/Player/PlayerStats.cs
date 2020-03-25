using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        OxygenBar = OxygenBarObject.GetComponent<Slider>();
        OxygenBarText = OxygenBarObject.GetComponentInChildren(typeof(Text), true) as Text;

        // Set Oxygen to Max
        Oxygen = MaxOxygen;

        // Set Slider Max Value
        OxygenBar.maxValue = MaxOxygen;

        // Start OxygenTick
        Invoke("OxygenTick", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        OxygenBar.value = Oxygen;
        OxygenBarText.text = $"Oxygen: {Oxygen}s";
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
            // No Oxygen
        }
        Invoke("OxygenTick", 1f);
    }
}
