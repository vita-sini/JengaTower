Shader "Custom/EdgesAndCorners"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (0,0,0,1)
        _LineThickness ("Line Thickness", Range(0.001, 0.1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
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
            float4 _LineColor;
            float _LineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Толщина линий
                float lineThickness = _LineThickness;

                // Проверка расстояния до границ (верх, низ, лево, право)
                float edgeTop = smoothstep(1.0 - lineThickness, 1.0, uv.y);
                float edgeBottom = smoothstep(lineThickness, 0.0, uv.y);
                float edgeLeft = smoothstep(lineThickness, 0.0, uv.x);
                float edgeRight = smoothstep(1.0 - lineThickness, 1.0, uv.x);

                // Объединение всех граней
                float edgeMask = edgeTop + edgeBottom + edgeLeft + edgeRight;

                // Основной цвет текстуры
                fixed4 texColor = tex2D(_MainTex, uv);

                // Добавление черных линий на грани (и углы включены)
                return lerp(texColor, _LineColor, edgeMask);
            }
            ENDCG
        }
    }
}
