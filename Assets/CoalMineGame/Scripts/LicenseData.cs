using UnityEngine;

[CreateAssetMenu(menuName ="LicenseData")]
public class LicenseData : ScriptableObject
{
    [TextArea(10, 10)]
    public string licenseText;
}
