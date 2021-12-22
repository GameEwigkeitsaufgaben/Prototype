using UnityEngine;

public class CaveColliderBottom : MonoBehaviour
{
    public Cave cave;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" .." +other.name);
        if (other.name == "TriggerSohle1")
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
