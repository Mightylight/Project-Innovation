using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayPhraseMinigameState : MinigameState
{
    #if !UNITY_ANDROID
    [SerializeField]UnitySpeechRecognizer speechRecognizer;

    public override void OnStateEnter()
    {
        speechRecognizer = GetComponent<UnitySpeechRecognizer>();
        speechRecognizer.enabled = true;
    }

    public override void OnStateExit()
    {
        speechRecognizer.enabled = false;
    }
    #else
    public override void OnStateEnter()
    {
        Debug.LogError("Change the build of the server to windows!");
    }

    public override void OnStateExit()
    {
        Debug.LogError("Change the build of the server to windows!");
    }
#endif
}
