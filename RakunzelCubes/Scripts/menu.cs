//GAME/MENU MECHANICS HERE!!!
/*
The codes can manage the game difficulty and skin shop 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //required: UI elements such as text, panel etc...
using UnityEngine.SceneManagement; //required: loadscene function

public class menu : MonoBehaviour
{
    //MENU MECHANICS HERE
    public GameObject loadingBar; //UI Panel: Loading
    public GameObject skins; //UI Panel: Skins
    public GameObject difficulty; //UI Panel: Difficulty
    public Text score; // UI Text: score on top
    public Text points; // UI Text: score to spend on skins
    private bool starting=true; //bool: handles skins on new game and game start
    public AudioClip[] audioNotes; //AudioClip: random notes on key press and events
    public AudioClip[] audioChords; //AudioClip: random chords on key press and events
    void Start()
    {
        score.text="Best Score:"+ PlayerPrefs.GetInt("maxScore").ToString(); //sets highscore from PlayerPrefs
        points.text="Points:"+ PlayerPrefs.GetInt("totalScore").ToString(); //sets points to spend on shop from PlayerPrefs
        setSkin( PlayerPrefs.GetInt("skin") ); //sets player skin from PlayerPrefs
        setDifficulty( PlayerPrefs.GetInt("difficulty") ); //sets difficulty from PlayerPrefs
    }  
    public void loadscene(string sceneName)
    {   
        SceneManager.LoadSceneAsync(sceneName); //loads given scene with string
        loadingBar.SetActive(true); //shows loading panel
    }
    public void setDifficulty(int x)
    {   
        PlayerPrefs.SetInt("difficulty",x); //sets playerprefs difficulty to "x" (0-1)
        for(int i = 0; i < difficulty.transform.childCount; i++) //loops "difficulty" children objects
        {
            difficulty.transform.GetChild(i).gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(160, 20); //makes children objects smaller
        }
        difficulty.transform.GetChild(x).gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(160, 40); //makes sleceted object bigger
    }
    public void setSkin(int x)
    {   
        int price=50; //sets price for skins
        if(starting){ 
            price=0; //sets skin price 0 on starting
            starting=false; //sets "starting" false
        }
        if(PlayerPrefs.GetInt("totalScore")>=price){ // if "totalScore" is greater than price:
            PlayerPrefs.SetInt("totalScore",PlayerPrefs.GetInt("totalScore")-price); // subtracts "price" from "totalScore" 
            points.text="Points:"+ PlayerPrefs.GetInt("totalScore").ToString(); //sets new "totalScore"
            PlayerPrefs.SetInt("skin",x); //sets new player skin
            for(int i = 0; i < skins.transform.childCount; i++) //loops "skins" children objects
            {
                GameObject a = skins.transform.GetChild(i).gameObject; //gets indexed object
                a.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(40, 40); //makes indexed obj smaller
                a.GetComponent<Button>().enabled=true; //makes indexed obj button enabled (makes it purchasable)
                a.transform.GetChild(0).gameObject.GetComponent<Text>().text="50"; //resets price to "50"
            }
            skins.transform.GetChild(x).gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50, 50); //makes selected obj bigger
            skins.transform.GetChild(x).gameObject.GetComponent<Button>().enabled=false; //makes selected obj button disabled
            skins.transform.GetChild(x).gameObject.transform.GetChild(0).GetComponent<Text>().text=""; //clears price
        }
    }
    public void setColor(string x)
    {   
        PlayerPrefs.SetString("color",x); //sets skin color for player cube (trigger on UI button press)
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

