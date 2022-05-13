using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Chapters
{
    Ch01General = 1,
    Ch02Grubenwasser = 2,
    Ch03Ewigkeitsaufgabe = 3
}

public class ManagerOverviewChapters : MonoBehaviour
{
    [SerializeField] Button credits;
    [SerializeField] TMP_Text lawNotice;

    private void Start()
    {
        ColorBlock interactiveButton = GameColors.GetInteractionColorBlock();
        credits.colors = interactiveButton;

        lawNotice.text = GameData.lawNotiz;
    }
}
