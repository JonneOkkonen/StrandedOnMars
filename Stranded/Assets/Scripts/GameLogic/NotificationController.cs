using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public GameObject TextObject;
    Animator animator;
    Text Text;
    public bool ShowNotification = true;
    public float VisibilityTime = 2f;
    float Timer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        Text = TextObject.GetComponent<Text>();
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
    }

    // Show Panel
    void ShowPanel() {
        animator.SetBool("IsVisible", true);
    }

    // Set Objective Text
    public void SetPanelText(string text) {
        Text.text = text;
        // Show Notification
        ShowNotification = true;
    }
}
