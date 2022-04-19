using UnityEngine;
using UnityEngine.UI;

public enum OverlayChildType
{
    IMAGE, TYPEICON, DESCRIPTION, TAGS,
}

public class Overlay : MonoBehaviour
{
    private Transform[] allOverlayChildren;
    private const int OVERLAYIMAGE = 3; 
    private const int OVERLAYTYPEICON = 4;
    private const int OVERLAYDESCRIPTION = 6;
    private const int OVERLAYTAGS = 7;

    private WebGlVideoPlayer webglVideoPlayer;

    private SoPostData postData;
    private SoGameIcons icons;
    private bool videoFinished = false;
    private PostManagerChapterOne menuManager;


    private void Start()
    {
        icons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        webglVideoPlayer = GameObject.FindObjectOfType<WebGlVideoPlayer>();
        menuManager = FindObjectOfType<PostManagerChapterOne>();
    }

    public void SetReplayIcon()
    {
        videoFinished = true;
        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().sprite = postData.GetReplayIcon();
        SetIconActive(true);
    }

    public void SetIconActive(bool active)
    {
        allOverlayChildren[OVERLAYTYPEICON].gameObject.SetActive(active);
    }

    public void PlayVideo() //wird von OnClick von Button aufgerufen. 
    {
        allOverlayChildren[OVERLAYTYPEICON].gameObject.SetActive(false);
        
        ColorBlock ab = allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors;
        ab.disabledColor = Color.white;
        
        if (webglVideoPlayer != null)
        {
            webglVideoPlayer.StartTheVideo(gameObject.name, postData.videoName, allOverlayChildren[OVERLAYIMAGE].gameObject.GetComponent<RawImage>());
        }
        else
        {
            Debug.Log("Webgl player is null on Overlay: " + gameObject.name);
        }
    }

    private void SetUpOverlay()
    {
        allOverlayChildren = gameObject.transform.GetComponentsInChildren<Transform>(true); //inclusive inactive elements
        
        ShowOverlayChildrenInArray();

        allOverlayChildren[OVERLAYDESCRIPTION].gameObject.GetComponent<Text>().text = postData.postDescription;
        allOverlayChildren[OVERLAYTAGS].gameObject.GetComponent<Text>().text = postData.postTags;
        allOverlayChildren[OVERLAYIMAGE].gameObject.GetComponent<RawImage>().texture = postData.postSprite.texture;


        if (postData.overlayType == OverlayType.IMAGE)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = false;
            ColorBlock ab = allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors;
            ab.disabledColor = Color.white;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors = ab;
            allOverlayChildren[OVERLAYTYPEICON].gameObject.SetActive(false);
            return;
        }
        else if(postData.overlayType == OverlayType.QUIZ)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = true;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().onClick.AddListener(delegate { SwitchTheScene(postData.interactionScene); });
        }
        else if(postData.overlayType == OverlayType.INTERACTION)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = true;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().onClick.AddListener(delegate { SwitchTheScene(postData.interactionScene); });
        }
        

        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().sprite = postData.GetIcon();
        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().gameObject.SetActive(true);
        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().raycastTarget = false;
    }

    private void SwitchTheScene(string interactionScene)
    {
        menuManager.GetComponent<SwitchSceneManager>().SwitchScene(interactionScene);
    }

    public void SetOverlayData(SoPostData data)
    {
        postData = data;
        SetUpOverlay();
    }

    public void ShowOverlayChildrenInArray()
    {
        int index = 0;
        foreach(Transform f in allOverlayChildren)
        {
            index++;
        }
    }

    public void UpdateOverlayText()
    {
        string points = GameData.quizChapterOnePoints.ToString();
        allOverlayChildren[OVERLAYDESCRIPTION].gameObject.GetComponent<Text>().text = $"Punkte: {points}\n" + postData.postDescription;
    }

    public void CloseOverlay()
    {
        //SetIconActive(true);
        gameObject.SetActive(false);
        webglVideoPlayer.StopTheVideo();
    }

    public SoPostData GetPostData()
    {
        return postData;
    }

}
