using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailManager : MonoBehaviour
{
    [SerializeField] TMP_Text messageSenderText;
    [SerializeField] TMP_Text messageSubjectText;
    [SerializeField] TMP_Text messageDateText;
    [SerializeField] TMP_Text messageContentText;

    [SerializeField] GameObject inboxList;
    [SerializeField] GameObject messageWindow;
    List<Button> inboxButtons = new List<Button>();

    private void Start()
    {
        GetInboxItems();
    }

    private void Update()
    {

    }

    public void GetInboxItems()
    {
        for (int i = 0; i < inboxList.transform.childCount; i++)
        {
            inboxButtons.Add(inboxList.transform.GetChild(i).gameObject.GetComponent<Button>());
            int tmp = i;
            inboxButtons[i].onClick.AddListener(() => PopulateMessageWindow(inboxButtons[tmp].gameObject));
        }
    }

    public void PopulateMessageWindow(GameObject gO)
    {

        messageDateText.text = gO.transform.Find("Date").GetComponent<TMP_Text>().text;
        messageSubjectText.text = gO.transform.Find("Subject").GetComponent<TMP_Text>().text;
        messageSenderText.text = "From: " + gO.transform.Find("Sender").GetComponent<TMP_Text>().text;

        switch (messageSenderText.text)
        {
            case "Boss":
                messageContentText.text = "You're Fired.";
                break;

            case "Work Bestie":
                messageContentText.text = "Yaaas slay";
                break;
        }

        inboxList.SetActive(false);
        messageWindow.SetActive(true);
    }
}
