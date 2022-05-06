using UnityEngine;
using UnityEngine.UI;
using SWS;

public class ManagerArriveTrain : MonoBehaviour
{
    public PathManager pathArrivingTrain;
    public splineMove splineMove;
    public AudioSource audioSrcTrain;

    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);    
    }

    // Start is called before the first frame update
    void Start()
    {
        runtimeData.trainArrived = false;
    }

    public void StartTrain()
    {
        splineMove.StartMove();
    }

    public void StartTrainAudio()
    {
        audioSrcTrain.Play();
    }

    public void SetTrainArraived()
    {
        runtimeData.trainArrived = true;
        audioSrcTrain.Stop();
    }

    private void Update()
    {
        if (runtimeData.viewPointS3passed)
        {
            runtimeData.viewPointS3passed = false;
            Invoke("StartTrain" , 4);
        }
    }
}
