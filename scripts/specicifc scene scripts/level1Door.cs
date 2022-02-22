using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Door : MonoBehaviour
{
    public GameObject oldDoor;
    public GameObject newDoor;
    public dollScene_Bed dollBedScript;

    void Start()
    {
        newDoor.SetActive(false);
    }

    
    void Update()
    {
        if (dollBedScript.dollPlaced == true)
        {
            //anim for enemy & door opening
            newDoor.SetActive(true);
            oldDoor.SetActive(false);
        }
    }
}
