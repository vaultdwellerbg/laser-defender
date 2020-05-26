using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material backgroundMaterial;
    Vector2 offset;

    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroundScrollSpeed);
    }

    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
