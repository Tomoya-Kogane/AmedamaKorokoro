Shader "ShaderCamera/DefaultScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // レンダリング設定
        Cull Back
        ZWrite Off
        ZTest Always

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
                float3 worldpos : WORLD_POS;
            };

            // 頂点シェーダー
            v2f vert (appdata v)
            {
                v2f o;
                // UV座標
                o.uv = v.uv;
                // 2D座標
                o.vertex = UnityObjectToClipPos(v.vertex);
                // ワールド座標
                o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            sampler2D _MainTex;

            // フラグメントシェーダー
            fixed4 frag (v2f i) : SV_Target
            {
                // ピクセルの色を取得
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
