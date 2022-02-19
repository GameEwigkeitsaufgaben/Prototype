using UnityEngine;
using UnityEngine.UI;

public class MainCapterSceneManager : MonoBehaviour
{
    private const string pwdChapterOne = "111";
    private const string pwdAdminChapterOne = "1212";
    private const string pwdChapterTwo = "222";
    private const string pwdChapterThree = "333";

    [SerializeField] InputField inputFieldChapterOne;
    [SerializeField] InputField inputFieldChapterTwo;
    [SerializeField] InputField inputFieldChapterThree;

    //shake effect of InputField
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 0.0f;
    
    private float shakeDuration = 1.0f;
    private Vector3 origPos;
    private bool shakeObj = false;
    private GameObject toShakeObj;

    private void Start()
    {
        if (GameData.chapterOneUnlocked == 1)
        {
            inputFieldChapterOne.gameObject.SetActive(false);
        }
        if (GameData.chapterTwoUnlocked == 1)
        {
            inputFieldChapterTwo.gameObject.SetActive(false);
        }
        if (GameData.chapterThreeUnlocked == 1)
        {
            inputFieldChapterThree.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (shakeObj)
        {
            toShakeObj.GetComponent<InputField>().image.color = Color.red;
            if(shakeDuration > 0)
            {
                toShakeObj.transform.localPosition = origPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 1f;
                toShakeObj.transform.localPosition = origPos;
                shakeObj = false;
                toShakeObj.GetComponent<InputField>().image.color = Color.white;
            }
        }
    }

    public void CheckInputForChapter(int chapterCode)
    {
        bool pwdCorrect = false;
        pwdCorrect = CheckPwdCorrect(chapterCode, pwdCorrect);

        if (pwdCorrect)
        {
            SwitchToChapter(chapterCode);
        }
        else
        {
            ProvideFeedbackInputIncorrect(chapterCode);
        }
    }

    private void ProvideFeedbackInputIncorrect(int chapterCode)
    {
        switch (chapterCode)
        {
            case 1:
                SetObjectToShake(inputFieldChapterOne.gameObject);
                inputFieldChapterOne.text = "";
                break;
            case 2:
                SetObjectToShake(inputFieldChapterTwo.gameObject);
                inputFieldChapterTwo.text = "";
                break;
            case 3:
                SetObjectToShake(inputFieldChapterThree.gameObject);
                inputFieldChapterThree.text = "";
                break;
        }
    }

    private void SwitchToChapter(int chapterCode)
    {
        string sceneToSwitch = "";
        switch (chapterCode)
        {
            case 1:
                sceneToSwitch = GameScenes.ch01InstaMain;
                GameData.chapterOneUnlocked = 1;
                break;
            case 2:
                //sceneToSwitch = GameData.sceneInstaMainChapterTwo;
                GameData.chapterTwoUnlocked = 1;
                break;
            case 3:
                //sceneToSwitch = GameData.sceneInstaMainChapterThree;
                GameData.chapterThreeUnlocked = 1;
                break;
        }

        gameObject.GetComponent<SwitchSceneManager>().SwitchScene(sceneToSwitch);
    }

    private bool CheckPwdCorrect(int chapterCode, bool pwdCorrect)
    {
        //Check admin and set unlock all for chapter
        switch (chapterCode)
        {
            case 1:
                if (inputFieldChapterOne.text == pwdAdminChapterOne)
                {
                    GameData.chapterOneUnlocked = 1;
                    GameData.progressWithAdmin = true;
                }
                break;
            case 2:
                //ToDO
                break;
            case 3:
                //Todo
                break;
        }
        
        switch (chapterCode)
        {
            case 1:
                if ((inputFieldChapterOne.text == pwdChapterOne) ^ GameData.chapterOneUnlocked == 1) pwdCorrect = true;
                break;
            case 2:
                if (inputFieldChapterTwo.text == pwdChapterTwo ^ GameData.chapterTwoUnlocked == 1) pwdCorrect = true;
                break;
            case 3:
                if (inputFieldChapterThree.text == pwdChapterThree ^ GameData.chapterThreeUnlocked == 1) pwdCorrect = true;
                break;
        }

        return pwdCorrect;
    }

    public void SetObjectToShake(GameObject obj)
    {
        toShakeObj = obj;
        origPos = toShakeObj.transform.localPosition;
        shakeObj = true;
    }

}
