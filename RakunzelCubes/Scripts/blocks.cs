//RED ENEMY BLOCKS
/*
The codes here allow enemy objects to move and perform actions on their chilren objects.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class blocks : MonoBehaviour
{
    public float speed =1; //float: sets speed of cubes
    void Start()
    {
        GameObject go=gameObject.transform.GetChild(Random.Range(0, gameObject.transform.childCount)).gameObject; //selects one of the childs
        go.SetActive(false); //makes selected child object inactive
    }
    void Update()
    {
        float step = speed * Time.deltaTime; //sets movement
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,-11,0), step); //makes red blocks to move 
        if(gameObject.transform.position.y<-10){
            Destroy(gameObject); //removes gameobjects after they move beyond position.y 10
        }        
    }
}
