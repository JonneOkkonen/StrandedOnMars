using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public GameObject Gun;
    Animator GunAnimator;
    bool IsGunVisible;

    void Awake() {
        GunAnimator = Gun.GetComponent<Animator>();
        Gun.SetActive(false);
        GunAnimator.SetBool("IsVisible", false);
        IsGunVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Hide Gun with Q-key
        if(Input.GetKeyDown(KeyCode.Q)) {
            if(IsGunVisible) {
                // Disable Gun
                StartCoroutine("DisableGun");
                // Start Gun Hide Animation
                GunAnimator.SetBool("IsVisible", false);
                IsGunVisible = false;
            }else {
                // Enable Gun
                Gun.SetActive(true);
                // Start Gun UnHide Animation
                GunAnimator.SetBool("IsVisible", true);
                IsGunVisible = true;
            }
        }
    }

    // Disable Gun after 800ms
    IEnumerator DisableGun() 
    {
        yield return new WaitForSeconds(.8f);
        Gun.SetActive(false);
    }
}
