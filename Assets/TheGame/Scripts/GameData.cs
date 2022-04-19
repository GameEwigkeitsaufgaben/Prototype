using UnityEngine;

public static class GameData
{
    //Name of objects in Folder "Resources" 
    public const string NameConfigSfx = "SfxConfig";
    public const string NameRuntimeStoreData = "RuntimeExchangeData";
    public const string NameConfigMinerEquiment = "ConfigMinerEquipment";
    public const string NameConfigMuseum = "ConfigMuseum";
    public const string NameGameIcons = "Icons";

    public static int chapterOneUnlocked = 0;
    public static int chapterTwoUnlocked = 0;
    public static int chapterThreeUnlocked = 0;

    public static bool progressWithAdmin = false; //if start chapter with admin pwd true, else false;


    //Unlock Posts data; is Introvideo played as a whole, than next post is to unlock
    public static bool introVideoPlayedOnce = false;
    public static bool restorIntroVideo = false;
    public static string overlayToLoad = "";
    public static bool liftIntroDadDone = false;
    public static bool liftBtnsEnabled = false;
    public static float cavePosX = 0f;
    public static float cavePosY = 0f;
    public static float cavePosZ = 0f;
    public static bool moveCave = false;
    public static int moveDirection = -1; //-1 for moving down, +1 for moving up
    public static int nbrSchacht = 1;

    public static int currentStopSohle = -1; //Based on enum Coalminestop defined in Cave, -1 = unset
    public static int sohleToReload = -1; //Based on enum Coalminestop defined in Cave, -1 = unset
    public static float playerOffsetToAnkerObjX = 0;
    public static float playerOffsetToAnkerObjY = 0;
    public static float playerOffsetToAnkerObjZ = 0;
    public static float playerPositonXatS3Bahnsteig = 0;
    public static float playerPositonYatS3Bahnsteig = 0;
    public static float playerPositonZatS3Bahnsteig = 0;
    //public static bool scene1162LoadedOnce = false;
    public static bool rideIn = true;
    internal static bool sohle3IntroPlayedOnce = false;

    public static int quizChapterOnePoints = 0;

    internal static bool bubbleOnEnvy = false;
    internal static bool bubbleOnDad = false;
    internal static bool bubbleOnGeorg = false;
    internal static bool bubbleOnMuseumGuide = false;
    internal static bool quizFinished = false; //changed in QuizManager.LoadNextQuestion();

    public static string lawNotiz = "Hinweis: Alle Inhalte dieses Internetangebotes sind urheberrechtlich geschützt. Das Urheberrecht liegt, " +
        "soweit nicht ausdrücklich anders gekennzeichnet, bei Frau Susanne Meerwald-Stadler. Alle Rechte vorbehalten. Jede Art der Vervielfältigung, " +
        "Verbreitung, Vermietung, Verleihung, öffentlichen Zugänglichmachung oder andere Nutzung bedarf der ausdrücklichen, schriftlichen Zustimmung von Frau Susanne Meerwald-Stadler.  " +
        "Das Herunterladen der in diesem Internetangebot enthaltenen Informationen ist grundsätzlich nicht gestattet, sofern dies nicht ausdrücklich abweichend kenntlich gemacht wird.";


    public static void PrintState()
    {
        Debug.Log(
            "chapterOneUnlocked: " + chapterOneUnlocked + "\n" +
            "chaperTwoUnlocked: " + chapterTwoUnlocked + "\n" +
            "chapterThreeUnlocked: " + chapterThreeUnlocked + "\n" +
            "introVideoPlayedOnce: " + introVideoPlayedOnce
            );
    }

    public static int GetCurrentStop(CoalmineStop stop)
    {
        switch (stop)
        {
            case CoalmineStop.EntryArea: return 0;
            case CoalmineStop.Sole1: return 1;
            case CoalmineStop.Sole2: return 2;
            case CoalmineStop.Sole3: return 3;
            default: return -1;
        }
    }
}
