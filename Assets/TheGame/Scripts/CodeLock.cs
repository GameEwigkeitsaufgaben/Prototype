/* Handle Enter Chapter with Adminpin and Enterpin
 * Adminpin --> all posts are unlocked
 * Enterpin --> pin to start game with partly locked posts
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CodeLock : MonoBehaviour
{
    //Display single numbers in nbr1-nbr4;
    public TMP_Text nbr1, nbr2, nbr3, nbr4;
    public Chapters lockForChapter;
    
    private SoChaptersRuntimeData chaptersRuntimeData;
    
    void Start()
    {
        chaptersRuntimeData = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);

        if (chaptersRuntimeData.ch1GeneralUnlocked && lockForChapter == Chapters.Ch01General)
        {
            LoadCodeInDisplay(Chapters.Ch01General);
            return;
        }

        ResetCode();
    }

    public void CheckCode()
    {
        string code = nbr1.text + nbr2.text + nbr3.text + nbr4.text;

        if (!int.TryParse(code, out int result))
        {
            ResetCode();
            return;
        }

        switch (lockForChapter)
        {
            case Chapters.Ch01General:
                if (!(code == GameData.pwdChapterOne || code == GameData.pwdAdminChapterOne))
                {
                    ResetCode();
                    return;
                }

                if(code == GameData.pwdAdminChapterOne)
                {
                    chaptersRuntimeData.progressWithAdminCh1 = true;
                }
          
                chaptersRuntimeData.ch1GeneralUnlocked = true;
                SceneManager.LoadScene(GameScenes.ch01InstaMain);
                break;
            case Chapters.Ch02Grubenwasser:
                if (!(code == GameData.pwdChapterTwo || code == GameData.pwdAdminChapterTwo))
                {
                    ResetCode();
                    return;
                }

                if (code == GameData.pwdAdminChapterTwo)
                {
                    chaptersRuntimeData.progressWithAdminCh2 = true;
                }

                chaptersRuntimeData.ch2GrubenwasserUnlocked = true;
                SceneManager.LoadScene(GameScenes.ch02InstaMain);
                break;
        }
    }
    public void EnterNumber(int nbr)
    {
        string emptyNbr = "-";
   
        if (nbr1.text == emptyNbr) nbr1.text = nbr.ToString();
        else if (nbr2.text == emptyNbr) nbr2.text = nbr.ToString();
        else if (nbr3.text == emptyNbr) nbr3.text = nbr.ToString();
        else if (nbr4.text == emptyNbr) nbr4.text = nbr.ToString();
    }

    public void LoadCodeInDisplay(Chapters chapter)
    {
        if (chaptersRuntimeData.progressWithAdminCh1)
        {
            switch (chapter)
            {
                case Chapters.Ch01General:
                    SetCodeDisplay("1", "2", "1", "2");
                    break;
                case Chapters.Ch02Grubenwasser:
                    SetCodeDisplay("2", "3", "2", "3");
                    break;
            }

            return;
        }

        switch (chapter)
        {
            case Chapters.Ch01General:
                SetCodeDisplay("1", "1", "1", "1");
                break;
            case Chapters.Ch02Grubenwasser:
                SetCodeDisplay("2", "2", "2", "2");
                break;
        }
    }

    public void ResetCode()
    {
        SetCodeDisplay("-", "-", "-", "-");
    }

    private void SetCodeDisplay(string d1, string d2, string d3, string d4)
    {
        nbr1.text = d1;
        nbr2.text = d2;
        nbr3.text = d3;
        nbr4.text = d4;
    }
}
