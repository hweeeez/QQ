using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGallery : MonoBehaviour
{
    private List<Sprite> photos = new List<Sprite>();
    [SerializeField]
    private Image photoOnDisplay;


    // Start is called before the first frame update
    void Start()
    {
        Sprite[] loadPhotos = Resources.LoadAll<Sprite>("PhotoGallery");
        foreach (var photo in loadPhotos)
        {
            photos.Add(photo);

        }

        photoOnDisplay.sprite = photos[0];
    }

    public void OnLeftButtonPressed()
    {
        if (photos.IndexOf(photoOnDisplay.sprite) == 0)
        {
            photoOnDisplay.sprite = photos[photos.Count - 1];
        }

        else
        {
            photoOnDisplay.sprite = photos[photos.IndexOf(photoOnDisplay.sprite) - 1];
        }
        //Debug.Log(photos[photos.IndexOf(photoOnDisplay.sprite)].name);
    }

    public void OnRightButtonPressed()
    {

        if (photos.IndexOf(photoOnDisplay.sprite) == photos.Count - 1)
        {
            photoOnDisplay.sprite = photos[0];
        }
        else
        {
            photoOnDisplay.sprite = photos[photos.IndexOf(photoOnDisplay.sprite) + 1];
        }
    }
}
