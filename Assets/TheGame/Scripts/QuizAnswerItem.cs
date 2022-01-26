using UnityEngine;
using UnityEngine.UI;

public class QuizAnswerItem
{
    public string questionIdentifier;
    public string answer;
    public int answerIdentifier;
    public bool isCorrect;
    public Button btn;
    public bool buttonSelected = false;
    VerticalLayoutGroup buttonGroup;

    public QuizAnswerItem() {}

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

    public void CreateButton(VerticalLayoutGroup parent)
    {
        var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
        newButton.transform.SetParent(parent.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x,
                                                            newButton.transform.localPosition.y,
                                                            0);
        newButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("neon_square_orange");
        newButton.GetComponentInChildren<Text>().fontSize = 20;
        newButton.GetComponentInChildren<Text>().text = answer;
        btn = newButton.GetComponent<Button>();
        btn.onClick.AddListener(ToogleSelected);
        btn.gameObject.SetActive(false);
    }

    private void ToogleSelected()
    {
        buttonSelected = !buttonSelected;

        Color selectColor = Color.green;
        if (buttonSelected)
        {
            selectColor = Color.green;
        }
        else
        {
            selectColor = Color.white;
        }

        ColorBlock cb = btn.colors;
        cb.normalColor = selectColor;
        cb.normalColor = selectColor;
        cb.highlightedColor = selectColor;
        cb.selectedColor = selectColor;
        btn.colors = cb;
    }

    public void ShowResult()
    {
        if (!isCorrect)
        {
            Color c = btn.GetComponent<Image>().color;
            btn.GetComponent<Image>().color = new Vector4(c.r, c.g, c.b, 0.2f);
            btn.GetComponentInChildren<Text>().color = new Vector4(c.r, c.g, c.b, 0.2f);
        }
    }
}
