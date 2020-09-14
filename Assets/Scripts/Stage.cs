using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Platform
{
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathPartCount = 1;

    [Range(0, 2)]
    public int verticalParts = 0;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject
{
    public Color helixCylinderColor = Color.white;
    public Color stageBackgroundColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;
    public Color deathPartColor = Color.white;
    public Boolean isBonus = false;
    public List<Platform> Platforms = new List<Platform>();
}
