using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _doorOpen;
    [SerializeField] private SO_LevelObjects _levelObjects;

    public bool DoorOpen { get { return _doorOpen; } set { _doorOpen = value; } }

    private void Awake()
    {
        _doorOpen = _levelObjects.doorOpen;
        SetState(_doorOpen);
    }

    public void OpenDoor()
    {
        Destroy(this.gameObject);
    }

    public void SetState(bool doorOpen)
    {
        if (doorOpen)
            OpenDoor();
    }

    public void SaveState()
    {
        _levelObjects.doorOpen = _doorOpen;
    }
}
