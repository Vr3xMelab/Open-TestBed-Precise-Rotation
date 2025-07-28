Shader "Unlit / VISIBLE"  //render the objects behind  
{
    Properties
    {
       _MainTex("Texture", 2D) = "white" {}
       _Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5 // Threshold to control the cutout effect
    }
        SubShader
       {
           Tags { "Queue" = "Transparent" } // Render queue set to Transparent
           LOD 100

           Pass
           {
               Cull Off
               ZWrite Off // controls depth buffer: leave this off if transparent, on if solid
               ZTest Always

               CGPROGRAM
               #pragma vertex vert
               #pragma fragment frag
               #include "UnityCG.cginc"

               struct appdata
               {
                   float4 vertex : POSITION;
                   float2 uv : TEXCOORD0;
               };

               struct v2f
               {
                   float2 uv : TEXCOORD0;
                   float4 vertex : SV_POSITION;
               };

               sampler2D _MainTex;
               float4 _MainTex_ST;
               float _Cutoff;

               v2f vert(appdata v)
               {
                   v2f o;
                   o.vertex = UnityObjectToClipPos(v.vertex);
                   o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                   return o;
               }

               fixed4 frag(v2f i) : SV_Target
               {
                   // Sample the texture
                   fixed4 col = tex2D(_MainTex, i.uv);

               // Apply cutout effect based on alpha channel
               clip(col.a - _Cutoff); // Discard fragments with alpha below _Cutoff

               return col;
           }
           ENDCG
       }
       }
}
