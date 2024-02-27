using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject startWindow;
    [SerializeField] GameObject photoWindow;

    public void ToggleWindow(GameObject gO)
    {
        if (gO.activeInHierarchy)
        {
            gO.SetActive(false);
        }
        else
        {
            gO.SetActive(true);
        }
    }
}

