using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PostData")]
public class PostData : ScriptableObject
{
    public OverlayType overlayType;
    public Sprite postSprite;
    [TextArea(10, 100)]
    public string postDescription;
    [TextArea(3, 100)]
    public string postTags;

    public bool postLocked;

    public string interactionScene;
    public string videoName;
}
