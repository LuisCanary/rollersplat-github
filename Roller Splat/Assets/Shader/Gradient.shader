﻿Shader "Custom/Gradient"
{
    Properties
    {
       _TopColor ("TopColor", Color)=(1,1,1,1)
	   _BottomColor("BottomColor",Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float4 screenPos;
        };
       
        fixed4 _TopColor;
		fixed4 _BottomColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

			float2 screenUV = (IN.screenPos.xy / IN.screenPos.w);
			screenUV.y = 1 - screenUV.y;
			fixed4 color = lerp(_TopColor, _BottomColor, screenUV.y);
			o.Albedo = color.rgb;
			o.Alpha = color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
