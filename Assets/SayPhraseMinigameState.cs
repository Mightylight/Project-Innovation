using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayPhraseMinigameState : MinigameState
{
    [SerializeField]UnitySpeechRecognizer speechRecognizer;
    public override void OnStateEnter()
    {
        //Enable speechrecognizer to listen for keyword.
        //Enable text on the wall for vr 
        //Enable text on the wall for phone
        speechRecognizer.enabled = true;
    }

    public override void OnStateExit()
    {
        speechRecognizer.enabled = false;
    }
}
