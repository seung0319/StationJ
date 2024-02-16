using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
{


    public Text text; // 변경할 텍스트 오브젝트를 Inspector에서 할당해주세요.
    public Text text2;
    public Color newColor; // 변경할 색상을 Inspector에서 할당해주세요.
    public Color newColor2;

    public void ChangeTextColor()
    {
        text.color = newColor;
        text2.color = newColor2;
    }
}
