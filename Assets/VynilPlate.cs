using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class VynilPlate
{
    public AudioClip music;
   [SerializeField] public VynilPlateName vinylPlateName;
}

[System.Serializable]  public enum VynilPlateName { AMONGUS, L_CASINO, }