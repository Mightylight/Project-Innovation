using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Minigames
{
    public class MinigameFSM : MonoBehaviour
    {
        [SerializeField] private MinigameState _startState;
        [SerializeField] private List<MinigameState> _minigameStates;
        [SerializeField] private GameObject _glowObject;
        [SerializeField] private float _hintTimeInSeconds;
    
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
            _glowObject.transform.position = _currentState.hintPosition.transform.position;
            _glowObject.SetActive(true);
            StartCoroutine(GlowObjectTimer());
        }
    
        private IEnumerator GlowObjectTimer()
        {
            yield return new WaitForSeconds(_hintTimeInSeconds);
            _glowObject.SetActive(false);
        }
    }
}
