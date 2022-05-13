using UnityEngine;
using UnityEngine.UI;

public static class GameColors
{
    const int r = 254;
    const int g = 198;
    const int b = 75;
    const int a = 255;

    public static Color defaultInteractionColorNormal = new Color32(r,g,b,a);
    public static Color defaultInteractionColorHighlighted = new Color32(255, 255, 195, 255);
    public static Color defaultInteractionColorPresses = new Color32(r - 55, g - 55, b - 55, a);
    public static Color defaultInteractionColorDisabled = new Color32(r - 55, g - 55, b - 55, a-128);

    public static Color buzzerInteractionColor = new Color32(227, 86,60,255);

    public static Color discInteractonDoneColor = new Color32(0, 197, 26, a);

    public static Color showBackgroundInfoColor = new Color32(223, 202, 183, a);

    public static ColorBlock GetInteractionColorBlock() 
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = GameColors.defaultInteractionColorNormal;
        uiInteractionColors.highlightedColor = GameColors.defaultInteractionColorHighlighted;
        uiInteractionColors.pressedColor = GameColors.defaultInteractionColorPresses;
        uiInteractionColors.selectedColor = uiInteractionColors.normalColor;
        uiInteractionColors.disabledColor = GameColors.defaultInteractionColorDisabled;

        return uiInteractionColors;
    }

    public static ColorBlock GetPostColorBlock()
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = Color.white;
        uiInteractionColors.highlightedColor = GameColors.defaultInteractionColorNormal;
        uiInteractionColors.pressedColor = GameColors.defaultInteractionColorPresses;
        uiInteractionColors.selectedColor = uiInteractionColors.normalColor;
        uiInteractionColors.disabledColor = Color.white;

        return uiInteractionColors;
    }

}
