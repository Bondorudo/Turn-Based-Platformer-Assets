using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    // Create new text object
    public static FloatingText Create(Vector3 position, string message, int fontSize, float disappearTime)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.prefabDamagePopup, position, Quaternion.identity);

        FloatingText damagePopup = damagePopupTransform.GetComponent<FloatingText>();
        damagePopup.SetUp(message, fontSize, disappearTime);

        return damagePopup;
    }
    private static int sortingOrder;

    TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void SetUp(string msg, int fontSize, float timeToDisappear)
    {
        textMesh.SetText(msg.ToString());

        textMesh.fontSize = fontSize;

        textColor = textMesh.color;
        disappearTimer = timeToDisappear;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        float moveYSpeed = 1;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.time;
        if (disappearTimer <= 0)
        {
            // Start Disappearing
            float disappearSpeed = 3;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
