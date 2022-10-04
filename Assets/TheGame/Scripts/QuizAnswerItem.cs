using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class QuizAnswerItem
{
    //Is not a Monobehavior script so gameObject is available to ref sprite; 
    private SoQuizConfig myQuizConfig;
    private SoChapOneRuntimeData runtimeData;
    private SoChapTwoRuntimeData runtimeDataChap02;
    private SoChapThreeRuntimeData runtimeDataChap03;
    private VerticalLayoutGroup buttonGroup;

    public string questionIdentifier;
    public int answerIdentifier;
    public QuizQuestionType questType;
    public string answer;

    public bool isCorrect;
    public Button btn;
    public bool buttonSelected = false;
    public bool longAnswer;
    public int timeToAnswerInSec;

    public QuizAnswerItem() 
    {
        myQuizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChap02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChap03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
    }

    public QuizAnswerItem(VerticalLayoutGroup parent) 
    {
        buttonGroup = parent;
        PrintItemData();
    }

    public void PrintItemData()
    {
        Debug.Log("questionIdentifier: " + questionIdentifier + "\n" +
                  "answer: " + answer + "\n" +
                  "answerIdentifier: " + answerIdentifier + "\n" +
                  "isCorrect: " + isCorrect);
    }

    
    //https://stackoverflow.com/questions/33431719/unity-9-slice-in-multiple-sprite-sheet

    public void CreateButton(VerticalLayoutGroup parent, int id)
    {
        var newButton = TMP_DefaultControls.CreateButton(new TMP_DefaultControls.Resources());
        
        newButton.transform.SetParent(parent.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x,
                                                        newButton.transform.localPosition.y,
                                                        0);

        newButton.GetComponent<Image>().sprite = myQuizConfig.btnSprite;

        newButton.GetComponent<Button>().name = "AnswerBtn"+id;
        newButton.GetComponent<Button>().gameObject.AddComponent<QuizAnswerUiBehaviour>();
        newButton.GetComponent<Button>().gameObject.AddComponent<MouseChange>();
        newButton.GetComponent<Button>().gameObject.AddComponent<ButtonHoverTriggers>();
        newButton.GetComponent<Button>().gameObject.AddComponent<ReactOnlyOnInTranspartentParts>();
        newButton.GetComponent<Button>().onClick.AddListener(delegate { SelectThisAnswer(); });
        newButton.GetComponent<Button>().gameObject.SetActive(false);

        //https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/HOWTO-UIFitContentSize.html#fit-to-size-of-ui-element-with-child-text
        newButton.GetComponent<Button>().gameObject.AddComponent<HorizontalLayoutGroup>();
        
        int myPadding = 50;
        newButton.GetComponent<Button>().GetComponent<HorizontalLayoutGroup>().padding.left = myPadding;
        newButton.GetComponent<Button>().GetComponent<HorizontalLayoutGroup>().padding.right = myPadding;
        newButton.GetComponent<Button>().GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        newButton.GetComponent<Button>().GetComponent<Image>().type = Image.Type.Sliced;
        newButton.GetComponent<Button>().GetComponent<Image>().pixelsPerUnitMultiplier = 0.5f;
        //Navigation customNav = new Navigation();
        //customNav.mode = Navigation.Mode.None;
        //newButton.GetComponent<Button>().navigation = customNav;

        TMP_Text btnText = newButton.GetComponentInChildren<TMP_Text>();
        btnText.fontSize = 30;
        btnText.color = GameColors.defaultTextColor;
        btnText.font = myQuizConfig.font;
        btnText.fontMaterial = myQuizConfig.font.material;
        btnText.fontStyle = FontStyles.Normal;
        btnText.text = answer;

        btn = newButton.GetComponent<Button>();
    }

    public void ShowResult()
    {
        btn.GetComponent<QuizAnswerUiBehaviour>().ShowResult();
    }

    public int GetPointPerAnswerAndShowUIResult()
    {
        return btn.GetComponent<QuizAnswerUiBehaviour>().GetAndShowResultPerAnswer();
    }

    private void SelectThisAnswer()
    {
        if(SceneManager.GetActiveScene().name == GameScenes.ch01Quiz)
        {
            if (runtimeData.singleSelectAwIdOld == null)
            {
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
                runtimeData.singleSelectAwIdOld = btn.gameObject;
            }

            else if (runtimeData.singleSelectAwIdOld.GetComponent<QuizAnswerUiBehaviour>().awId != btn.GetComponent<QuizAnswerUiBehaviour>().awId)
            {
                runtimeData.singleSelectAwIdOld.GetComponent<QuizAnswerUiBehaviour>().isSelected = false;
                runtimeData.singleSelectAwIdOld = btn.gameObject;
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02Quiz)
        {
            if(EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject == null)
            {
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
                runtimeDataChap02.singleSelectAwObjName = btn.gameObject.name;
            }
            else if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject.name != runtimeDataChap02.singleSelectAwObjName)
            {
                runtimeDataChap02.singleSelectAwObjName = btn.gameObject.name;
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03Quiz)
        {
            if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject == null)
            {
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
                runtimeDataChap03.singleSelectAwObjName = btn.gameObject.name;
            }
            else if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject.name != runtimeDataChap03.singleSelectAwObjName)
            {
                runtimeDataChap03.singleSelectAwObjName = btn.gameObject.name;
                btn.GetComponent<QuizAnswerUiBehaviour>().isSelected = true;
            }
        }
    }

    public int GetPointsPerAnswer()
    {
        return btn.GetComponent<QuizAnswerUiBehaviour>().IsCorrectlySelected() ? 1 : 0;
    }
}
