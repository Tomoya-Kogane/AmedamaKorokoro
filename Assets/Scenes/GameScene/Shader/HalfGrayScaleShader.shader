Shader "ShaderCamera/HalfGrayScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // �����_�����O�ݒ�
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

            // ���_�V�F�[�_�[
            v2f vert (appdata v)
            {
                v2f o;
                // UV
                o.uv = v.uv;
                // 2D���W
                o.vertex = UnityObjectToClipPos(v.vertex);
                // ���[���h���W
                o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            sampler2D _MainTex;

            // �t���O�����g�V�F�[�_�[
            fixed4 frag (v2f i) : SV_Target
            {
                // �s�N�Z���̐F���擾
                fixed4 col = tex2D(_MainTex, i.uv);

                // �O���[�X�P�[���̋��E����g���ɂ���
                float wave = sin(60 *  (i.uv.y + _Time)) / 100 + 0.5;

                // �������̐F���O���[�X�P�[��(����)
                if (i.uv.x <= wave){
                    float gray = dot(col.rgb, fixed3(0.30, 0.59, 0.11));
                    col = fixed4(gray, gray, gray, col.a);
                }

                return col;
            }
            ENDCG
        }
    }
}
