using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StaticEventHandler.CallGameWonEvent("Victory!", "You escaped this world!");
    }
}
