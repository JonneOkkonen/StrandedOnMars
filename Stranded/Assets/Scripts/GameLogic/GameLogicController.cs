using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject NotificationObject;
    public GameObject ObjectiveObject;
    List<string> Objectives = new List<string>(){
        "Find the base that we saw during crash landing",
        "Take control of the base",
        "Restore power to the base"
    };

    List<string> Notifications = new List<string>(){
        "Move using (W,A,S,D or ArrowKeys). Press Q to take out your huge gun and shoot with left mouse button.",
        "You have a new objective in your objective menu. Open it with TAB-key."
    };

    int CurrentObjective = -1;
    NotificationController NotificationController;
    ObjectiveController ObjectiveController;
    public float FirstObjectiveTime;

    void Awake()
    {
        NotificationController = NotificationObject.GetComponent<NotificationController>();
        ObjectiveController = ObjectiveObject.GetComponent<ObjectiveController>();
    }

    void Start() {
        // Show first notification at start of the game
        StartCoroutine("FirstNotification");

        // Start First Objective with Delay
        StartCoroutine("StartFirstObjective");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Activate next objective
    void NextObjective() {
        // Go to next objective
        CurrentObjective += 1;
        // Show notification that you have a new objective
        NotificationController.SetPanelText(Notifications[1]);
        // Set Objective Description text
        ObjectiveController.SetObjective(Objectives[CurrentObjective]);
    }

    // Start First Objective
    IEnumerator StartFirstObjective() 
    {
        yield return new WaitForSeconds(FirstObjectiveTime);
        NextObjective();
    }

    // Show first notification at start of the game
    IEnumerator FirstNotification() 
    {
        yield return new WaitForSeconds(1f);
        NotificationController.SetPanelText(Notifications[0], 10);
    }
}
