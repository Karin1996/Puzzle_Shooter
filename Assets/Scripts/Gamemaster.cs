using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas press;
    public Canvas iteminfo;
    private void Start()
    {
        //standard canvas false
        DisableCanvas(press);
        DisableCanvas(iteminfo);
    }

    public void EnableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = true;
    }

    public void DisableCanvas(Canvas canvasName)
    {
        canvasName.GetComponent<Canvas>().enabled = false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
