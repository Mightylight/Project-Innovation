using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSocketManager : MonoBehaviour
{

    bool firstTime = true;


    public void OnGhostEnter()
    {
        if (firstTime)
        {
            MinigameFSM.Instance.NextState();
            firstTime = false;
        }
        //TODO: make text appear
        //Suggestion:: Add a communicative puzzle where the ghost can see the alphabet next to some symbols and the VR player sees symbols.
        //NEEDS MORE TEAMWORK IN THE GAME?
        //To progress to next state,
    }
    
    public void OnGhostExit()
    {
        //TODO: Make text dissapear
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
