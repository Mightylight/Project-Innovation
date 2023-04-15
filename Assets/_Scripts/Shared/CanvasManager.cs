using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] GameObject phoneConsole;
    public Button fixButton;
    public Button toggleGyroButton;
    public Button startGyroButton;

    [SerializeField] Button clueButton;
    [SerializeField] TextMeshProUGUI clueButtonText;

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

    public void SetClueCountdown(float countdown)
    {
        clueButton.interactable = false;
        StartCoroutine(ClueCountDown(countdown));
    }

    public void OnGetClueButton()
    {
        NerworkProtocolManager.Instance.RequestClueServerRpc();
    }

    IEnumerator ClueCountDown(float time)
    {

        while(time > 0)
        {
            time--;
            clueButtonText.text = $"{time}";
           yield return new WaitForSeconds(1f);
        }
        clueButton.interactable = true;
        clueButtonText.text = "Get Clue";
    }
}
