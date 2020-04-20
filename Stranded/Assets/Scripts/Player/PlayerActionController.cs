using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public GameObject Gun;
    Animator GunAnimator;
    bool IsGunVisible;
    public GameObject Crosshair;

    void Awake() {
        GunAnimator = Gun.GetComponent<Animator>();
        Gun.SetActive(false);
        Crosshair.SetActive(false);
        GunAnimator.SetBool("IsVisible", false);
        IsGunVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Hide Gun with Q-key
        if(Input.GetButtonDown("Gun")) {
            if(IsGunVisible) {
                // Disable Gun
                StartCoroutine("DisableGun");
                // Start Gun Hide Animation
                GunAnimator.SetBool("IsVisible", false);
                IsGunVisible = false;
            }else {
                // Enable Gun
                Gun.SetActive(true);
                // Enable Crosshair
                Crosshair.SetActive(true);
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
        // Disable Crosshair
        Crosshair.SetActive(false);
        Gun.SetActive(false);
    }
}
