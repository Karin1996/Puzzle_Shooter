using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public bool start = false;
    private TextMeshPro time; //The time gameobject 
    float elapsed = 0f; // will go from 0 to 1 second
    private AudioSource alarm;

    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        minutes = 1;
        seconds = 05;

        time = gameObject.GetComponent<TextMeshPro>();
        alarm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime; //The elapsed time
        if (elapsed >= 1f && start == true)
        {
            elapsed = elapsed % 1f; //back to zero
            int status = Timer(); //execute Timer()
            if (status == -1)
            {
                // Timer is done.
                // Do shit here.
                time.text = "Done";
            }
        }
    }

    int Timer()
    {
        if (minutes == 0 && seconds == 0)
        {
            return -1;
        }

        if (minutes == 0 && seconds <= 30)
        {
            time.color = new Color32(255, 0, 0, 255); //Change the color
            alarm.Play();
        }

        if (seconds == 0)
        {
            minutes -= 1;
            seconds = 59;
        }
        time.text = minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
        seconds -= 1;
        return 1;
    }
}
