namespace Assets.Scripts.Water
{
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class WaterPropertyBlockSetter : MonoBehaviour
    {
        private static readonly int WaterColor = Shader.PropertyToID("_WaterColor");
        private static readonly int BorderColor = Shader.PropertyToID("_BorderColor");
        private static readonly int Tiling = Shader.PropertyToID("_Tiling");
        private static readonly int DistTiling = Shader.PropertyToID("_DistTiling");
        private static readonly int MoveDirection = Shader.PropertyToID("_MoveDirection");
        private static readonly int WaterTex = Shader.PropertyToID("_WaterTex");
        private static readonly int DistTex = Shader.PropertyToID("_DistTex");
        private static readonly int TextureVisibility = Shader.PropertyToID("_TextureVisibility");
        private static readonly int WaterHeight = Shader.PropertyToID("_WaterHeight");
        private static readonly int WaterDeep = Shader.PropertyToID("_WaterDeep");
        private static readonly int WaterDepth = Shader.PropertyToID("_WaterDepth");
        private static readonly int WaterMinAlpha = Shader.PropertyToID("_WaterMinAlpha");
        private static readonly int BorderWidth = Shader.PropertyToID("_BorderWidth");
        
        [SerializeField] private Renderer[] waterRenderers;

        [Space]
        [SerializeField] private Color waterColor;
        [SerializeField] private Texture waterTex;
        [SerializeField] private Vector2 waterTile;
        [Range(0, 1)][SerializeField] private float textureVisibility;

        [Space]
        [SerializeField] private Texture distortionTex;
        [SerializeField] private Vector2 distortionTile;

        [Space]
        [SerializeField] private float waterHeight;
        [SerializeField] private float waterDeep;
        [Range(0, 0.1f)][SerializeField] private float waterDepthParam;
        [Range(0, 1)][SerializeField] private float waterMinAlpha;

        [Space]
        [SerializeField] private Color borderColor;
        [Range(0, 1)][SerializeField] private float borderWidth;

        [Space]
        [SerializeField] private Vector2 moveDirection;

        private MaterialPropertyBlock materialPropertyBlock;

        public void Awake()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            SetUpPropertyBlock(materialPropertyBlock);

            if (waterRenderers != null)
            {
                for (var i = 0; i < waterRenderers.Length; i++)
                {
                    waterRenderers[i].SetPropertyBlock(materialPropertyBlock);
                }
            }
        }

#if UNITY_EDITOR
        public void OnEnable()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            SetUpPropertyBlock(materialPropertyBlock);
        }

        public void Update()
        {
            SetUpPropertyBlock(materialPropertyBlock);

            if (waterRenderers != null)
            {
                for (var i = 0; i < waterRenderers.Length; i++)
                {
                    waterRenderers[i].SetPropertyBlock(materialPropertyBlock);
                }
            }
        }
#endif

        private void SetUpPropertyBlock(MaterialPropertyBlock propertyBlock)
        {
            propertyBlock.SetColor(WaterColor, waterColor);
            propertyBlock.SetColor(BorderColor, borderColor);

            propertyBlock.SetVector(Tiling, waterTile);
            propertyBlock.SetVector(DistTiling, distortionTile);
            propertyBlock.SetVector(MoveDirection, new Vector4(moveDirection.x, 0f, moveDirection.y, 0f));

            if (waterTex != null)
            {
                propertyBlock.SetTexture(WaterTex, waterTex);
            }

            if (distortionTex != null)
            {
                propertyBlock.SetTexture(DistTex, distortionTex);
            }

            propertyBlock.SetFloat(TextureVisibility, textureVisibility);
            propertyBlock.SetFloat(WaterHeight, waterHeight);
            propertyBlock.SetFloat(WaterDeep, waterDeep);
            propertyBlock.SetFloat(WaterDepth, waterDepthParam);
            propertyBlock.SetFloat(WaterMinAlpha, waterMinAlpha);
            propertyBlock.SetFloat(BorderWidth, borderWidth);
        }
    }
}
