using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    public bool visible;
    private Vector3 myPosition;
    public GameObject myCharacter;
    private float timer;
    private float blocked = 0.77f;
    void Start()
    {
        myPosition = transform.position;    //save the position of the DisappearingBlock
        if (visible)
            {
            gameObject.transform.SetPositionAndRotation(new Vector3(1000, 1000, 1000), new Quaternion());   //just tp it far away lol
            //Debug.Log("bye");
            visible = !visible;
            }else{
                transform.position = myPosition;    //set the position to the saved Position
                visible = !visible;
                //Debug.Log("hey");
            }
        myCharacter = GameObject.Find("Char1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(myCharacter.GetComponent<Rigidbody2D>().velocity.y) < 0.1f){
            //Debug.Log("Ground");
        } else {
            //Debug.Log("Air");
        }
        //Debug.Log(Mathf.Abs(myCharacter.GetComponent<Rigidbody2D>().velocity.y));
        TogglePlatform();
        timer += Time.deltaTime;
    }

    void TogglePlatform(){
        if(Input.GetButtonDown("Jump")){
            //&& Mathf.Abs(myCharacter.GetComponent<Rigidbody2D>().velocity.y) < 0.1f){
            swap();
        }
    }
    void swap(){
        if (timer > blocked){
            if (visible){
                gameObject.transform.SetPositionAndRotation(new Vector3(1000, 1000, 1000), new Quaternion());   //just tp it far away lol
                //Debug.Log("bye");
                timer = 0;
                visible = !visible;
            } else{
                transform.position = myPosition;    //set the position to the saved Position
                timer = 0;
                visible = !visible;
                //Debug.Log("hey");
            }
        }
    }
}
