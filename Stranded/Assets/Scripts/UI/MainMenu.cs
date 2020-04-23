using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string Story = "It is year 8 452, humanity has been able to achieve interstellar space-travel and has quickly colonized other worlds. Our traveller was part of a merchant convoy that was struct by space pirates and in the chaos where everything was lost, our traveler managed to get into an emergency pod and escape. The traveller crashlanded into Mars and was Stranded there and now he has to survice and figure a way to get out of that planet.";
    public AudioClip StoryAudio;
    AudioSource Audio;
    bool StartStoryTell = false;
    public GameObject StoryScreen;
    public float TypeSpeed;
    Text StoryText;
    float Timer;
    int CurrentChar = 0;
    int MaxChar;
    bool done = false;
    GameObject GameStartInstructions;
    Text GameStartText;

    void Awake() {
        Audio = GetComponent<AudioSource>();
        StoryText = StoryScreen.transform.GetChild(0).gameObject.GetComponent<Text>();
        GameStartInstructions = StoryScreen.transform.GetChild(1).gameObject;
        GameStartText = GameStartInstructions.GetComponent<Text>();
        MaxChar = Story.Length;
    }

    public void PlayGame() 
    {
        // Play Story Audio
        Audio.PlayOneShot(StoryAudio);
        // Show StoryScreen
        StoryScreen.SetActive(true);
        // Start StoryTelling
        StartStoryTell = true;
    }

    void LoadGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    void Update() {
        // Type Story
        if(StartStoryTell) {
            Timer += Time.deltaTime;
            if(Timer >= TypeSpeed && CurrentChar < MaxChar) {
                Timer = 0;
                StoryText.text += Story[CurrentChar];
                CurrentChar += 1;
            }
            if(CurrentChar == MaxChar) {
                done = true;
                // Show GameStartInstructions
                GameStartInstructions.SetActive(true);
            }
        }
        if(done) {
            if(Input.GetButtonDown("Action")) {
                LoadGame();
                GameStartText.text = "Loading...";
            }
        }
    }
}
