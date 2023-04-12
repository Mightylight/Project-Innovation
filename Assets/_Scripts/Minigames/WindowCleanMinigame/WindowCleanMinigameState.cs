using UnityEngine;

namespace _Scripts.Minigames.WindowCleanMinigame
{
    public class WindowCleanMinigameState : MinigameState
    {
        [SerializeField] private CleanableSurface _cleanableSurface;
        
        
        public override void OnStateEnter()
        {
            if (_cleanableSurface._hasBeenCleaned)//Skip this state if it has already been cleaned
            {
                MinigameFSM.Instance.NextState();
            }
        }

        public override void OnStateExit()
        {
           // throw new System.NotImplementedException();
        }
    }
}
