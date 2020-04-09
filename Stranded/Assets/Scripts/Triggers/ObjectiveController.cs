using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    public GameObject ObjectiveTextObject;
    Animator animator;
    bool IsVisible = false;
    Text ObjectiveText;
    void Awake()
    {
        animator = GetComponent<Animator>();
        ObjectiveText = ObjectiveTextObject.GetComponent<Text>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            UnHideObjectivePanel();
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            HideObjectivePanel();
        }
    }

    void HideObjectivePanel() {
        IsVisible = false;
        animator.SetBool("IsVisible", false);
    }

    void UnHideObjectivePanel() {
        IsVisible = true;
        animator.SetBool("IsVisible", true);
    }

    // Set Objective Text
    public void SetObjective(string text) {
        ObjectiveText.text = text;
    }
}
