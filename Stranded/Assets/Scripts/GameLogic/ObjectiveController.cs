using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    public GameObject ObjectiveTextObject;
    Animator animator;
    Text ObjectiveText;
    void Awake()
    {
        animator = GetComponent<Animator>();
        ObjectiveText = ObjectiveTextObject.GetComponent<Text>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            ShowObjectivePanel();
        }
        if(Input.GetKeyUp(KeyCode.Tab)) {
            HideObjectivePanel();
        }
    }

    void HideObjectivePanel() {
        animator.SetBool("IsVisible", false);
    }

    void ShowObjectivePanel() {
        animator.SetBool("IsVisible", true);
    }

    // Set Objective Text
    public void SetObjective(string text) {
        ObjectiveText.text = text;
    }
}
