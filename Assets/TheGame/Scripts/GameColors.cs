using UnityEngine;

public static class GameColors
{
    const int r = 254;
    const int g = 198;
    const int b = 75;
    const int a = 255;

    public static Color defaultInteractionColorNormal = new Color32(r,g,b,a);
    public static Color defaultInteractionColorHighlighted = new Color32(r-10, g-10, b-10, a);
    public static Color defaultInteractionColorPresses = new Color32(r - 55, g - 55, b - 55, a);
    public static Color defaultInteractionColorDisabled = new Color32(r - 55, g - 55, b - 55, a-128);

    public static Color discInteractonDoneColor = new Color32(0, 197, 26, a);

}
