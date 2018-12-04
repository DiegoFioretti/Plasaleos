using UnityEngine;

[CreateAssetMenu(menuName = "Resources/Level Resource")]
public class LevelResources : ScriptableObject {
    [Header("Ambient")]
    public int lianas;
    public int mushrooms;

    [Header("Sounds")]
    public int alerts;
    public int scares;

    [Header("Gravity")]
    public bool restricted;
    public Vector2 direction = new Vector2(0f, -1f);
}