using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PUN2_Chat : MonoBehaviourPun
{
    bool isChatting = false;
    string chatInput = "";
    [SerializeField] private TMP_InputField chatInputField;
    [SerializeField] private GameObject SMSIncoming;
    [SerializeField] private GameObject SMSOutgoing;
    [SerializeField] private GameObject chatWindowParent;
    [SerializeField] private ContactManager contactManager;
    public Transform chatWindow;

    [System.Serializable]
    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
    }

    List<ChatMessage> chatMessages = new List<ChatMessage>();
    [SerializeField] Button sendButton;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Photon View
        if (gameObject.GetComponent<PhotonView>() == null)
        {
            PhotonView photonView = gameObject.AddComponent<PhotonView>();
            photonView.ViewID = 1;
        }
        else
        {
            photonView.ViewID = 1;
        }
        sendButton.onClick.AddListener(SendMessage);
    }

    // Update is called once per frame
    public void OnChat()
    {
        if (!isChatting)
        {
            isChatting = true;
            chatInput = "";
        }

    }

    void Update()
    {
        if (isChatting)
        {
            chatInput = chatInputField.text;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage();

            }

        }

        //Show messages
        if (chatMessages.Count > 0)
        {
            for (int i = 0; i < chatMessages.Count; i++)
            {
                if (chatMessages[i].sender != PhotonNetwork.LocalPlayer.NickName)
                {
                    GameObject textBox = Instantiate(SMSIncoming, chatWindow);
                    textBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = chatMessages[i].message;
                    chatMessages.RemoveAt(i);
                }
                else
                {
                    GameObject textBox = Instantiate(SMSOutgoing, chatWindow);
                    textBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = chatMessages[i].message;
                    chatMessages.RemoveAt(i);
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(chatWindowParent.GetComponent<RectTransform>());
                StartCoroutine(ScrollToBottom());
            }
        }
    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        chatWindowParent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

    }

    void SendMessage()
    {

        if (chatInputField.text != "")
        {
            //isChatting = false;
            if (chatInput.Replace(" ", "") != "")
            {
                //Send message
                photonView.RPC("SendChat", RpcTarget.All, PhotonNetwork.LocalPlayer, chatInput);
                contactManager.currentContact.transform.GetChild(2).GetComponent<TMP_Text>().text = chatInput;
            }
            chatInput = "";
            chatInputField.Select();
            chatInputField.text = "";
        }
    }

    [PunRPC]
    void SendChat(Photon.Realtime.Player sender, string message, PhotonMessageInfo info)
    {
        ChatMessage m = new ChatMessage();
        m.sender = sender.NickName;
        m.message = message;

        chatMessages.Insert(0, m);

    }
}