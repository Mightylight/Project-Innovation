using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SpeechRecognizerPlugin;

public class MySpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{

    private SpeechRecognizerPlugin plugin = null;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextMeshProUGUI textMeshProUGUIError;
    public void OnError(string recognizedError)
    {
        textMeshProUGUIError.text = recognizedError;
    }

    public void OnResult(string recognizedResult)
    {
        textMeshProUGUI.text = recognizedResult;
    }

    // Start is called before the first frame update
    void Start()
    {
        plugin = new SpeechRecognizerPlugin_Editor(this.gameObject.name);
        StartVoice();
    }

    public void StartVoice()
    {
        plugin.SetContinuousListening(true);
        plugin.SetMaxResultsForNextRecognition(1);
        plugin.StartListening();
    }

}
