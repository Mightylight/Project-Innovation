using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCanvasFSM : MonoBehaviour
{
    [SerializeField] List<MobileCanvasState> states;
    Dictionary<MobileState, MobileCanvasState> statesDic;//
    [SerializeField] MobileCanvasState currentState;


    private void Awake()
    {

        statesDic = new();
        //State state the new state states :D
        foreach (MobileCanvasState state in states)
        {
            statesDic.Add(state.state,state);
        }
    }

    public void LoadState(MobileState mobileState)
    {
        currentState.OnStateExit();
        currentState = statesDic[mobileState];
        currentState.OnStateEnter();
    }

    public void LoadState(MobileCanvasState stateToLoad)
    {
        LoadState(stateToLoad.state);
    }
}
