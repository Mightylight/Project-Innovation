using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGhostInsideMinigameState : MinigameState
{
    [SerializeField] private WallSlider _wallSlider;
    public override void OnStateEnter()
    {
        _wallSlider.SlideWall();
    }

    public override void OnStateExit()
    {
        // throw new System.NotImplementedException();
    }
}
