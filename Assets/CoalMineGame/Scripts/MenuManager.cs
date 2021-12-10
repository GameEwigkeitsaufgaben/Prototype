using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public WebGlVideoPlayer webglVideoPlayer;

    public GameObject introOverlay;
    public GameObject menuItemMine;
    public GameObject menuItemMineWather;
    public GameObject menuItemFutureTasks;

    public Button skipIntroVideo; 

    public Image lockMine;
    public Image lockMineWather;
    public Image lockFutureTasks;
    public Image videoPlayerIcon;

    public Sprite lockOpened;
    public Sprite lockClosed;
    public Sprite videoPlayerPlayIcon;
    public Sprite videoPlayerRestartIcon;

    public bool testMineActivated, testMineWatherActivated, testLockFutureTasksActivted;

    public void PlayIntroVideo()
    {
        introOverlay.GetComponent<Button>().interactable = false;
        videoPlayerIcon.gameObject.SetActive(false);
        webglVideoPlayer.StartTheVideo();
        
        if (GameData.introPlayedOnce)
        {
            skipIntroVideo.gameObject.SetActive(true);
        }
    }

    public void ActivateItemMine()
    {
        if (!menuItemMine.GetComponent<Button>().interactable)
        {
            menuItemMine.GetComponent<Button>().interactable = true;
            lockMine.GetComponent<Image>().sprite = lockOpened;
        }

    }

    public void ActivateItemMineWather()
    {
        menuItemMineWather.GetComponent<Button>().interactable = true;
        lockMineWather.GetComponent<Image>().sprite = lockOpened;
    }

    public void ActivateItemFutureTasks()
    {
        menuItemFutureTasks.GetComponent<Button>().interactable = true;
        lockFutureTasks.GetComponent<Image>().sprite = lockOpened;
    }

    void Start()
    {
        menuItemMine.GetComponent<Button>().interactable = false;
        menuItemMineWather.GetComponent<Button>().interactable = false;
        menuItemFutureTasks.GetComponent<Button>().interactable = false;

        videoPlayerIcon.sprite = videoPlayerPlayIcon;
        skipIntroVideo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameData.mineIsPlayable)
        //{
        //    ActivateItemMine();
        //    gameData.mineIsPlayable = false;
        //}

        //else if (testMineWatherActivated)
        //{
        //    ActivateItemMineWather();
        //    testMineWatherActivated = false;
        //}

        //else if (testLockFutureTasksActivted)
        //{
        //    ActivateItemFutureTasks();
        //    testLockFutureTasksActivted = false;
        //}

        if (GameData.restorIntroVideo)
        {
            introOverlay.GetComponent<Button>().interactable = true;
            videoPlayerIcon.sprite = videoPlayerRestartIcon;
            videoPlayerIcon.gameObject.SetActive(true);
            skipIntroVideo.gameObject.SetActive(false);
            GameData.restorIntroVideo = false;
        }
    }

}
