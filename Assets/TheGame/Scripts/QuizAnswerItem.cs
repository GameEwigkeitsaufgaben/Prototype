using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizAnswerItem
{
    //Is not a Monobehavior script so so gameObject is available to ref sprite; 
    private SoQuizConfig myQuizConfig; 
    public string questionIdentifier;
    public string answer;
    public int answerIdentifier;
    public bool isCorrect;
    public Button btn;
    public bool buttonSelected = false;
    VerticalLayoutGroup buttonGroup;
    public bool longAnswer;
    public int btnNbr = 0;

    SoChapOneRuntimeData runtimeData;

    public int timeToAnswerInSec;

    public QuizAnswerItem() 
    {
        myQuizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
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
        //var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        var newButton = TMP_DefaultControls.CreateButton(new TMP_DefaultControls.Resources());
        newButton.transform.SetParent(parent.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x,
                                                            newButton.transform.localPosition.y,
                                                            0);

        newButton.GetComponent<Image>().sprite = myQuizConfig.btnSprite;
        btn = newButton.GetComponent<Button>();
        btn.name = "AnswerBtn";
        btn.gameObject.AddComponent<MouseInteractionElement>();
        btn.gameObject.GetComponent<MouseInteractionElement>().uiType = MouseInteraction.BtnQuizAnswer;
        btn.gameObject.AddComponent<MouseChange>();
        btn.gameObject.AddComponent<ButtonHoverTriggers>();
        btn.gameObject.AddComponent<ReactOnlyOnInTranspartentParts>();
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

        TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
        btnText.fontSize = 30;
        btnText.color = GameColors.defaultTextColor;
        btnText.font = myQuizConfig.font;
        btnText.fontMaterial = myQuizConfig.font.material;
        btnText.fontStyle = FontStyles.SmallCaps;
        btnText.text = answer;

        btnNbr++;
    }

    private void ToogleSelected()
    {
        buttonSelected = !buttonSelected;
        
        if (buttonSelected)
        {
            //selectColor = Color.cyan;
            //btn.GetComponent<Image>().color = myQuizConfig.selected;
            btn.Select();
            
        }
        else
        {
            //selectColor = Color.white;
            //btn.GetComponent<Image>().color = myQuizConfig.normal;
        }
    }

    public void ShowResult()
    {
        if (!isCorrect)
        {
           btn.GetComponent<Image>().color = myQuizConfig.incorrect;
        }
        else
        {
            btn.GetComponent<Image>().color = myQuizConfig.correct;
        }

        Debug.Log("llllllllllll + *" + btn.GetComponent<MouseInteractionElement>().IsSelected());
        if (btn.GetComponent<MouseInteractionElement>().IsSelected())
        {
            Debug.Log("in Show Result üüüüüüüüüüüüüüüüüüüüüüüüü" + btn.GetComponent<MouseInteractionElement>().IsSelected());
            //btn.GetComponentInChildren<TMP_Text>().fontWeight = FontWeight.Bold;
        }
        
    }

    public int GetPointForAnswer()
    {
        return (buttonSelected && isCorrect) ^ (!buttonSelected && !isCorrect) ? 1 : 0;
    }
}
