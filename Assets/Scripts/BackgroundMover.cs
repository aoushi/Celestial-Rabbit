using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] Renderer bgRenderer;
    [SerializeField] float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(moveSpeed * Time.deltaTime, 0);
    }
}
