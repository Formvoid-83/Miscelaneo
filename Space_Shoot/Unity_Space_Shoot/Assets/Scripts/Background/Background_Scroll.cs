using UnityEngine;

public class Background_Scroll : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Renderer  bgRenderer;


    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
