using UnityEngine;

namespace Modules
{
    public class BotTest : MonoBehaviour
    {
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private Material smileFaceMaterial;
        [SerializeField] private Material sadFaceMaterial;
        [SerializeField] private Material pokerFaceMaterial;
        [SerializeField] private Material angryFaceMaterial;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                meshRenderer.material = smileFaceMaterial;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                meshRenderer.material = sadFaceMaterial;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                meshRenderer.material = pokerFaceMaterial;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                meshRenderer.material = angryFaceMaterial;
            }
        }
    }
}