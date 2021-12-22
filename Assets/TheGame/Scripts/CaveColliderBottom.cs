using UnityEngine;

public class CaveColliderBottom : MonoBehaviour
{
    public Cave cave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerEinstieg")
        {
            cave.StopCave();
            cave.currentStop = CurrentStop.Einstieg;
        }
        Debug.Log(" .." +other.name);
        if (other.name == "TriggerSohle1")
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
