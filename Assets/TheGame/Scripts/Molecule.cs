using UnityEngine;
using TMPro;

public class Molecule : MonoBehaviour
{
    private bool done;

    [SerializeField] private TMP_Text 
        verhaltnisformel, 
        verhaeltnisformelTG, 
        ionen,
        molName;
    

    public void SetDone()
    {
        done = true;
    }

    public bool IsDone()
    {
        return done;
    }

    public void SetColor(Color c)
    {
        if (verhaeltnisformelTG != null) verhaeltnisformelTG.color = c;
        if (verhaltnisformel != null) verhaltnisformel.color = c;
        if (ionen!=null) ionen.color = c;
        if (molName != null) molName.color = c;
    }
}
