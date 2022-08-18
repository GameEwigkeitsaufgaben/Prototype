using UnityEngine;
using TMPro;

public class Molecule : MonoBehaviour
{
    [SerializeField] private TMP_Text 
        verhaltnisformel, 
        verhaeltnisformelTG, 
        molName;
    
    public void SetColor(Color c)
    {
        verhaeltnisformelTG.color = c;
        verhaltnisformel.color = c;
        molName.color = c;
    }
}
