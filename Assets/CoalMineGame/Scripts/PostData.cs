using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PostData")]
public class PostData : ScriptableObject
{
    public OverlayType overlayType;
    public GameIcons icons;
    public Sprite postSprite;
    [TextArea(10, 100)]
    public string postDescription;
    [TextArea(3, 100)]
    public string postTags;

    public bool postLocked;

    public string interactionScene;
    public string videoName;

    public Sprite GetReplayIcon()
    {
        return icons.replayIcon;
    }

    public Sprite GetIcon()
    {
        Sprite xy;

        switch (overlayType)
        {
            case OverlayType.IMAGE:
                xy = null;
                break;
            case OverlayType.VIDEO:
                xy = icons.videoIcon;
                break;
            case OverlayType.QUIZ:
                xy = icons.interactIcon;
                break;
            case OverlayType.INTERACTION:
                xy = icons.interactIcon;
                break;
            default:
                xy = null;
                break;
        }

        Debug.Log("obj is null " + xy == null);
        return xy;
    }
}
