using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SO_LevelObjects _levelObjects;

    public void SavePlayerPos()
    {
        _levelObjects.playerPosition = transform.position;
        _levelObjects.playerRotation = transform.rotation;
    }
}
