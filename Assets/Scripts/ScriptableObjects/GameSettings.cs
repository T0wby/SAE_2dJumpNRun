using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 0)]

public class SaveGame : ScriptableObject
{
    public Toggle _coyoteToggle;
    public Toggle _doubleJumpToggle;
    public Toggle _wallSlideToggle;
}
