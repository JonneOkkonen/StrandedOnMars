using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirlockDoorController : MonoBehaviour
{
    Animator Animator;
    public bool IsOpen;
    public bool PlayerNearby = false;
    public bool CanBeOpened = true;
    public GameObject AnotherDoorObject;
    AirlockDoorController AnotherDoor;
    public GameObject InformationTextObject;
    Text InformationText;
    
    void Awake()
    {
        AnotherDoor = AnotherDoorObject.GetComponent<AirlockDoorController>();
        InformationText = InformationTextObject.GetComponent<Text>();
        Animator = GetComponent<Animator>();
        Animator.SetBool("IsOpen", false);
        IsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Change CanBeOpenedStatus based on anotherdoor status
        if(AnotherDoor.IsOpen) {
            CanBeOpened = false;
        }else {
            CanBeOpened = true;
        }

        // Press E to Open/Close Door
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(IsOpen) {
                CloseDoor();
            }else {
                OpenDoor();
            }

        }
    }

    // Open Door
    void OpenDoor() {
        // Make sure Player is nearby and door can be opened
        if(PlayerNearby && CanBeOpened) {
            Animator.SetBool("IsOpen", true);
            IsOpen = true;
            InformationTextObject.SetActive(false);
        }else if(PlayerNearby && CanBeOpened == false) {
            InformationTextObject.SetActive(true);
            InformationText.text = "Close another door first";
        }
    }

    // Close Door
    void CloseDoor() {
        // Make sure Player is nearby
        if(PlayerNearby) {
            Animator.SetBool("IsOpen", false);
            IsOpen = false;
        }
    }
}
