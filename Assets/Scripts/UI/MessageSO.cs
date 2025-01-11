using UnityEngine;

[CreateAssetMenu(menuName = "Messages/New")]
public class MessageSO : ScriptableObject
{
    public int minPlayerPois;
    public int maxPlayerPois;
    public int minEnemyPois;
    public int maxEnemyPois;
    public bool boboMode;

    [Multiline]
    public string message;
    public int lines = 1;
}
