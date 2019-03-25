using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorVisibility : MonoBehaviour
{
    void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<FirstPersonController>() != null)
        {
            Debug.Log("no cursor");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.Log("cursor");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
