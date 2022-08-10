using UnityEngine;
using UnityEngine.UI;

public class ResizePrefab : MonoBehaviour
{
    public float width, height;

    void Start()
    {
        if(gameObject.GetComponent<Image>() != null)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(width,height);
        }
    }
}
