using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public Canvas hitbox;
    public Canvas press;
    public Canvas iteminfo;
    public Canvas dialogueBox;

    public TextMeshProUGUI dialogueText;
    private Queue sentences;
    private Dialogue dialogue;
    private bool dialogueFinished;

    public bool startTimer;
    public bool startPuzzle;

    private void Start()
    {
        //diable all movement of player until dialogue is finished
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;

        //standard canvas false
        DisableCanvas(press);
        DisableCanvas(iteminfo);
        DisableCanvas(hitbox);

        sentences = new Queue();
        dialogue = new Dialogue();
        dialogueFinished = false;
        StartDialogue(dialogue);
        startTimer = false;
        startPuzzle = false;
    }

    private void Update()
    {
        //make sure you cant see the other dialogues when looking around
        if (!dialogueFinished)
        {
            DisableCanvas(press);
            DisableCanvas(iteminfo);
        }
        //only display nextsentence if the dialogue bool dialogueFinished = false
        if (!dialogueFinished && Input.GetKeyUp(KeyCode.Return))
        {
           DisplayNextSentence();
        }
    }

    public void EnableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = true;
    }

    public void DisableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting");
        sentences.Clear(); //clear sentences that might have still been there

        foreach(string sentence in dialogue.sentences) //for all sentences from dialogue do something
        {
            sentences.Enqueue(sentence); //add them to our queue
        }
        //display sentence
        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
        //if there are no more sentences
        if(sentences.Count == 0)
        {
            EndDialogue();
            StartTimer();
            return;
        }

        //there are still sentences, get them from the queue
        string sentence = (string) sentences.Dequeue();
        dialogueText.text = sentence;
    }
    void EndDialogue()
    {
        //Dialogue is done, player may move again
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;

        DisableCanvas(dialogueBox);
        EnableCanvas(hitbox);
        dialogueFinished = true;
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
