using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizePass : ScriptableRenderPass
{
    private PixelizeFeature.CustomPassSettings settings;
    private Material material;
    private int pixelScreenHeight, pixelScreenWidth;

    // RTHandles for color and pixel buffers
    private RTHandle colorBuffer;
    private RTHandle pixelBuffer;
    
    // Shader property IDs
    private static readonly int PixelBufferID = Shader.PropertyToID("_PixelBuffer");
    private static readonly int BlockCountID = Shader.PropertyToID("_BlockCount");
    private static readonly int BlockSizeID = Shader.PropertyToID("_BlockSize");
    private static readonly int HalfBlockSizeID = Shader.PropertyToID("_HalfBlockSize");

    public PixelizePass(PixelizeFeature.CustomPassSettings settings)
    {
        this.settings = settings;
        this.renderPassEvent = settings.renderPassEvent;
        
        // Create material if null
        if (material == null)
        {
            material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
            if (material == null)
            {
                Debug.LogError("Failed to create Pixelize material");
            }
        }
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        // Get camera color buffer
        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;
        
        // Calculate pixel dimensions
        pixelScreenHeight = settings.screenHeight;
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

        // Set material properties
        material.SetVector(BlockCountID, new Vector2(pixelScreenWidth, pixelScreenHeight));
        material.SetVector(BlockSizeID, new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        material.SetVector(HalfBlockSizeID, new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        // Create pixel buffer descriptor
        var descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.width = pixelScreenWidth;
        descriptor.height = pixelScreenHeight;
        descriptor.depthBufferBits = 0;
        descriptor.msaaSamples = 1;

        // Allocate pixel buffer
        RenderingUtils.ReAllocateIfNeeded(ref pixelBuffer, descriptor, 
            FilterMode.Point, 
            TextureWrapMode.Clamp, 
            name: "PixelBufferID");
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Get command buffer from pool
        CommandBuffer cmd = CommandBufferPool.Get();
        
        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
        {
            // First pass: color buffer to pixel buffer with material
            Blitter.BlitCameraTexture(cmd, colorBuffer, pixelBuffer, material, 0);
            
            // Second pass: pixel buffer back to color buffer
            Blitter.BlitCameraTexture(cmd, pixelBuffer, colorBuffer);
        }

        // Execute and release command buffer
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException(nameof(cmd));
        
        // Release the pixel buffer
        if (pixelBuffer != null)
        {
            pixelBuffer.Release();
        }
    }

    // Cleanup for when pass is disposed
    public void Dispose()
    {
        pixelBuffer?.Release();
        CoreUtils.Destroy(material);
    }
}