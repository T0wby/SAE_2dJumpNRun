using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelObjects", menuName = "ScriptableObjects/LevelObjects", order = 1)]

public class SO_LevelObjects : ScriptableObject
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int health;
    public int diamondCount;
    public string[] activeScenes;
}
