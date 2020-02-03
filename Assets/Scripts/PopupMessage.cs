using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    public static GameObject popUpWindow;

    void Awake()
    {
        popUpWindow = GameObject.Find("PopUp Message");
        popUpWindow.SetActive(false);
    }

    public static IEnumerator setPopupText(string text)
    {
        popUpWindow.GetComponentInChildren<TextMeshProUGUI>().text = text;
        popUpWindow.SetActive(true);
        yield return new WaitForSeconds(3);
        popUpWindow.SetActive(false);
    }
}
