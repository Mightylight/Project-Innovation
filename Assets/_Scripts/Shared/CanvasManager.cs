using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] GameObject phoneConsole;

    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CanvasManager is null!");
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        PhoneConsoleMessage("Console is on!");
    }


    /// <summary>
    /// Send a message to the console, it will appear in the topright.
    /// Keep in mind, this is for debugging only and eats performance :D
    /// </summary>
    /// <param name="messageToPrint">Message to be displayed</param>
    public void PhoneConsoleMessage(string messageToPrint)
    {
        GameObject display = new GameObject($"PhoneConsoleMessage: {messageToPrint}");

        display.transform.parent = phoneConsole.transform;

        display.AddComponent<RectTransform>();
        display.AddComponent<TextMeshProUGUI>();
        display.GetComponent<TextMeshProUGUI>().text = messageToPrint;

    }


}
