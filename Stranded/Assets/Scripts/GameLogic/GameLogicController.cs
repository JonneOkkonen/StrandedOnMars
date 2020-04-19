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
        "Find a place to shelter",
        "Take control of the base, by taking out all the metalon's",
        "Try to get to the base",
        "Restore power to the base. Check power cable for damages",
        "Go inside base",
        "Look what fabricator has to offer",
        "Get enough points to buy beacon",
        "Buy beacon",
        "Set Beacon to high ground",
        "Wait for the rescue party to arrive and try not to die",
        "No Objective"
    };

    List<string> Notifications = new List<string>(){
        "Move using (W,A,S,D, ArrowKeys or LeftStick). Press Q (LB) to take out your huge gun and shoot with left mouse (RB) button.",
        "You have a new objective in your objective menu. Open it with TAB-key (RT)."
    };

    int CurrentObjective = -1;
    NotificationController NotificationController;
    ObjectiveController ObjectiveController;
    public float FirstObjectiveTime;
    float Timer;
    public float UpdateFrequency;
    AudioSource VoiceLines;
    bool VoiceLine3Triggered = false;
    bool VoiceLine4Triggered = false;
    bool VoiceLine7Triggered = false;
    bool VoiceLine12Triggered = false;
    bool Rescued = false;
    public GameObject AirlockTrigger;
    AirlockPressurisationController AirlockPressurisationController;
    public GameObject FabricatorTrigger;
    FabricatorController FabricatorController;
    PlayerStats PlayerStats;
    BeaconLocationController BeaconLocationController;
    RescueController RescueController;
    public float RescueTime;
    float RescueTimer;

    void Awake()
    {
        NotificationController = NotificationObject.GetComponent<NotificationController>();
        ObjectiveController = ObjectiveObject.GetComponent<ObjectiveController>();
        ElectricCableController = ElectricCable.GetComponent<ElectricCableController>();
        AirlockDoorController = AirlockOuterDoor.GetComponent<AirlockDoorController>();
        VoiceLines = GetComponent<AudioSource>();
        AirlockPressurisationController = AirlockTrigger.GetComponent<AirlockPressurisationController>();
        FabricatorController = FabricatorTrigger.GetComponent<FabricatorController>();
        PlayerStats = GetComponent<PlayerStats>();
        BeaconLocationController = GetComponent<BeaconLocationController>();
        RescueController = GetComponent<RescueController>();
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
                // Play Voiceline when seeing base
                if(distance <= 250) {
                    if(!VoiceLine3Triggered) {
                        PlayVoiceLine(3);
                        VoiceLine3Triggered = true;
                    }
                }
                // Play Trigger Next objective when base found
                if(distance <= 180) {
                    NextObjective();
                    if(!VoiceLine4Triggered) {
                        PlayVoiceLine(4);
                        VoiceLine4Triggered = true;
                    }
                }
            }
            // Take Control of the base
            if(CurrentObjective == 1) {
                if(BaseMetalonGroup.transform.childCount == 0) {
                    NextObjective();
                    PlayVoiceLine(5);
                }
            }
            // Try to get inside base
            if(CurrentObjective == 2) {
                if(AirlockDoorController.PlayerNearby) {
                    PlayVoiceLine(6);
                    NextObjective();
                }
            }
            // Restore power to base
            if(CurrentObjective == 3) {
                if(!VoiceLine7Triggered && !VoiceLines.isPlaying) {
                    PlayVoiceLine(7);
                    VoiceLine7Triggered = true;
                }
                if(ElectricCableController.CableFixed) {
                    // Enable ElectricCableController
                    ElectricCableController.enabled = false;
                    // Enable ElectricCableCollider
                    ElectricCable.GetComponent<SphereCollider>().enabled = true;
                    // Enable AirlockDoor
                    AirlockDoorController.CanBeOpened = true;
                    NextObjective();
                    PlayVoiceLine(8);
                }
            }
            // Go to base
            if(CurrentObjective == 4) {
                if(AirlockPressurisationController.IsPressurized) {
                    NextObjective();
                    PlayVoiceLine(9);
                }
            }
            // Look what fabricator has to offer
            if(CurrentObjective == 5) {
                if(FabricatorController.FabricatorActive) {
                    NextObjective();
                    PlayVoiceLine(11);
                }
            }
            // Get enough points to buy beacon
            if(CurrentObjective == 6) {
                if(!VoiceLine12Triggered && !VoiceLines.isPlaying) {
                    PlayVoiceLine(12);
                    VoiceLine12Triggered = true;
                }
                if(PlayerStats.Points >= FabricatorController.BeaconPrize) {
                    NextObjective();
                }
            }
            // Buy beacon
            if(CurrentObjective == 7) {
                if(PlayerStats.PlayerHasBeacon) {
                    NextObjective();
                    PlayVoiceLine(13);
                }
            }
            // Set Beacon to high ground
            if(CurrentObjective == 8) {
                if(BeaconLocationController.BeaconSet) {
                    // Set all enemies to attack player
                    EnemyMovement.GroupAttack = true;
                    NextObjective();
                }
            }
            // Wait for the rescue party to arrive
            if(CurrentObjective == 9) {
                RescueTimer += 2;
                if(RescueTimer >= RescueTime && !Rescued) {
                    Rescued = true;
                    PlayVoiceLine(14);
                    RescueController.RescuePlayer();
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
        PlayVoiceLine(2);
    }

    // Show first notification at start of the game
    IEnumerator FirstNotification() 
    {
        yield return new WaitForSeconds(1f);
        NotificationController.SetPanelText(Notifications[0], 10);
        PlayVoiceLine(1);
    }

    // Play selected VoiceLine
    void PlayVoiceLine(int voiceline) {
        // Stop If Playing
        if(VoiceLines.isPlaying) {
            VoiceLines.Stop();
        }
        var newClip = Resources.Load<AudioClip>("VoiceLines/VoiceLine" + voiceline.ToString());
        VoiceLines.PlayOneShot(newClip);
    }
}
