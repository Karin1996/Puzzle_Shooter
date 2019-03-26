using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameManager gm;
    public Camera fpsCam;
    //float elapsed = 0f; 

    public GameObject red;
    Material redMaterial;
    public GameObject blue;
    Material blueMaterial;
    public GameObject green;
    Material greenMaterial;

    string[] colors = new string[] { "red", "blue", "green" };
    public List<string> sequence = new List<string> { };

    bool shownSequence = false;

    void Start()
    {
        //default material colors
        redMaterial = red.GetComponent<Renderer>().material;
        redMaterial.SetColor("_EmissionColor", new Color(0.1f, 0f, 0f));

        blueMaterial = blue.GetComponent<Renderer>().material;
        blueMaterial.SetColor("_EmissionColor", new Color(0f, 0f, 0.1f));

        greenMaterial = green.GetComponent<Renderer>().material;
        redMaterial.SetColor("_EmissionColor", new Color(0f, 0.1f, 0f));
        //Generate a random sequence for the colors to show up
        MakeSequence();
    }

    void MakeSequence()
    {
        //we want a sequence of 5 colors 
        for (int i = 0; i < 5; i++)
        {
            //put random color from colors array in sequence list
            sequence.Add(colors[Random.Range(0, 3)]);
        }
    }

    private void Update()
    {
        if(gm.startPuzzle == true)
        {
            RaycastHit Item;
            // we want to look forward starting from the camera and we want to enable the canvas to press q to start sequence.
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Item) && Item.transform.tag == "puzzle")//Not execute when accidently Looking at yourself (looking down for example)
            {
                //Make sure you can only see the sequence once
                if (shownSequence == false)
                {
                    gm.EnableCanvas(gm.puzzlePress);
                    //extra check
                    if (Input.GetKeyUp(KeyCode.Q) && shownSequence == false)
                    {
                        Debug.Log("Showing sequence");
                        ShowSequence();
                        gm.DisableCanvas(gm.puzzlePress);
                        shownSequence = true;
                    }
                }
                else
                {
                    gm.DisableCanvas(gm.puzzlePress);
                }
            }
        }
    }

    void ShowSequence()
    {
        foreach (string sequenceColor in sequence)
        {
            //first set all the colors to default material colors everytime
            redMaterial = red.GetComponent<Renderer>().material;
            redMaterial.SetColor("_EmissionColor", new Color(0.1f, 0f, 0f));

            blueMaterial = blue.GetComponent<Renderer>().material;
            blueMaterial.SetColor("_EmissionColor", new Color(0f, 0f, 0.1f));

            greenMaterial = green.GetComponent<Renderer>().material;
            redMaterial.SetColor("_EmissionColor", new Color(0f, 0.1f, 0f));

            //Check what the colorname is of the current loop and then change color
            switch (sequenceColor)
            {
                case "red":
                    redMaterial = red.GetComponent<Renderer>().material;
                    redMaterial.SetColor("_EmissionColor", new Color(0.9f, 0.2f, 0.2f));
                    break;
                case "blue":
                    blueMaterial = blue.GetComponent<Renderer>().material;
                    blueMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.9f));
                    break;
                case "green":
                    greenMaterial = green.GetComponent<Renderer>().material;
                    greenMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.9f, 0.2f));
                    break;
            }
            //wait a few and start loop again
        }
    }
    
}
