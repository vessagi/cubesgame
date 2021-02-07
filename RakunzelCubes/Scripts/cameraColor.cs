/*
The codes can manage the game speed over a certain period of time, change the background color, manage events such as stopping the game, ending the game and play sounds
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //required: UI elements such as text, panel etc...
using UnityEngine.SceneManagement; //required: loadscene function
public class cameraColor : MonoBehaviour
{
    //GAME MECHANICS HERE!!!
    public Color[] colors; //Background colors
    public GameObject panel; //UI Panel: Game pause
    public GameObject panelCountDown; //UI Panel: Countdown after resume
    public GameObject panelGameOver; //UI Panel: Gameover
    public GameObject loadingBar; //UI Panel: Loading
    private Color rndColor; //Color: Random color from "colors"
    private Camera cam; //Camera: cam (main camera)
    private float timeScaler = 0.9F; //float: handles gamespeed
    private float runEveryXSecs = 45f; //float: triggers InvokeRepeating by given number 
    float timer = 0; //float: timer start number. 
    float timerDown = 3; //float: timer stop number, also handles the numbers visible on panelCountDown
    public AudioClip[] audioNotes; //AudioClip: random notes on key press and events
    public AudioClip[] audioChords; //AudioClip: random chords on key press and events
    void Start()
    {
        //Start function.
        Time.timeScale =timeScaler; //sets gamespeed
        cam = gameObject.GetComponent<Camera>(); //gets camera component
        cam.clearFlags = CameraClearFlags.SolidColor; //sets camera background to show color
        rndColor=colors[Random.Range(0,colors.Length)]; //picks a random color
        StartCoroutine(changeColor(rndColor,1)); //animates color change
        InvokeRepeating("eachXsecs", runEveryXSecs,runEveryXSecs); //makes game faster every "runEveryXSecs" second 
        
    }
    public void newColor(){
        rndColor=colors[Random.Range(0,colors.Length)]; //picks a random color
        StartCoroutine(changeColor(rndColor,1)); //animates color change
    }
    private void eachXsecs(){
            newColor(); 
            timeScaler+=.1F; //adds .1F
            Time.timeScale =timeScaler; //sets new game speed
    }      
    public void pause(){
        panel.SetActive(true); //shows pause menu
        Time.timeScale =0; //stops game

    }   
    public void gameOver(){
        panelGameOver.SetActive(true); //shows gameover menu
        Time.timeScale =0; //stops game
        RandomChords();  //plays a chord

    }
    public void loadscene(string sceneName)
    {   
        SceneManager.LoadSceneAsync(sceneName); //loads given scene with string
        loadingBar.SetActive(true); //shows loading panel
    }
    public void resume(){
        //RESUME after "3" secs countdown
        panel.SetActive(false); //hides pause menu
        panelCountDown.SetActive(true); //shows countdown menu
        StartCoroutine(resumeAfterNSeconds(3.0f)); //runs coroutine "resumeAfterNSeconds"
    }
    
    IEnumerator changeColor(Color color , float duration)
    {
        float counter = 0f; //number to handle time. 
        while (counter < duration) //runs if counter is lower than duration
        {   
            counter += Time.deltaTime; //add counter value
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, color, counter); //changes "cam.backgroundColor" to "color" by "counter" time
            yield return null; //lets the game handle other codes while color changes 
        }
    }
    IEnumerator resumeAfterNSeconds(float timePeriod)
    {
        yield return new WaitForEndOfFrame(); //keeps the countdown loop. "resumeAfterNSeconds" will be called inside the loop.
        timer += Time.unscaledDeltaTime; //sets wait time
        timerDown -= Time.unscaledDeltaTime; //sets countdown number  
        panelCountDown.transform.GetChild(0).gameObject.GetComponent<Text>().text=((int)timerDown+1).ToString(); //gets Text component of first child of "panelCountDown" and sets its text to ((int)timerDown+1).ToString(). // ((int)timerDown+1).ToString()= converts timerDown to an int value and adds 1 to it then converts it to a string. 
        if(timer < timePeriod) //runs if timer is lower than timePeriod
            StartCoroutine(resumeAfterNSeconds(3.0f)); //calls self to keep countdown
        else
        {
            Time.timeScale = timeScaler; //sets game time to "timeScaler". timeScaler = previous game time.
            timer = 0; //sets timer 0
            timerDown=timePeriod; //sets timerDown to timePeriod (=3)
            panelCountDown.SetActive(false); //hides countdown menu
        }
    }    
    public void RandomNotes()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioNotes[Random.Range(0, audioNotes.Length)],2.5F); //plays random clip from "audioNotes"
    }
    
    public void RandomChords()
    {   
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioChords[Random.Range(0, audioChords.Length)],.1F); //plays random clip from "audioChords"
    }
}