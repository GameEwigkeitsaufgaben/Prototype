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

    public static Color defaultTextColor = new Color32(60,60,60,255);

    public static ColorBlock GetInteractionColorBlock() 
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = defaultInteractionColorNormal;
        uiInteractionColors.highlightedColor = defaultInteractionColorHighlighted;
        uiInteractionColors.pressedColor = defaultInteractionColorPresses;
        uiInteractionColors.selectedColor = uiInteractionColors.normalColor;
        uiInteractionColors.disabledColor = defaultInteractionColorDisabled;

        return uiInteractionColors;
    }

    public static ColorBlock GetQuizAnswerColorBlock()
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = defaultInteractionColorNormal;
        uiInteractionColors.highlightedColor = defaultInteractionColorHighlighted;
        uiInteractionColors.pressedColor = defaultInteractionColorPresses;
        //uiInteractionColors.selectedColor = new Color32(227, 86, 60, 255); rot
        uiInteractionColors.selectedColor = new Color32(59, 59, 59, 255);
        uiInteractionColors.disabledColor = new Color32(255,255,255,255);

        return uiInteractionColors;
    }

    public static ColorBlock GetPostColorBlock()
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = Color.white;
        uiInteractionColors.highlightedColor = defaultInteractionColorNormal;
        uiInteractionColors.pressedColor = defaultInteractionColorPresses;
        uiInteractionColors.selectedColor = uiInteractionColors.normalColor;
        uiInteractionColors.disabledColor = new Color32(226, 89, 57, 255);

        return uiInteractionColors;
    }

    public static ColorBlock GetOverlayColorBlock()
    {
        ColorBlock uiInteractionColors = ColorBlock.defaultColorBlock;
        uiInteractionColors.normalColor = Color.white;
        uiInteractionColors.highlightedColor = defaultInteractionColorNormal;
        uiInteractionColors.pressedColor = defaultInteractionColorPresses;
        uiInteractionColors.selectedColor = uiInteractionColors.normalColor;
        uiInteractionColors.disabledColor = new Color32(255, 255, 255, 255);

        return uiInteractionColors;
    }

}
