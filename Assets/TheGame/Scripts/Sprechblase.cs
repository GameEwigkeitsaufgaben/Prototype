using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Sprechblase : MonoBehaviour
{
    public Image sprechblaseImg;
    public Button sprechblaseInteraktion;
    public Sprite play, reload, talking;

    public bool vibrateSprechblase;

    private Vector3 initialPosition;
    //Vector3 directionOfShake = transform.forward;
    public float amplitude; // the amount it moves
    public float frequency; // the period of the earthquake

    public bool couroutineAllowed = true;

    private IEnumerator StartPulsingOld()
    {
        couroutineAllowed = false;
        Debug.Log("local scale " + transform.localScale);
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.025f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.025f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z + 0.025f, Mathf.SmoothStep(0f, 1f, i)))
                );
            yield return new WaitForSeconds(0.015f);
        }

        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            transform.localScale = new Vector3(
                (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.025f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.025f, Mathf.SmoothStep(0f, 1f, i))),
                (Mathf.Lerp(transform.localScale.z, transform.localScale.z - 0.025f, Mathf.SmoothStep(0f, 1f, i)))
                );
            yield return new WaitForSeconds(0.015f);
        }

        couroutineAllowed = true;
    }

    private IEnumerator StartPulsing()
    {
        couroutineAllowed = false;

        transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
        yield return new WaitForSeconds(1f);

        transform.localScale = transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
        yield return new WaitForSeconds(1f);

        couroutineAllowed = true;
    }
    
    private void VibrateUncontrolled() //https://forum.unity.com/threads/vibrating-the-gameobjects.536217/
    {
        var speed = 5.0f;
        var intensity = 0.1f;

        transform.localPosition = intensity * new Vector3(
            Mathf.PerlinNoise(speed * Time.time, 1),
            Mathf.PerlinNoise(speed * Time.time, 2),
            Mathf.PerlinNoise(speed * Time.time, 3));
    }

    private void Vibrate()
    {

        Debug.Log("in Transform: " + (-amplitude + Mathf.PingPong(frequency * Time.deltaTime, 2.0f * amplitude)));
        //transform.position = initialPosition + transform.up * (-amplitude + Mathf.PingPong(frequency * Time.deltaTime, 2.0f * amplitude));
        //Vector3 pos = initialPosition + Random.insideUnitSphere * amplitude;
        //Mathf.PingPong(frequency * Time.deltaTime, 2.0f * amplitude)
        Vector3 scale = gameObject.transform.localScale + transform.localScale * Mathf.PingPong(frequency, amplitude);
        
        transform.localScale = scale;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialPosition = transform.position; // store this to avoid floating point error drift
        initialPosition = transform.localPosition;
        if ((CurrentStop)GameData.currentStopSohle == CurrentStop.Einstieg)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vibrateSprechblase)
        {
            StartCoroutine("StartPulsing");
        }
    }
}
