using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab = new GameObject();

    public void Show(string msg, Vector2 position, Quaternion rotation)
    {
        GameObject text = Instantiate(textPrefab, position, rotation);
        text.GetComponentInChildren<TextMesh>().text = msg;
        text.GetComponentInChildren<MeshRenderer>().sortingLayerName = "FloatingText";
    }
}
