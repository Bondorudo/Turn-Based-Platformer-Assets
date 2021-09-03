using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    // Create new text object
    public static DamagePopup Create(Vector3 position, int damageAmount, int fontSize)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.prefabDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.SetUp(damageAmount, fontSize);

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

    public void SetUp(int damage, int fontSize)
    {
        textMesh.SetText(damage.ToString());

        textMesh.fontSize = fontSize;

        textColor = textMesh.color;
        disappearTimer = 3;

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
