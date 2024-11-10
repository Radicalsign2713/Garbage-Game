using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProperty
{
    [Range(1, 10)]
    public float maxSpeed = 4;
    [Range(10, 100)]
    public float acelleration = 60;
    [Range(10, 100)]
    public float friction = 45;
}

[CreateAssetMenu(menuName = "CustomConfig/GameConfig")]
public class GameConfig : ScriptableObject
{
    public List<PlayerProperty> playerProperties = new List<PlayerProperty>();
}
