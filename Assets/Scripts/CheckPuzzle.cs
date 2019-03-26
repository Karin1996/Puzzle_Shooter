using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckPuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public Image[] canvasBlocks;

    int colorPlace = 0;

    List<string> playerSequence = new List<string> { };

    void Start()
    {
        //get the puzzle script
        Puzzle puzzleScript = puzzle.GetComponent<Puzzle>();
    }

    void Update()
    {
      //read what block are hit by player (dont let them destroy)
      //Save in playerSequence List (the first 5 shots)
      //Compare playerSequence List with puzzle.sequence (the correct one)
      //Win or lose
    }


    public void SaveColor(string colorname)
    {
        //if the playersequence doesn't have 5 color yet. Add the color 
        if(playerSequence.Count < 5)
        {
            //Make all the letters to lowercase. So Blue becomes blue
            colorname = colorname.ToLower();
            //add the shot color to colorname list
            playerSequence.Add(colorname);

            //update the color on order canvas blocks
            switch (colorname)
            {
                case "red":
                    canvasBlocks[colorPlace].color = new Color32(229, 85, 85, 100);
                    break;
                case "blue":
                    canvasBlocks[colorPlace].color = new Color32(40, 176, 255, 100);
                    break;
                case "green":
                    canvasBlocks[colorPlace].color = new Color32(94, 192, 103, 100);
                    break;
            }

            /*foreach (string seq in playerSequence)
            {
                Debug.Log(seq);
            }
            Debug.Log("--------------------");*/
            //the name of the canvas ++
            colorPlace++;
        }
        //sequence is full. Execute compare function
        else
        {
            Debug.Log("list full");
            //CompareLists();
        }
    }
}
