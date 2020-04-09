using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public GameObject TextObject;
    Animator animator;
    Text NotificationText;
    public bool ShowNotification = false;
    public float VisibilityTime = 2f;
    float Timer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        NotificationText = TextObject.GetComponent<Text>();
    }

    void Update() {
        // Show notification when true
        if(ShowNotification) {
            // Show Notification
            ShowPanel();
            // Start Timer
            Timer += Time.deltaTime;
            // After time has passed hide panel
            if(Timer >= VisibilityTime) {
                HidePanel();
            }
        }
    }

    // Hide Panel
    void HidePanel() {
        animator.SetBool("IsVisible", false);
        ShowNotification = false;
        Timer = 0;
    }

    // Show Panel
    void ShowPanel() {
        animator.SetBool("IsVisible", true);
    }

    // Set Objective Text
    public void SetPanelText(string text, float time = 0) {
        NotificationText.text = text;
        if(time != 0) {
            VisibilityTime = time;
        }
        // Show Notification
        ShowNotification = true;
    }
}
