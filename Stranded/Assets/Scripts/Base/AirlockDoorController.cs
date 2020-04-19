using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockDoorController : MonoBehaviour
{
    Animator Animator;
    public bool IsOpen;
    public bool PlayerNearby = false;
    public bool CanBeOpened;
    
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Animator.SetBool("IsOpen", false);
        IsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Press E to Open/Close Door
        if(Input.GetButtonDown("Action"))
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
