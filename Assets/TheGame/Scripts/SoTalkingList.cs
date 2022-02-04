using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoTalkingList")]
public class SoTalkingList : ScriptableObject
{
    public string listName;
    public AudioClip[] orderedListOfAudioClips;
}
