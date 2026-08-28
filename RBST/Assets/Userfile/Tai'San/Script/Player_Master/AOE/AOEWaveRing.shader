Shader "Custom/AOEWaveRing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Radius", Range(0,1)) = 0
        _RingWidth ("Ring Width", Range(0.01,1)) = 0.1
        _Alpha ("Alpha", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            fixed4 _Color;
            float _Radius;
            float _RingWidth;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 centered = i.uv - float2(0.5, 0.5);
                float dist = length(centered) * 2;

                // 距離に応じてUVを揺らす(波打つ効果)
                float wobble = sin(dist * 20 - _Time.y * 10) * 0.02;
                float distortedDist = dist + wobble;

                float ring = 1 - smoothstep(0, _RingWidth, abs(distortedDist - _Radius));
                fixed4 col = _Color;
                col.a *= ring * _Alpha;
                return col;
            }
            ENDCG
        }
    }
}