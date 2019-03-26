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
        blueMaterial = blue.GetComponent<Renderer>().material;
        greenMaterial = green.GetComponent<Renderer>().material;

        // Reset the lanterns
        ResetLanterns();

        //Generate a random sequence for the colors to show up
        MakeSequence(5);
        printSequence(sequence);
    }

    void printSequence(List<string> sequence) { 
        foreach (string color in sequence)
        {
            Debug.Log(color);
        }
    }

    void MakeSequence(int amount)
    {
        //we want a sequence of 5 colors 
        for (int i = 0; i < amount; i++)
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
                    if (Input.GetKeyUp(KeyCode.Q) && shownSequence == false)
                    {
                        gm.DisableCanvas(gm.puzzlePress);
                        shownSequence = true;
                        StartCoroutine(ShowSequenceAsync(sequence, 0));
                    }
                }
                else
                {
                    gm.DisableCanvas(gm.puzzlePress);
                }
            }
        }
    }

    void ResetLanterns()
    {
        //first set all the colors to default material colors everytime
        redMaterial.SetColor("_EmissionColor", new Color(0.1f, 0f, 0f));
        blueMaterial.SetColor("_EmissionColor", new Color(0f, 0f, 0.1f));
        greenMaterial.SetColor("_EmissionColor", new Color(0f, 0.1f, 0f));
    }

    IEnumerator ShowSequenceAsync (List<string> sequence, int position)
    {
        //Check what the colorname is of the current loop and then change color
        switch (sequence[position])
        {
            case "red":
                redMaterial.SetColor("_EmissionColor", new Color(0.9f, 0.2f, 0.2f));
                break;
            case "blue":
                blueMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.9f));
                break;
            case "green":
                greenMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.9f, 0.2f));
                break;
        }

        yield return new WaitForSeconds(1);
        ResetLanterns();
        yield return new WaitForSeconds(0.5f);

        if ((position+1) != sequence.Count)
        {
            StartCoroutine(ShowSequenceAsync(sequence, ++position));
        }
    }
}
