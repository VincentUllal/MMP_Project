using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScene1 : MonoBehaviour //must be uniqe till i find a way to Serialize an array
{

    [SerializeField] private GameObject TextBox1, TextBox2, TextBox3; // Add however many you need.
    [SerializeField] private GameObject SceneCanvas, SceneRoot;

    private GameObject[] BoxArray;
    private int nr = -1; //would want to make this static inside nextbox. bs c#
    private void Start()
    {
        BoxArray = new GameObject[] { TextBox1, TextBox2, TextBox3 }; // copy the string from above into here.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneCanvas.SetActive(true);
        nextBox();
    }

    private void Update()
    {
        if (SceneCanvas.activeSelf && Input.GetKeyDown(KeyCode.Return))
            buttonPress();
    }

    public void buttonPress()
    {
        if (!nextBox())
            SceneRoot.SetActive(false);
    }

    bool nextBox () // flip to next textbox.
    {

        if (nr >= BoxArray.Length) // end reached, stop Scene
            return false;

        for(int i = 0; i < BoxArray.Length; i++)
        {
            if(i == nr)
            {
                BoxArray[i].SetActive(true); 
            }
            else
            {
                BoxArray[i].SetActive(false);
            }
        }
        nr++;
        return true;
    }
}
