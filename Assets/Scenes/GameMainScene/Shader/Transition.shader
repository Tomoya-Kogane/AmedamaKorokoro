Shader "Unlit/Transition"
{
    Properties
    {
        [PerRendererDat] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color)      = (1,1,1,1)
        _Alpha ("Time", Range(0,1)) = 0
    }
    SubShader
    {
        Tags
        { 
            // オブジェクト描画順（アルファ値合成）
            "Queue"="Transparent" 
            // 透過描画
            "RenderType"="Transparent" 
            // プレビュー表示（板）
            "PreviewType"="Plane"
        }

        // 全面表示
        Cull Off
        // 深度バッファ無効
        ZWrite Off
        // 深度テストの設定（GUI設定値）
        ZTest[unity_GUIZTestMode]
        // 透過設定
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half alpha = tex2D(_MainTex, i.uv).a;
                alpha = saturate(alpha + (_Alpha * 2 - 1));
                return fixed4(_Color.r, _Color.g, _Color.b, alpha);
            }
            ENDCG
        }
    }
}
