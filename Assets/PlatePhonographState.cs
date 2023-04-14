using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatePhonographState : MinigameState
{
    [SerializeField] WallSlider wallToSlide;
    public override void OnStateEnter()
    {
        hintObjects = GameObject.FindGameObjectsWithTag("Plate").ToList<GameObject>();//Oh god this is awfull, but I do it only once...
        wallToSlide.SlideWall();
    }

    public override void OnStateExit()
    {

    }
}
