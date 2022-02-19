using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    private PostData postData;
    private SoGameIcons icons;
    public GameObject prefabImgLocked;

    private WebGlVideoPlayer webglVideoPlayer;

    private GameObject childIcon = null;
    private SpriteRenderer iconSpriteRenderer = null;

    void Start()
    {
        webglVideoPlayer = GameObject.FindObjectOfType<WebGlVideoPlayer>();
        icons = Resources.Load<SoGameIcons>("Icons");
    }

    public void UpdateIcon()
    {
        //Debug.Log("Update Icon in Post " + gameObject.name);
        childIcon.GetComponent<Image>().sprite = icons.replayIcon;
    }

    //will be called form Class Entry!
    private void SetUpPost()
    {
        gameObject.GetComponent<Image>().sprite = postData.postSprite;

        //generate child obj as image container locked, ineract, video.
        //if locked a corresponding symbol is shown and the post is not interactable

        //Maybe better to create it not at runtime, only set it active.
        //Todo: create it via script not via prefab
        childIcon = Instantiate(prefabImgLocked);
        childIcon.transform.SetParent(gameObject.transform.parent, false);
        childIcon.GetComponent<RectTransform>().localPosition = Vector3.zero;
        childIcon.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        childIcon.GetComponent<Image>().sprite = icons.lockedIcon;
        childIcon.GetComponent<Image>().preserveAspect = true;
        childIcon.GetComponent<Image>().raycastTarget = false;

        gameObject.GetComponent<Button>().interactable = false;
        
        //&& to unlock all || to lock all
        if (!GameData.progressWithAdmin && !postData.postUnLocked) return;

        //if the post is unlocked the post is interactable, the sprite is set and icon is set 
        //if it is an image without an icon, the lockicon will be deactivated.
        postData.postUnLocked = true;
        gameObject.GetComponent<Button>().interactable = true;

        if (OverlayType.IMAGE == postData.overlayType)
        {
            childIcon.GetComponent<Image>().gameObject.SetActive(false);
            return;
        }

        childIcon.GetComponent<Image>().sprite = postData.GetIcon();
    }

    public void SetOverlayData(PostData data)
    {
        postData = data;
        SetUpPost();
    }

    public void SetButtonFunctionInteractable(bool interactable)
    {
        gameObject.GetComponent<Button>().interactable = interactable;
    }

    public void UnlockPost()
    {
        SetButtonFunctionInteractable(true);
        childIcon.GetComponent<Image>().sprite = postData.GetIcon();
        if (gameObject.name == "Post1110")
        {
            childIcon.SetActive(false);
        }
    }
}
