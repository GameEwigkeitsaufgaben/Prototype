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

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);    
    }

    // Start is called before the first frame update
    void Start()
    {
        runtimeData.trainArrived = false;
        trainComesStarted = false;
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
        if (!trainComesStarted && runtimeData.viewPointS3passed)
        {
            trainComesStarted = true;
            Invoke("StartTrain" , 4);
        }
    }
}
