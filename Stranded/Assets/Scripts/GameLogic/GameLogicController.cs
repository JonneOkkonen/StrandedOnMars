using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject NotificationObject;
    public GameObject ObjectiveObject;
    public GameObject Base;
    public GameObject BaseMetalonGroup;
    public GameObject ElectricCable;
    public GameObject AirlockOuterDoor;
    ElectricCableController ElectricCableController;
    AirlockDoorController AirlockDoorController;
    List<string> Objectives = new List<string>(){
        "Go the base that we saw during crash landing",
        "Take control of the base, by taking out all the metalon's",
        "Restore power to the base. Check power cable for damages",
        "Go inside base"
    };

    List<string> Notifications = new List<string>(){
        "Move using (W,A,S,D or ArrowKeys). Press Q to take out your huge gun and shoot with left mouse button.",
        "You have a new objective in your objective menu. Open it with TAB-key."
    };

    int CurrentObjective = -1;
    NotificationController NotificationController;
    ObjectiveController ObjectiveController;
    public float FirstObjectiveTime;
    float Timer;
    public float UpdateFrequency;

    void Awake()
    {
        NotificationController = NotificationObject.GetComponent<NotificationController>();
        ObjectiveController = ObjectiveObject.GetComponent<ObjectiveController>();
        ElectricCableController = ElectricCable.GetComponent<ElectricCableController>();
        AirlockDoorController = AirlockOuterDoor.GetComponent<AirlockDoorController>();
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
        Timer += Time.deltaTime;
        // Run Checks with UpdateFrequency
        if(Timer >= UpdateFrequency) {
            Timer = 0;
            // Find Base Objective
            if(CurrentObjective == 0) {
                float distance = Vector3.Distance(Base.transform.position, transform.position);
                if(distance <= 120) {
                    NextObjective();
                }
            }
            // Take Control of the base
            if(CurrentObjective == 1) {
                if(BaseMetalonGroup.transform.childCount == 0) {
                    NextObjective();
                }
            }
            // Restore power to base
            if(CurrentObjective == 2) {
                if(ElectricCableController.CableFixed) {
                    // Enable ElectricCableController
                    ElectricCableController.enabled = false;
                    // Enable ElectricCableCollider
                    ElectricCable.GetComponent<SphereCollider>().enabled = true;
                    // Enable AirlockDoor
                    AirlockDoorController.CanBeOpened = true;
                    NextObjective();
                }
            }
        }
    }

    // Activate next objective
    void NextObjective() {
        // Go to next objective
        CurrentObjective += 1;
        // Show notification that you have a new objective
        NotificationController.SetPanelText(Notifications[1]);
        // Set Objective Description text
        ObjectiveController.SetObjective(Objectives[CurrentObjective]);

        // Addional Stuff needed to initialize objective
        if(CurrentObjective == 2) {
            // Enable ElectricCableController
            ElectricCableController.enabled = true;
            // Enable ElectricCableCollider
            ElectricCable.GetComponent<SphereCollider>().enabled = true;
        }
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
