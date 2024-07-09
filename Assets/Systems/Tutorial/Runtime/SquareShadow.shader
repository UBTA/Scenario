Shader "SquareShadow"
{
    Properties
    {
        _RectCenterSize ("Center / Size", Vector) = (1,1,1,1)
        _CornerRadius ("Corner Radius", Range(0, 1)) = 0.05
        _EdgeBlur ("Edge Blur", Range(0, 0.5)) = 0.01
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)
    }
    
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _RectCenterSize;
            float _CornerRadius;
            float _EdgeBlur;
            float4 _MainColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv.z = _ScreenParams.y / _ScreenParams.x;
                o.uv.y *= o.uv.z;
                return o;
            }

            float sdRoundRect(float2 uv, float2 size, float radius)
            {
                uv = abs(uv) - size + float2(radius, radius);
                return length(max(uv, 0.0)) - radius;
            }


            float4 frag(v2f i) : SV_Target
            {
                _RectCenterSize.y *= i.uv.z;
                float2 uv = i.uv * 2 - _RectCenterSize.xy;
                uv -= float2(1,i.uv.z);
                float d = sdRoundRect(uv, float2(_RectCenterSize.z,_RectCenterSize.w * i.uv.z), _CornerRadius);
                float a = 1.0 - smoothstep(0.0, _EdgeBlur, d);
                return float4(_MainColor.rgb, _MainColor.a * (1-a));
            }
            ENDCG
        }
    }
}