using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandFullImageMove : MonoBehaviour
{

    public Image BG;
    public Text TXT;
    public float moveSpeed;
    public float currentAlpha;
    public float currentAlphaGoneSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, currentAlpha);
        TXT.color = new Color(TXT.color.r, TXT.color.g, TXT.color.b, currentAlpha);
        currentAlpha -= currentAlphaGoneSpeed * Time.deltaTime;

        if (currentAlpha < 0f)
        {
            Destroy(gameObject);
        }
    }
}
