using UnityEngine;

[CreateAssetMenu(menuName = "SoMuseumConfig")]
public class SoMuseumConfig : ScriptableObject
{
    public Sprite info, world, carbonification, myth, miner;
    public Sprite memoryBackside;
    public Sprite minerIdle, minerThumpUp, minerThumpDown;

    [TextArea(10,100)]
    public string textmyth, textCentury13,textCentury16,textCentury19, textCentury21;
}
