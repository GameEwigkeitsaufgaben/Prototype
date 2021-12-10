using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    private PostData postData;
    public GameObject prefabImgLocked;

    private GameObject childIcon = null;
    private SpriteRenderer iconSpriteRenderer = null;

    void Start()
    {
        Debug.Log("Post created" + gameObject.name);
    }

    private void SetUpPost()
    {
        gameObject.GetComponent<Image>().sprite = postData.postSprite;

        //generate child obj as image container locked, ineract, video.
        //if locked a corresponding symbol is shown and the post is not interactable
        childIcon = Instantiate(prefabImgLocked);
        childIcon.transform.SetParent(gameObject.transform.parent, false);
        childIcon.GetComponent<RectTransform>().localPosition = Vector3.zero;
        childIcon.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        childIcon.GetComponent<Image>().raycastTarget = false;

        gameObject.GetComponent<Button>().interactable = false;

        if (postData.postLocked) return;

        //if the post is unlocked the post is interactable, the sprite is set and icon is set 
        //if it is an image without an icon, the lockicon will be deactivated. 
        gameObject.GetComponent<Button>().interactable = true;
        childIcon.GetComponent<Image>().sprite = postData.GetIcon();
        if (childIcon.GetComponent<Image>().sprite == null)
            childIcon.GetComponent<Image>().gameObject.SetActive(false);
        Debug.Log("Post created" + gameObject.name);
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
}
