Shader "Unlit/dissolveimage"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        half _FillAmount;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate transparency based on fill amount
            half transparency = _FillAmount;

            // Apply transparency
            o.Alpha = 1.0 - transparency;

            // Sample texture
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}