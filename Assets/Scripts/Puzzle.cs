using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameManager gm;
    float elapsed = 0f; 

    public GameObject red;
    Material redMaterial;
    public GameObject blue;
    Material blueMaterial;
    public GameObject green;

    void Start()
    {
        redMaterial = red.GetComponent<Renderer>().material;
        redMaterial.SetColor("_EmissionColor", new Color(0.1f, 0f, 0f));

        blueMaterial = blue.GetComponent<Renderer>().material;
        blueMaterial.SetColor("_EmissionColor", new Color(0f, 0f, 0.1f));
    }

    void Update()
    {
        if (gm.startPuzzle == true)
        {
            ShowSequence();
            gm.startPuzzle = false;
        }
    }

    void ShowSequence()
    {
        redMaterial = red.GetComponent<Renderer>().material;
        redMaterial.SetColor("_EmissionColor", new Color(0.9f, 0.2f, 0.2f));
        //wait
        //green
        //wait
        //red
        //wait
        blueMaterial = blue.GetComponent<Renderer>().material;
        blueMaterial.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.9f));
    }
}
