Shader "Custom/Uncertainty2DEllipseInv" {
    Properties {
        _Radius("Radius", Range(1, 10)) = 10
        _Alpha("Alpha", Range(1, 100)) = 1
        _Beta("Beta", Range(1, 100)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert;
            #pragma fragment frag;
            #include "UnityCG.cginc"

            float _Radius;
            float _Alpha;
            float _Beta;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float2 screenSize = _ScreenParams.xy;
                float2 xy = (i.uv - 0.5) * screenSize;

                float alpha = _Radius + _Alpha;
                float beta = _Radius + _Beta;
                float t = xy.x * xy.x / (alpha * alpha) + xy.y * xy.y / (beta * beta);

                float c = 1 / t;

                return float4(c, c, c, 1);
            }
            ENDCG
        }
    }
}
