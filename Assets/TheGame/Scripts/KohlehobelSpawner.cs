using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohlehobelSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objToSpawn;
    public Transform spawnPoint;
    public float coolDown;
    public bool spawnerEnabled = false;
    private float timer;


    void Update()
    {
        timer += Time.deltaTime;

        if (spawnerEnabled && timer > coolDown)
        {
            GameObject clone = Instantiate(objToSpawn[Random.Range(0,objToSpawn.Length)],spawnPoint.position,Quaternion.identity);
            clone.AddComponent<SphereCollider>();
            clone.AddComponent<Rigidbody>();

            timer = 0;
        }
    }
}
