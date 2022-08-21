using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _doorOpen;
    [SerializeField] private GameObject _doorText;
    [SerializeField] private SO_LevelObjects _levelObjects;

    public bool DoorOpen { get { return _doorOpen; } set { _doorOpen = value; } }
    public GameObject DoorText { get { return _doorText; } }

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
