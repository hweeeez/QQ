using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContactManager : MonoBehaviour
{
    List<GameObject> chatWindows = new List<GameObject>();
    List<Button> contactButtons = new List<Button>();
    [SerializeField] private GameObject contactListParent;
    [SerializeField] private GameObject chatWindowViewport;
    [SerializeField] private GameObject chatWindowParent;
    [SerializeField] private GameObject passwordWindow;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_Text chatWindowHeader;
    [SerializeField] private PUN2_Chat chatManager;
    public GameObject currentContact;
    GameObject currentButton;
    GameObject currentLock;
    bool unlocked = false;
    private void Start()
    {
        GetContactButtons();
        GetChatWindows();
    }

    public void GetContactButtons()
    {
        for (int i = 0; i < contactListParent.transform.childCount; i++)
        {
            contactButtons.Add(contactListParent.transform.GetChild(i).GetComponent<Button>());
        }
        foreach (var button in contactButtons)
        {
            string[] name = button.gameObject.name.Split("ContactItem");

            button.onClick.AddListener(() => SwitchChatWindows(name[0], button.gameObject));


        }
    }

    public void GetChatWindows()
    {
        for (int i = 0; i < chatWindowViewport.transform.childCount; i++)
        {
            chatWindows.Add(chatWindowViewport.transform.GetChild(i).gameObject);
        }
    }

    public void SwitchChatWindows(string buttonName, GameObject button)
    {
        if (button.transform.GetChild(0).name != "Lock")
        {

            for (int i = 0; i < chatWindows.Count; i++)
            {
                if (!chatWindows[i].name.Contains(buttonName))
                    chatWindows[i].SetActive(false);
                else
                {
                    chatWindows[i].SetActive(true);
                    chatManager.chatWindow = chatWindows[i].transform;
                    chatWindowParent.GetComponent<ScrollRect>().content = chatWindows[i].GetComponent<RectTransform>();
                }
            }
            currentContact = button;
            chatWindowHeader.text = buttonName;
        }
        else
        {
            passwordWindow.SetActive(true);
            currentLock = button.transform.GetChild(0).gameObject;
            currentButton = button;
        }


    }

    IEnumerator UnlockCoroutine()
    {
        Destroy(currentLock);
        yield return new WaitForEndOfFrame();
        passwordWindow.SetActive(false);
        SwitchChatWindows(currentButton.name.Split("ContactItem")[0], currentButton);
    }

    public void UnlockChat()
    {
        if (passwordInputField.text == "pw")
        {

            passwordInputField.text = "";
            StartCoroutine(UnlockCoroutine());

        }
        else
        {
            passwordInputField.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnlockChat();
        }
    }
}
