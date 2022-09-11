using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroll : MonoBehaviour
{
    [SerializeField] private float scrollX = 0.5f;
    [SerializeField] private float scrollY = 0.5f;

    private RawImage image;
    private Vector2 offset;

    private void Start()
    {
        image = GetComponent<RawImage>();
        offset = new Vector2(scrollX, scrollY);
    }

    private void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + offset * Time.deltaTime, image.uvRect.size);
    }
}
