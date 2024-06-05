using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public static float animationSpeed = 0.2f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
