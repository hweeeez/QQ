using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogIn : MonoBehaviour
{
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] GameObject lockScreen;
    [SerializeField] GameObject bgObject;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (passwordInputField.text == "quest" || passwordInputField.text == "Quest")
            {
                StartCoroutine(Decrease());
            }
        }
    }

    public void CheckPassword()
    {
        if (passwordInputField.text == "quest" || passwordInputField.text == "Quest")
        {
            StartCoroutine(Decrease());
        }
    }

    IEnumerator Decrease()
    {
        while (bgObject.GetComponent<Image>().color.a > 137)
        {
            float alpha = bgObject.GetComponent<Image>().color.a;
            alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        lockScreen.gameObject.SetActive(false);
    }
}
