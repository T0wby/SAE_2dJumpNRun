using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _doorOpen;

    public bool DoorOpen { get { return _doorOpen; } set { _doorOpen = value; } }


    public void OpenDoor()
    {
        this.gameObject.SetActive(false);
    }
}
