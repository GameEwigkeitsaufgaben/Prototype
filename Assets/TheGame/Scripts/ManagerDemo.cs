using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDemo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject myPrefab;
    public GameObject player;
    public List<GameObject> mob;
    public Canvas mobCanvas;


    void Start()
    {
        //CreateDemoPeopleAroundPoint(20, player.transform.position, 43f);
        //CreateDemoPeopleAroundPoint(10, player.transform.position, 40f);
        //CreateDemoPeopleAroundPoint(7, player.transform.position, 35f);
        //CreateDemoPeopleAroundPoint(5, player.transform.position, 30f);
    }

    public void CreateDemoPeopleAroundPoint(int num, Vector3 point, float radius)
    {

        //CreateGO in canvas
        GameObject tmpObj = new GameObject(radius+"radius");
        tmpObj.transform.SetParent(mobCanvas.transform);

        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var enemy = Instantiate(myPrefab, spawnPos, Quaternion.identity) as GameObject;

            /* Rotate the enemy to face towards player */
            enemy.transform.LookAt(point);

            /* Adjust height */
            enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));

            enemy.transform.SetParent(tmpObj.transform);
        }

        //tmpObj.transform.rotation = Quaternion.Euler(0,Random.Range(0f,360f),0);
    }
}
