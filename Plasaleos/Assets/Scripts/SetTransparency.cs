using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTransparency : MonoBehaviour {

    [SerializeField] private float value = 1f;

    // Use this for initialization
    void Start()
    {
        Image[] image = GetComponentsInChildren<Image>();
        for (int i = 0; i < image.Length; i++)
        {
            image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, value);
        }
        Text[] text = GetComponentsInChildren<Text>();
        for (int i = 0; i < text.Length; i++)
        {
            text[i].color = new Color(text[i].color.r, text[i].color.g, text[i].color.b, value);
        }
    }
}
