using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private const string noCharacterSprite = "noCharacterSprite";
    public SoCharacter characterConfigSO;

    SpeechBubble speechBubble;
    public Image characterImage;

    int previousStop = -1;
    public bool sole1ImgUpdated = false, s2ImgUpdated = false, s3ImgUpdated = false;
    public bool entryAreaUpdated = false;

    [Header("Assigned in Runtime")]
    [SerializeField] private SoGameIcons iconConfigSo;
    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        iconConfigSo = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
    }

    void Start()
    {
        //Aufbau: Name des Characters (Canvas, CanvasScaler Spline Move, this Script)
        //Über dieses Script ist alles andere erreichbar

        // Im Canvas die main cam setzen.
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        gameObject.GetComponent<Canvas>().worldCamera = mainCam;

        //Die Kinderholen und GOs speechBubble und Img von Character holen. 

        SetupElements();

        //setze speechbubble sprite
        speechBubble.PopulateBubbleSprites();

        //setzte default sprite für character
        if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01LongwallCutter)
        {
            characterImage.sprite = characterConfigSO.longwallCutterStandingTalking;
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01Mine)
        {
            ChangeCharacterImage(CoalmineStop.EntryArea);
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineIntro)
        {
            ChangeCharacterImage(CoalmineStop.Outside);
        }
    }

    public void SetupElements()
    {
        Component[] character = GetComponentsInChildren<Transform>(true);

        if (characterImage != null && speechBubble != null) return;

        for (int i = 0; i < character.Length; i++)
        {
            if (i == 1)
            {   //speechbubble
                speechBubble = character[i].GetComponent<SpeechBubble>();
                speechBubble.gameObject.SetActive(false);
            }
            if (i == 4)
            {   //Image von Character
                characterImage = character[i].GetComponent<Image>();
            }
        }
    }

    public void RotateCharacter(float yRotation)
    {
        gameObject.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void ChangeCharacterImage(CoalmineStop stop)
    {
        switch (stop)
        {
            case CoalmineStop.Outside:
                characterImage.GetComponent<Image>().sprite = characterConfigSO.outsideMineStandingSilient;
                break;
            case CoalmineStop.EntryArea:
                entryAreaUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.entryAreaStandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != noCharacterSprite) break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.entryAreaStandingTalking;
                break;
            case CoalmineStop.Sole1:
                sole1ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole1StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != noCharacterSprite) break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole1StandingTalking;
                break;
            case CoalmineStop.Sole2:
                s2ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole2StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != noCharacterSprite) break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole2StandingTalking;
                break;
            case CoalmineStop.Sole3:
                s3ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole3StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != noCharacterSprite) break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole3StandingTalking;
                break;
        }
    }

    private void Update()
    {
        //within cave
        //Update characterimage accordingly to cavestop; the character in s3 is the most dirty one, if the character reaches this sole no more updates happen and
        //updates only happen if the cave is moving down.
        if (s3ImgUpdated) return;

        bool stopChanged = previousStop != (int)runtimeData.currentCoalmineStop;
        bool caveDirectionIsDown = (int)CaveMovement.MoveDown == GameData.moveDirection;

        if (!entryAreaUpdated)
        {
            if (runtimeData.currentCoalmineStop == CoalmineStop.EntryArea)
            {
                ChangeCharacterImage(CoalmineStop.EntryArea);
                previousStop = (int)CoalmineStop.EntryArea;
            }
        }
        else if (!sole1ImgUpdated && (runtimeData.currentCoalmineStop == CoalmineStop.Sole1))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole1);
                previousStop = (int)CoalmineStop.Sole1;
            }
        }
        else if (!s2ImgUpdated && (runtimeData.currentCoalmineStop == CoalmineStop.Sole2))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole2);
                previousStop = (int)CoalmineStop.Sole2;
            }
        }
        else if (!s3ImgUpdated && (runtimeData.currentCoalmineStop == CoalmineStop.Sole3))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole3);
                previousStop = (int)CoalmineStop.Sole3;
            }
        }
    }
}
