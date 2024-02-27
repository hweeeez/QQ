using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent doubleClick;
    public UnityEvent singleClick;


    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 1)
            OnSingleClick();
        else if (clickCount == 2)
            OnDoubleClick();
        else if (clickCount > 2)
            OnMultiClick();
    }

    void OnSingleClick()
    {
        Debug.Log("Single Clicked");
        singleClick.Invoke();
    }

    void OnDoubleClick()
    {
        Debug.Log("Double Clicked");
        doubleClick.Invoke();
    }

    void OnMultiClick()
    {
        Debug.Log("MultiClick Clicked");
    }
}
