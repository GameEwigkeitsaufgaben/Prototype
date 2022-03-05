using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoCharacter")]
public class SoCharacter : ScriptableObject
{
    public CharactersInGame character;
    public Sprite outsideMineMovingTalking;
    public Sprite outsideMineMovingSilent;
    public Sprite outsideMineStandingTalking;
    public Sprite outsideMineStandingSilient;

    //Charaters are only in Cave, accept the one viewport! Enry Area, Sole1, Sole2
    public Sprite entryAreaStandingSilent;
    public Sprite entryAreaStandingTalking;
    public Sprite sole1StandingTalking;
    public Sprite sole1StandingSilent;
    public Sprite sole2StandingTalking;
    public Sprite sole2StandingSilent;
    public Sprite sole3StandingTalking;
    public Sprite sole3StandingSilent;
    public Sprite longwallCutterStandingTalking;
    public Sprite longwallCutterStandingSilent;
    public Sprite trainRideBlinkEyes;

    private void Awake()
    {
        
    }
}
