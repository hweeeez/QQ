using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    // Start is called before the first frame update
    public void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }

    public void OpenWindow(GameObject go)
    {
        go.SetActive(true);
    }

    public void RemoveIcon(GameObject go)
    {
        go.SetActive(false);
    }

    public void AddIcon(GameObject go)
    {
        go.SetActive(true);
    }
}
