using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelObjects", menuName = "ScriptableObjects/LevelObjects", order = 1)]

public class SO_LevelObjects : ScriptableObject
{
    [Header("Player")]
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int health;
    public int diamondCount;

    [Header("Level")]
    public bool leverPulled;
    public bool doorOpen;
    public List<string> collectedDiamonds;
    public Diamond[] maxCountDiamonds;

    [Header("Scenes")]
    public string[] activeScenes;
}
