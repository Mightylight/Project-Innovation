using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class VynilPlate
{
    public AudioClip music;
   [SerializeField] public VynilPlateName vinylPlateName;
}

[System.Serializable]  public enum VynilPlateName { AMONGUS, L_CASINO, THE_CORRECT_SONG, DARK_MYSTERY, HOUSE_WITH_GHOST, INSIDE_SERIAL_KILLER, LID, SPELLCRAFT}