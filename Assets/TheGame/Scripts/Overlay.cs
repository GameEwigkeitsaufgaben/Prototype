using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum OverlayChildType
{
    IMAGE, TYPEICON, DESCRIPTION, TAGS,
}

public enum OverlaySoundState
{
    Closed,
    Opened,
    SoudAjusted,
    NoSound
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
    public ManagerInstaPost instaPostManager;
    private SoChapOneRuntimeData runtimeDataChap01;
    private SoChapTwoRuntimeData runtimeDataChap02;
    private SoChapThreeRuntimeData runtimeDataChap03;

    private chapter currentCH;

    private void Awake()
    {
        icons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        runtimeDataChap01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChap02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChap03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        webglVideoPlayer = GameObject.FindObjectOfType<WebGlVideoPlayer>();

        if (SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            currentCH = chapter.ch1;
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            currentCH = chapter.ch2;
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            currentCH = chapter.ch3;
        }
    }

    private void Start()
    {
        instaPostManager = FindObjectOfType<ManagerInstaPost>();
    }

    public void SetReplayIcon()
    {
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
        allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors = GameColors.GetOverlayColorBlock();
        
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

        allOverlayChildren[OVERLAYDESCRIPTION].gameObject.GetComponent<TMP_Text>().text = postData.postDescription;
        allOverlayChildren[OVERLAYTAGS].gameObject.GetComponent<TMP_Text>().text = postData.postTags;
        allOverlayChildren[OVERLAYIMAGE].gameObject.GetComponent<RawImage>().texture = postData.postSprite.texture;

        if (postData.overlayType == OverlayType.IMAGE)
        {
            Destroy(allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>());
            Destroy(allOverlayChildren[OVERLAYIMAGE].GetComponent<MouseChange>());
            allOverlayChildren[OVERLAYTYPEICON].gameObject.SetActive(false);
            return;
        }
        else if(postData.overlayType == OverlayType.QUIZ)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = true;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors = GameColors.GetOverlayColorBlock();
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().onClick.AddListener(delegate { SwitchTheScene(postData.interactionScene); });
        }
        else if(postData.overlayType == OverlayType.INTERACTION)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = true;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors = GameColors.GetOverlayColorBlock();
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().onClick.AddListener(delegate { SwitchTheScene(postData.interactionScene); });
        }

        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().sprite = postData.GetIcon();
        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().gameObject.SetActive(true);
        allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().raycastTarget = false;
    }

    private void SwitchTheScene(string interactionScene)
    {
        instaPostManager.GetComponent<SwitchSceneManager>().SwitchScene(interactionScene);
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

    public void UpdateOverlayText(chapter inChapter)
    {
        // verbessern!
        string points = "---";

        Debug.Log("update points: in ch " + inChapter );

        switch (inChapter)
        {
            case chapter.ch1:
                if (runtimeDataChap01 == null) return;
                points = runtimeDataChap01.quizPointsOverall.ToString();
                break;
            case chapter.ch2:
                if (runtimeDataChap02 == null) return;
                points = runtimeDataChap02.quizPointsOverall.ToString();
                runtimeDataChap02.updatePoints = false;
                break;
            case chapter.ch3:
                if (runtimeDataChap03 == null) return;
                points = runtimeDataChap03.quizPointsOverall.ToString();
                runtimeDataChap03.updatePoints = false; 
                break;
            default:
                points = "error";
                break;
        }

        allOverlayChildren[OVERLAYDESCRIPTION].gameObject.GetComponent<TMP_Text>().text = $"Punkte: {points}\n" + postData.postDescription;
    }

    public void CloseOverlay()
    {
        gameObject.SetActive(false);
        runtimeDataChap01.overlaySoundState = OverlaySoundState.Closed;
        webglVideoPlayer.StopTheVideo();
    }

    public SoPostData GetPostData()
    {
        return postData;
    }
}
