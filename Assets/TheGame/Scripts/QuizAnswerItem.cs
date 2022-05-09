using UnityEngine;
using UnityEngine.UI;

public class QuizAnswerItem
{
    //Is not a Monobehavior script so so gameObject is available to ref sprite; 
    private const string pathToConfig = "QuizChapterOneConfig";
    private SoQuizConfig myQuizConfig; 
    public string questionIdentifier;
    public string answer;
    public int answerIdentifier;
    public bool isCorrect;
    public Button btn;
    public bool buttonSelected = false;
    VerticalLayoutGroup buttonGroup;
    public bool longAnswer;

    SoChapOneRuntimeData runtimeData;

    public int timeToAnswerInSec;

    public QuizAnswerItem() 
    {
        myQuizConfig = Resources.Load<SoQuizConfig>(pathToConfig);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
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

    public void CreateButton(VerticalLayoutGroup parent)
    {
        var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        newButton.transform.SetParent(parent.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x,
                                                            newButton.transform.localPosition.y,
                                                            0);

        newButton.GetComponent<Image>().sprite = myQuizConfig.btnSprite;
        newButton.GetComponentInChildren<Text>().fontSize = 20;
        newButton.GetComponentInChildren<Text>().text = answer;
        btn = newButton.GetComponent<Button>();
        btn.onClick.AddListener(ToogleSelected);
        btn.gameObject.SetActive(false);

        //https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/HOWTO-UIFitContentSize.html#fit-to-size-of-ui-element-with-child-text
        btn.gameObject.AddComponent<HorizontalLayoutGroup>();
        int myPadding = 50;
        btn.gameObject.GetComponent<HorizontalLayoutGroup>().padding.left = myPadding;
        btn.gameObject.GetComponent<HorizontalLayoutGroup>().padding.right = myPadding;
        btn.gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        btn.gameObject.GetComponent<Image>().type = Image.Type.Sliced;
        btn.gameObject.GetComponent<Image>().pixelsPerUnitMultiplier = 0.5f;
        btn.GetComponentInChildren<Text>().fontSize = 30;
        btn.GetComponent<Image>().color = myQuizConfig.normal;

        //disable color when set buttons inactable
        ColorBlock cb = btn.colors;
        cb.disabledColor = new Color(1f, 1f, 1f, 1f);
        btn.colors = cb;
    }

    private void ToogleSelected()
    {
        buttonSelected = !buttonSelected;

        Color selectColor = Color.cyan;
        
        if (buttonSelected)
        {
            //selectColor = Color.cyan;
            btn.GetComponent<Image>().color = myQuizConfig.selected;
        }
        else
        {
            //selectColor = Color.white;
            btn.GetComponent<Image>().color = myQuizConfig.normal;
        }
    }

    public void ShowResult()
    {
        if (!isCorrect)
        {
            Color c = btn.GetComponent<Image>().color;
            btn.GetComponent<Image>().color = myQuizConfig.incorrect;
            
            //btn.GetComponentInChildren<Text>().color = new Vector4(c.r, c.g, c.b, 0.4f);
        }
        else
        {
            btn.GetComponent<Image>().color = myQuizConfig.correct;
           
        }
    }

    public int GetPointForAnswer()
    {
        return (buttonSelected && isCorrect) ^ (!buttonSelected && !isCorrect) ? 1 : 0;
    }
}
