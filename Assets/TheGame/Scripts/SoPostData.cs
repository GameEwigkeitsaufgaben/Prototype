using UnityEngine;

[CreateAssetMenu(menuName ="SoPostData")]
public class SoPostData : ScriptableObject
{
    public OverlayType overlayType;
    public SoGameIcons icons;
    public Sprite postSprite;
    [TextArea(10, 100)]
    public string postDescription;
    [TextArea(3, 100)]
    public string postTags;

    public bool postUnLocked;
    public string hintToUnlock;

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

        return xy;
    }

    private void OnEnable()
    {
        if(this.name == GameData.NamePost113 ||
            this.name == GameData.NamePost114 ||
            this.name == GameData.NamePost115 ||
            this.name == GameData.NamePost116 ||
            this.name == GameData.NamePost117 ||
            this.name == GameData.NamePost118 ||
            this.name == GameData.NamePost119 ||
            this.name == GameData.NamePost1110)
        {
            postUnLocked = false;
        }
    }
}
