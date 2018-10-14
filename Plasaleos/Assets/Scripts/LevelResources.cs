using UnityEngine;

[CreateAssetMenu(fileName = "Level Resource")]
public class LevelResources : ScriptableObject {
    [Header("Ambient")]
    public int lianas;
    public int mushrooms;

    [Header("Sounds")]
    public int alerts;
    public int scares;
}