Shader "Outlines/HoverOutline"
{
    Properties
    {
        _Thickness("Thickness", float) = 1
        _Color("Color", Color) = (1,1,1,1)
         _Glow ("Intensity", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        Pass
        {
               Name "Outline"

               Cull Front

               HLSLPROGRAM
               #pragma prefer_hlslcc gles
               #pragma exclude_renderers d3d11_9x

               #pragma vertex Vertex
               #pragma fragment Fragment

               #include "Outlines.hlsl"

               ENDHLSL
        }
    }
}
