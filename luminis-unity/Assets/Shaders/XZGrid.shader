Shader "Custom/XZGrid" {
    Properties {
        _GridColor ("Grid Color", Color) = (0.2, 0.2, 0.2, 1)
        _BackgroundColor ("Background", Color) = (1, 1, 1, 1)
        _LineWidth ("Line Width", Float) = 0.02
        _GridSpacing ("Grid Spacing", Float) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert;
            #pragma fragment frag;
            #include "UnityCG.cginc"

            fixed4 _GridColor;
            fixed4 _BackgroundColor;
            float _LineWidth;
            float _GridSpacing;

            struct appdata {
                float4 vertex : POSITION;
            }

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            }

            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float2 coord = i.worldPos.xz; / _GridSpacing;
                float2 grid = abs(frac(coord - 0.5) - 0.5) / fwidth(coord);
                float line = min(grid.x, grid.y);

            }

            ENDCG
        }
    }
}
