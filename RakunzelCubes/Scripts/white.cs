//GAME/PLAYER MECHANICS HERE!!!
/*
The codes here include showing obstacles in front of the player, moving the player left and right, and the actions to be taken when player touches the obstacles.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //required: UI elements such as text, panel etc...
public class white : MonoBehaviour
{   
    public Text score; // UI Text: score on top
    public GameObject camareObj; //camera (main camera)
    public GameObject block; //Prefab: blocks3
    public GameObject block5; //Prefab: blocks5
    private float time = 1; //float: waves time handler
    //maxY and maxX set the boundaries of player(white/cube). 3 grids in easy (x:1-2-3), 5 grids in hard (x:0-1-2-3-4)
    private int maxY=1; //int: max -X pos for player
    private int maxX=3; //int: max +X pos for player
    private int scor=1; //int: multiplier for score
    private float x=2; //int: player starting pos
    void Start()
    {        
        StartCoroutine(waves()); //starts waves
        if(PlayerPrefs.GetInt("difficulty")==1){ //runs if difficulty sets to 1
            maxY=0; //sets new max -X 
            maxX=4; //sets new max +X 
            block=block5; //sets new (bigger) blocks
            scor=3; //3x multiplier for score
        }
        SpriteRenderer spr = GetComponent<SpriteRenderer>(); //gets sprite renderer for player cube
        Color newCol;
        if (ColorUtility.TryParseHtmlString("#"+PlayerPrefs.GetString("color"), out newCol)) //gets color from player prefs and assings it to newCol
        {
            spr.color = newCol; //sets playerPref color for the player cube
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.tag == "red") //if touches any collider named red:
        {
            gameOver(); //game over function
        }
        if (col.tag == "block") //if touches any collider named block:
        {
            int a=int.Parse(score.text); //gets UI text "score.text" and converts integer "a"
            a+=scor; //adds "score" to "a";
            score.text=a.ToString(); //converts int "a" to string and sets UI text "score.text"
            camareObj.GetComponent<cameraColor>().RandomChords(); //plays random chord
        }
    }
    public void moveRight(){
        //x++
        x=this.transform.position.x; //gets X position of player
        if(x!=maxX){ //if not maximum value:
            this.transform.position=new Vector3(x+1,this.transform.position.y,this.transform.position.z); //adds 1 to position.x
        }
    }
    public void moveLeft(){
        //x--
        x=this.transform.position.x; //gets X position of player
        if(x!=maxY){ //if not minimum value:
            this.transform.position =new Vector3(x-1,this.transform.position.y,this.transform.position.z); //removes 1 from position.x
        }
    }  
    private void spawnEnemy(){
        GameObject a = Instantiate(block) as GameObject; //calls prefab as gameobject
        a.GetComponent<blocks>().speed=Random.Range(6,8); //sets prefab fall speed range from 6-8
        a.transform.position = new Vector3(0, 10, 0); //sets prefab position x:0 y:10 z:0. Spawns prefab at the top of the scene. 
    }
    private void gameOver(){
        int a=int.Parse(score.text); //gets UI text "score.text" and converts integer "a"
        PlayerPrefs.SetInt("totalScore",PlayerPrefs.GetInt("totalScore")+a); //sets total score of player by adding gamescore to previous (playerpref.totalScore) score
        if(PlayerPrefs.GetInt("maxScore")<a){ //checks highscore
            PlayerPrefs.SetInt("maxScore", a); //sets highscore
        }        
        Destroy(gameObject); //destroys player
        score.text="Game Over"; //sets score text to "Game Over"
        camareObj.GetComponent<cameraColor>().gameOver(); //calls camareObj gameOver function
    }
    IEnumerator waves(){
        //spawns enemies
        while(true){
            yield return new WaitForSeconds(time);
            spawnEnemy();
        }
    }
}
