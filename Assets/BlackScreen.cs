using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = GetComponent<Image>();
    }

    public void SetAlpha(float alpha)
    {
        var tempColor = blackScreen.color;
        tempColor.a = alpha;
        blackScreen.color = tempColor;
    }

}
