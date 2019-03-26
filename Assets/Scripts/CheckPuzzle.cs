using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckPuzzle : MonoBehaviour
{
    public GameObject puzzle;
    Puzzle puzzleScript;
    public Image[] canvasBlocks;

    int colorPlace = 0;

    List<string> playerSequence = new List<string> { };

    void Start()
    {
        //get the puzzle script
        puzzleScript = puzzle.GetComponent<Puzzle>();
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
                    canvasBlocks[colorPlace].color = new Color32(229, 85, 85, 200);
                    break;
                case "blue":
                    canvasBlocks[colorPlace].color = new Color32(40, 176, 255, 200);
                    break;
                case "green":
                    canvasBlocks[colorPlace].color = new Color32(94, 192, 103, 200);
                    break;
            }
            colorPlace++;
        }

        if (playerSequence.Count == 5)
        {
            bool equal = CompareLists(puzzleScript.sequence, playerSequence);
            if (equal)
            {
                FindObjectOfType<GameManager>().GameWon();
            }
            else
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }

    }

    bool CompareLists (List<string> a, List<string> b)
    {
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }
}
