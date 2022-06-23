using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]

public class SO_GameSettings : ScriptableObject
{
    public Toggle coyoteToggle;
    public Toggle doubleJumpToggle;
    public Toggle wallSlideToggle;
    public Toggle wallJumpToggle;
    public Toggle jumpBufferToggle;
}
