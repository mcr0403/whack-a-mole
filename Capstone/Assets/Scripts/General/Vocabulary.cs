using UnityEngine;

[CreateAssetMenu(menuName = "Vocabulary", fileName = "Vocabulary")]
public class Vocabulary : ScriptableObject
{
    public string lessonName;
    public string word;
    public Sprite image;
}
