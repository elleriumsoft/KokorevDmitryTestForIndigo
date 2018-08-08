using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Discs")]
    public int MinDiscs;
    public int MaxDiscs;
    public float StartSpeed;
    [HideInInspector]
    public int SelectedCount;
    [HideInInspector]
    public float CurrentSpeed;
}