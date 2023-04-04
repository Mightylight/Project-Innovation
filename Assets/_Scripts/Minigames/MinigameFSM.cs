using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class MinigameFSM : MonoBehaviour
{
    [SerializeField] private MinigameState _startState;
    [SerializeField] private List<MinigameState> _minigameStates;
    [SerializeField] private GameObject hintGlowPrefab;
    [SerializeField] private float _hintTimeInSeconds;

    // [SerializeField] private HintGlowManager 

    private static MinigameFSM _instance;
    public static MinigameFSM Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MinigameFSM is null");
            }

            return _instance;
        }
    }

    private MinigameState _currentState;

    public MinigameState CurrentState
    {
        get => _currentState;
        private set => _currentState = value;
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        _minigameStates = GetComponentsInChildren<MinigameState>().ToList();
        NextState();
    }

    public void NextState()
    {
        if (_currentState == null)
        {
            ChangeState(_startState);
        }
        int index = _minigameStates.IndexOf(_currentState);
        if (index < _minigameStates.Count - 1)
        {
            ChangeState(_minigameStates[index + 1]);
        }
        else
        {
            Debug.Log("No more states");
        }
    }

    private void ChangeState(MinigameState pMinigameState)
    {
        //exit current state if we have one
        if (_currentState != null)
        {
            _currentState.OnStateExit();
            _currentState = null;
        }

        //enter next state if we can find ot
        if (pMinigameState != null)
        {
            _currentState = pMinigameState;
            _currentState.OnStateEnter();
        }
    }

    public void GetClue()
    {
        if (true)//TODO:: if cooldown is done
        {
            GameObject obj = Instantiate(hintGlowPrefab);
            obj.transform.position = _currentState.hintPosition.transform.position;
            obj.GetComponent<NetworkObject>().Spawn();
            //TODO:: Start cooldown
            // NetworkProtocolManager.Instance.SetClueCooldownClientRpc(newcountdown);
            //
        }
        else
        {
            NerworkProtocolManager.Instance.ClueNotReadyClientRpc();
        }
    }

    //TODO: Cooldown needs to increase each time it gets requested? As in twice as long the second or something?
    //TODO: Cooldown Enumerator


}

