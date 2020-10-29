using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsDummy : MonoBehaviour
{
    [SerializeField]
    private Image adsDummyImage = default;



    private float time = 1;   
    private Color startColor;
    private Color endColor;
    private bool go;
    private float t;
    private bool destroy;
    private System.Action adShow;
    private System.Action adHide;

    public void StartDummy(System.Action adShow, System.Action adHide = default)
    {
        adsDummyImage.gameObject.SetActive(true);
        startColor = new Color(0, 0, 0, 0);
        endColor = new Color(0, 0, 0, 1);
        adsDummyImage.color = startColor;
        t = 0;
        go = true;
        destroy = false;
        this.adShow = adShow;
        this.adHide = adHide;
    }

    void Update()
    {
        if (go)
        {
            adsDummyImage.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime / time;
            if (t >= 1)
            {
                if (destroy)
                {
                    
                    adHide?.Invoke();
                    adsDummyImage.gameObject.SetActive(false);
                    go = false;
                }
                else
                {
                    adShow();
                    t = 0;
                    Color temp = endColor;
                    endColor = startColor;
                    startColor = temp;
                    destroy = true;
                }
            }
        }
    }
}
