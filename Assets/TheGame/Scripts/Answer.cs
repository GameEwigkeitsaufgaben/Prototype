using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Answer : MonoBehaviour
{
    public Transform origParent;
    public string answer;
    public bool isCorrect;
    public bool isSelected;
    public Button btn;
    public Image btnImage;
    public ManagerQuizAllChap manager;
    public bool mouseDown;
    public VerticalLayoutGroup vlayoutGroup;
    
    private TMP_Text btnText;
    private ColorBlock quizAnswerCB = GameColors.GetQuizAnswerColorBlock();
    private SoQuizConfig myQuizConfig;

    private void OnEnable()
    {
        if(btn == null)
        {
            btn = GetComponent<Button>();
        }
       
        if(myQuizConfig == null)
        {
            myQuizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
        }
       
        if(vlayoutGroup == null)
        {
            vlayoutGroup =  FindObjectOfType<VerticalLayoutGroup>();
        }

        SetupDesignAnswerText();
        SetupBtnDesign();

        if (gameObject.GetComponent<MouseChange>() == null) gameObject.AddComponent<MouseChange>();
        if (gameObject.GetComponent<ButtonHoverTriggers>() == null) gameObject.AddComponent<ButtonHoverTriggers>();
        if (gameObject.GetComponent<ReactOnlyOnInTranspartentParts>() == null) gameObject.AddComponent<ReactOnlyOnInTranspartentParts>();

        btn.onClick.AddListener(delegate { SelectThisAnswer(); });
        manager = FindObjectOfType<ManagerQuizAllChap>();
    }

    public void SelectThisAnswer()
    {
        manager.UpdateSelect(this.gameObject);
    }

    public void SetOrigParent(Transform trans)
    {
        origParent = trans;
        transform.SetParent(origParent);
    }

    public void Show()
    {
        btnText.text = answer;
        btn.gameObject.SetActive(true);
    }

    private void SetupBtnDesign()
    {
        btn.transform.SetParent(vlayoutGroup.transform);
        btn.transform.localScale = Vector3.one;
        btn.transform.localPosition = new Vector3(btn.transform.localPosition.x,
                                                        btn.transform.localPosition.y,
                                                        0);
        int myPadding = 50;
        
        if(btn.GetComponent<HorizontalLayoutGroup>() == null)
        {
            btn.GetComponent<Button>().gameObject.AddComponent<HorizontalLayoutGroup>();
        }
        
        HorizontalLayoutGroup hlz = GetComponent<Button>().GetComponent<HorizontalLayoutGroup>();
        hlz.padding.left = myPadding;
        hlz.padding.right = myPadding;
        hlz.childAlignment = TextAnchor.MiddleCenter;
        
        btnImage = btn.GetComponent<Image>();
        btnImage.sprite = myQuizConfig.btnSprite;
        btnImage.type = Image.Type.Sliced;
        btnImage.pixelsPerUnitMultiplier = 0.8f;
    }
    
    private void SetupDesignAnswerText()
    {
        btnText = btn.GetComponentInChildren<TMP_Text>();
        btnText.fontSize = 16;
        btnText.color = GameColors.defaultTextColor;
        btnText.font = myQuizConfig.font;
        btnText.fontMaterial = myQuizConfig.font.material;
        btnText.fontStyle = FontStyles.Normal;
    }

    public void SetSelected()
    {
        isSelected = true;
        ShowUnprovenSelect();
    }

    public void SetUnselected()
    {
        isSelected = false;
        ShowUnprovenUnselect();
    }

    public void ShowUnprovenSelect()
    {
        btnImage.color = quizAnswerCB.selectedColor;
        btnText.color = Color.white;
        btnText.fontStyle = FontStyles.SmallCaps | FontStyles.Bold;
    }

    public void ShowUnprovenUnselect()
    {
        btnImage.color = Color.white;
        btnText.color = Color.black;
        btnText.fontStyle = FontStyles.Normal;
    }

    public void TintGreen()
    {
        btnImage.color = Color.green;
        btnImage.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
        btnText.fontStyle = FontStyles.SmallCaps | FontStyles.Bold;
    }

    public void TintRed()
    {
        btnImage.color = Color.red;
        btnImage.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
        btnText.fontStyle = FontStyles.Normal;
    }

    public void TintWhite()
    {
        btnImage.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
        btnImage.color = Color.grey;
    }

    public void ShowIncorrectlyUnselected()
    {
        TintGreen();
    }

    public void ShowResult()
    {
        if(isCorrect && isSelected)
        {
            TintGreen();
        }
        else if (isCorrect && !isSelected)
        {
            TintGreen();
        }
        else if(!isCorrect && isSelected)
        {
            TintRed();
        }
        else if(!isCorrect && !isSelected)
        {
            TintWhite();
        }

        btn.interactable = false;
    }
}
