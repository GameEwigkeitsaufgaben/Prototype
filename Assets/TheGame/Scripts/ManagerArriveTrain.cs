using UnityEngine;
using UnityEngine.UI;
using SWS;



public class ManagerArriveTrain : MonoBehaviour
{
    public PathManager pathArrivingTrain;
    public splineMove splineMove;
    public AudioSource audioSrcTrain;

    private SoChapOneRuntimeData runtimeData;
    private bool trainComesStarted;
    private SoSfx sfx;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
    }

    // Start is called before the first frame update
    void Start()
    {
        runtimeData.trainArrived = false;
        trainComesStarted = false;
        audioSrcTrain.clip = sfx.coalmineIncomingTrain;
    }

    public void StartTrain()
    {
        splineMove.StartMove();
    }

    public void StartTrainAudio()
    {
        audioSrcTrain.Play();
    }

    public void SetTrainArrived()
    {
        runtimeData.trainArrived = true;
        audioSrcTrain.Stop();
    }

    private void Update()
    {
        if (!trainComesStarted && runtimeData.sole3GebaeudeDone && runtimeData.sole3BewetterungDone)
        {
            trainComesStarted = true;
            Invoke("StartTrain" , 2.0f);
        }
    }
}
