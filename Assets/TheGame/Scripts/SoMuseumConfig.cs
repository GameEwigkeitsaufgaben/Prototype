using UnityEngine;

[CreateAssetMenu(menuName = "SoMuseumConfig")]
public class SoMuseumConfig : ScriptableObject
{
    public Sprite info, world, carbonification, myth, miner;
    public Sprite memoryBackside;

    [TextArea(10,100)]
    public string textCentury13,textCentury16,textCentury19, textCentury21;

}
