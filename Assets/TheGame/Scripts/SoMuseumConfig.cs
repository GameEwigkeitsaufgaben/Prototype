using UnityEngine;

[CreateAssetMenu(menuName = "SoMuseumConfig")]
public class SoMuseumConfig : ScriptableObject
{
    public Sprite guideStanding, guideWalking;
    public Sprite info, world, carbonification, myth, miner;
    public Sprite tv, fliesspfad;
    public Sprite memoryBackside;
    public Sprite minerIdle, minerThumpUp, minerThumpDown;
    public AudioClip sfxFlipCard;

    [Header("History Mining")]
    [TextArea(10,100)]
    public string textmyth, textCentury13,textCentury16,textCentury19, textCentury21;

    [Header("Coalification")]
    [TextArea(10, 100)]
    public string pflanzenSterben, torfEntsteht, Wiederholung, tektonischeKraefte;
}
