using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject // Can be separated into different scriptable objects  
{
    [InfoBox("Make sure max values are higher than min values", EInfoBoxType.Warning)]
    public GameProcessSettings gameProcessSettings;
    public EnemiesSettings enemiesSettings;
    public PlayerSettings playerSettings;
    public MinesSettings minesSettings;
}
