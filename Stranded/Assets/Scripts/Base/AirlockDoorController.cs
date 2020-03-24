using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockDoorController : MonoBehaviour
{
    Animator Animator;
    public bool IsOpen;
    public bool PlayerNearby = false;
    
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
        if(PlayerNearby) {
            Animator.SetBool("IsOpen", true);
            IsOpen = true;
        }
    }

    // Close Door
    void CloseDoor() {
        if(PlayerNearby) {
            Animator.SetBool("IsOpen", false);
            IsOpen = false;
        }
    }
}
