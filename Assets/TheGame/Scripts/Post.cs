using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    private const string codePost = "Post1110";
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
        childIcon.GetComponent<Image>().sprite = icons.replayIcon;
    }

    //will be called form Class Entry!
    private void SetUpPost()
    {
        gameObject.GetComponent<Image>().sprite = postData.postSprite;

        //generate child obj with local positioning
        //per defalt set post locked, corresponding symbol is shown and the post is not interactable

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

        //If you entered the adminpin go futher to unlock post
        if (GameData.progressWithAdmin)
        {
            postData.postUnLocked = true;
        }
        
        //if post unlocked go further to unlock post in instamenu
        if (!postData.postUnLocked) return;

        //if the post is unlocked the post is interactable, the sprite is set and icon is set 
        //if it is an image without an icon, the lockicon will be deactivated.

        UnlockPost();
    }

    public void SetPostData(PostData data)
    {
        postData = data;
        SetUpPost();
    }


    public void UnlockPost()
    {
        postData.postUnLocked = true;

        gameObject.GetComponent<Button>().interactable = true;

        Debug.Log("Post " + gameObject.name + " unlocked.");

        if (OverlayType.IMAGE == postData.overlayType)
        {
            childIcon.GetComponent<Image>().gameObject.SetActive(false);
            return;
        }

        childIcon.GetComponent<Image>().sprite = postData.GetIcon();

        //if (gameObject.name == codePost)
        //{
        //    childIcon.SetActive(false);
        //}
    }
}
