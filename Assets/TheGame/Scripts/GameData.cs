using UnityEngine;

public static class GameData
{
    public static int chapterOneUnlocked = 0;
    public static int chapterTwoUnlocked = 0;
    public static int chapterThreeUnlocked = 0;
    public const string sceneMainMenu = "Scene03MainMenu";
    public const string sceneMainChapterOne = "Scene11InstaMain";
    public const string sceneMainChapterTwo = "Dummy";
    public const string sceneMainChapterThree = "Dummy";
    public const string sceneInstaMainChapterOne = "Scene11InstaMain";

    public static bool progressWithAdmin = false; //if start chapter with admin pwd true, else false;


    //Unlock Posts data; is Introvideo played as a whole, than next post is to unlock
    public static bool introPlayedOnce = false;
    public static bool restorIntroVideo = false;
    public static string overlayToLoad = "";
    public static bool liftIntroDadDone = false;
    public static bool liftBtnsEnabled = false;
    public static float cavePosX = 0f;
    public static float cavePosY = 0f;
    public static float cavePosZ = 0f;
    public static bool moveCave = false;
    //public static int moveDirection = -1; //-1 for moving down, +1 for moving up
    public static int nbrSchacht = 1;

    public static int sohleToReload = 0;
    public static float playerOffsetToAnkerObjX = 0;
    public static float playerOffsetToAnkerObjY = 0;
    public static float playerOffsetToAnkerObjZ = 0;
    public static bool scene1162LoadedOnce = false;

    public static void PrintState()
    {
        Debug.Log(
            "chapterOneUnlocked: " + chapterOneUnlocked + "\n" +
            "chaperTwoUnlocked: " + chapterTwoUnlocked + "\n" +
            "chapterThreeUnlocked: " + chapterThreeUnlocked + "\n" +
            "introPlayedOnce: " + introPlayedOnce
            );
    }
}
