using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
{


    public Text text; // ������ �ؽ�Ʈ ������Ʈ�� Inspector���� �Ҵ����ּ���.
    public Text text2;
    public Color newColor; // ������ ������ Inspector���� �Ҵ����ּ���.
    public Color newColor2;

    public void ChangeTextColor()
    {
        text.color = newColor;
        text2.color = newColor2;
    }
}
