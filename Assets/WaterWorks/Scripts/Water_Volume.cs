using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Note: Removed 'UnityEngine.Experimental.Rendering.Universal' as RTHandle is now stable
// and often available directly under UnityEngine.Rendering.Universal.

public class Water_Volume : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RTHandle sourceHandle; // Changed to RTHandle for consistency
        private Material _material;
        private RTHandle _temporaryRT;
        private const string ProfilerTag = "Water Volume Pass";
        private static readonly int TemporaryColorTextureID = Shader.PropertyToID("_TemporaryColourTexture");

        public CustomRenderPass(Material mat)
        {
            _material = mat;
        }
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.depthBufferBits = 0;
            descriptor.colorFormat = RenderTextureFormat.Default;
            RenderingUtils.ReAllocateIfNeeded(ref _temporaryRT, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: TemporaryColorTextureID.ToString());
        }
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            ConfigureTarget(sourceHandle);
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material == null) return;

            if (renderingData.cameraData.cameraType != CameraType.Reflection)
            {
                CommandBuffer commandBuffer = CommandBufferPool.Get(ProfilerTag);
                Blit(commandBuffer, sourceHandle, _temporaryRT, _material, 0);
                Blit(commandBuffer, _temporaryRT, sourceHandle);
                context.ExecuteCommandBuffer(commandBuffer);
                CommandBufferPool.Release(commandBuffer);
            }
        }
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            _temporaryRT?.Release();
        }
    }

    [System.Serializable]
    public class _Settings
    {
        public Material material = null;
        public RenderPassEvent renderPass = RenderPassEvent.AfterRenderingSkybox;
    }
    public _Settings settings = new _Settings();
    CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        if (settings.material == null)
        {
            settings.material = (Material)Resources.Load("Water_Volume");
        }
        m_ScriptablePass = new CustomRenderPass(settings.material);
        m_ScriptablePass.renderPassEvent = settings.renderPass;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.sourceHandle = renderer.cameraColorTargetHandle;
        renderer.EnqueuePass(m_ScriptablePass);
    }
}