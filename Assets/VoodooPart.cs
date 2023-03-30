using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooPart : MonoBehaviour
{
    [SerializeField] VoodooPartEnum part;
    [SerializeField] bool pinIsIn = false;

    private void OnTriggerEnter(Collider other)
    {
        VoodooPin pin = other.GetComponent<VoodooPin>();
        if (pin == null) return;
        pin.transform.parent = transform;
        pin.ToggleKinematicRigid(true);//Enable kinematic on rigid
        if(pin.pinPart == part) pinIsIn= true;//The correct pin is in!
    }
    private void OnTriggerExit(Collider other)
    {
        VoodooPin pin = other.GetComponent<VoodooPin>();
        if (pin == null) return;
        pin.transform.parent = null;
        pin.ToggleKinematicRigid(false);//Disable kinematic on rigid
        if (pin.pinPart == part) pinIsIn = false;//The correct pin left!
    }

}
public enum VoodooPartEnum {HEAD,TORSO,LEFT_ARM,RIGHT_ARM,LEFT_LEG,RIGHT_LEG};
