using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public float range = 1f;
    public Camera fpsCam;
    public GameManager gm;
    private TextMeshProUGUI pressItemName;
    private TextMeshProUGUI iteminfoItemName;
    private TextMeshProUGUI iteminfoItemDesc;
    private string itemName;
    private TextMeshProUGUI desc;
    private string itemDesc;

    void Update()
    {
        RaycastHit Item;
        // we want to look forward starting from the camera and we want info in the Item variable.The range is optional
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Item, range) && Item.transform.tag == "item")//Not execute when accidently Looking at yourself (looking down for example)
        {
            //Get the name of the item player is looking at
            itemName = Item.transform.name;
            //Get the textmesh component that is on the item player is looking at
            desc = Item.transform.Find("Desc").GetComponent<TextMeshProUGUI>();
            //Get the text that is in the textmesh text
            itemDesc = desc.text;

            //Get the press canvas textmeshcomponent with the name ItemName
            pressItemName = gm.press.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            //change the name of the canvas press itemname textmesh component to the item name the player is looking at
            pressItemName.text = itemName;
            //Show the canvas press
            gm.EnableCanvas(gm.press);

            //When player presses e show iteminfo canvas with the name and description of the item the player is looking at
            if (Input.GetKey(KeyCode.E))
            {
                gm.DisableCanvas(gm.press);
                gm.EnableCanvas(gm.iteminfo);

                iteminfoItemName = gm.iteminfo.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                iteminfoItemDesc = gm.iteminfo.transform.Find("Description").GetComponent<TextMeshProUGUI>();
                iteminfoItemName.text = itemName;
                iteminfoItemDesc.text = itemDesc;

            }
            else
            {
                gm.DisableCanvas(gm.iteminfo);
            }
        }
        else
        {
            gm.DisableCanvas(gm.press);
            gm.DisableCanvas(gm.iteminfo);
        }
    }
}
