using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGravityType : MonoBehaviour {

    public bool color = false;
    public Color color2;

    public void SetGravity(bool g)
    {
        GameManager.instance.isDragGravity = g;
    }

    private void Update()
    {
        if (GameManager.instance.isDragGravity == color)
        {
            gameObject.GetComponent<Image>().color = color2;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1,1,1,1);
        }
    }
}
