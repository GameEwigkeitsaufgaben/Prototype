using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabParticlesystem : MonoBehaviour
{
    public ParticleSystem system;
    public GameObject prefab;

    [SerializeField]
    public ParticleSystem.Particle[] p;
    public float[] particleLifetime;
    public int length;
    public GameObject[] prefabArr;
    public float sizeModifier;

    private void Awake()
    {
        p = new ParticleSystem.Particle[system.main.maxParticles];
        prefabArr = new GameObject[p.Length];
        particleLifetime = new float[p.Length];
        for (int i = 0; i < p.Length; i++)
        {
            GameObject clone = Instantiate(prefab);
            prefabArr[i] = clone;
            prefabArr[i].SetActive(false);
            clone.transform.SetParent(this.transform);
        }

    }

    private void LateUpdate()
    {
        system.GetParticles(p);
        length = p.Length;

        

        for (int i = 0; i < p.Length; i++)
        {
            particleLifetime[i] = p[i].remainingLifetime;

            if (p[i].remainingLifetime > 0)
            {
                prefabArr[i].SetActive(true);
                prefabArr[i].transform.position = transform.TransformPoint(p[i].position);
                float size = p[i].GetCurrentSize(system) * sizeModifier;
                prefabArr[i].transform.localScale = new Vector3(size, size, size);

            }
            else
            {
                prefabArr[i].SetActive(false);
            }
        }
    }
}
