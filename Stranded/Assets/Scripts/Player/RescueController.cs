using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RescueController : MonoBehaviour
{
    public GameObject RescueLight;
    public GameObject Camera;
    PlayerStats PlayerStats;
    bool LiftPlayer = false;
    public float LiftingSpeed;
    public float CameraRotationSpeed;
    public GameObject GameEndScreen;
    Rigidbody PlayerRB;

    void Awake() {
        PlayerStats = GetComponent<PlayerStats>();
        PlayerRB = GetComponent<Rigidbody>();
    }

    void Update() {
        if(LiftPlayer) {
            transform.position = transform.position + new Vector3(0, LiftingSpeed, 0);
            // Rotate Camera
            if((Camera.transform.rotation.eulerAngles).x < 60 || (Camera.transform.rotation.eulerAngles).x > 300) {
                Camera.transform.rotation = Camera.transform.rotation * Quaternion.Euler(new Vector3(CameraRotationSpeed, 0, 0));
            }
        }
    }

    public void RescuePlayer() {
        RescueLight.SetActive(true);
        StartCoroutine("StartRescueProcess");
    }

    void StartRescue() {
        // Disable Player Rigidbody
        PlayerRB.isKinematic = true;
        // Disable Player Control
        PlayerStats.Pause();
        // Set Health Full
        PlayerStats.currentHealth = 100;
        // Regen Oxygen
        PlayerStats.OxygenRegen = true;
        // Start Lifting Player
        LiftPlayer = true;
        // Show EndScreen
        StartCoroutine("ShowEndScreen");
        // Start GameEnd
        StartCoroutine("GameEnd");
    }

    // Start Rescue
    IEnumerator StartRescueProcess() {
        yield return new WaitForSeconds(5f);
        StartRescue();
    }

    // Show EndScreen
    IEnumerator ShowEndScreen() {
        yield return new WaitForSeconds(2f);
        GameEndScreen.SetActive(true);
    }

    // End Game
    IEnumerator GameEnd() {
        yield return new WaitForSeconds(10f);
        LiftPlayer = false;
        SceneManager.LoadScene("Menu");
    }
}
