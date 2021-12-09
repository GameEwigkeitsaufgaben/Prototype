using System;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public PostData postData;
    public GameIcons icons;
    public GameObject prefabImgLocked;

    private OverlayType overlayType; // Enum
    private GameObject childIcon = null;
    private SpriteRenderer spriteRenderer = null;

    void Start()
    {
        SetPostImage(postData.postSprite);
        
        if (postData.postLocked)
        {
            SetUpPostLocked();
        }
        else
        {
            SetUpPostUnlocked();
        }
    }

    private void SetPostImage(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }

    public void SetButtonFunctionInteractable(bool interactable)
    {
        gameObject.GetComponent<Button>().interactable = interactable;
    }

    public void SetUpPostLocked()
    {
        gameObject.GetComponent<Button>().interactable = false;
        childIcon = Instantiate(prefabImgLocked);
        childIcon.transform.SetParent(gameObject.transform.parent, false);
        childIcon.GetComponent<RectTransform>().localPosition = Vector3.zero;
        childIcon.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        childIcon.GetComponent<Image>().raycastTarget = false;
    }

    public void SetUpPostUnlocked()
    {
        gameObject.GetComponent<Button>().interactable = true;
        spriteRenderer = transform.GetChild(0).GetComponent<Image>().GetComponent<SpriteRenderer>();

        switch (overlayType)
        {
            case OverlayType.IMAGE:
                break;
            case OverlayType.VIDEO:
                spriteRenderer.sprite = icons.videoIcon;
                break;
            case OverlayType.QUIZ:
                spriteRenderer.sprite = icons.interactIcon;
                break;
            case OverlayType.INTERACTION:
                spriteRenderer.sprite = icons.interactIcon;
                break;
        }
    }
}
