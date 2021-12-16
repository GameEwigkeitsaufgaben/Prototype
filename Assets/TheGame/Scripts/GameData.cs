using UnityEngine;

public static class GameData
{
    public static int chapterOneUnlocked = 0;
    public static int chapterTwoUnlocked = 0;
    public static int chapterThreeUnlocked = 0;
    public const string sceneLicense = "Scene01License";
    public const string sceneLicenseDeclined = "Scene02LicenseDeclined";
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
