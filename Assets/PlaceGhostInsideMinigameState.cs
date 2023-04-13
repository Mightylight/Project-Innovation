using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGhostInsideMinigameState : MinigameState
{
    private WallSlider _wallSlider;
    public override void OnStateEnter()
    {
        _wallSlider.SlideWall();
    }

    public override void OnStateExit()
    {
        //TO ENTER NEXT STATE, SAY PHRASE
        // throw new System.NotImplementedException();
    }
}
