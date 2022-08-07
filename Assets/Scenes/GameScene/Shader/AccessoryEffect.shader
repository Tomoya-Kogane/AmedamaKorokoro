Shader "Sprites/AccessoryEffect"
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

        // ���ߐݒ�
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
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
                float3 worldpos : WORLD_POS;
            };

            // ���_�V�F�[�_�[
            v2f vert (appdata v)
            {
                v2f o;
                // UV���W
                o.uv = v.uv;
                // 2D���W
                o.vertex = UnityObjectToClipPos(v.vertex);
                // ���[���h���W
                o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                return o;
            }

            sampler2D _MainTex;

            // �s�N�Z���V�F�[�_�[
            fixed4 frag (v2f i) : SV_Target
            {
                // �s�N�Z���̐F���擾
                fixed4 col = tex2D(_MainTex, i.uv);

                // �`��̋��E����ݒ�i�g���j
                float wave = sin(60 *  ((i.worldpos.y / 10) + _Time + 5)) / 100;

                // �`��̋��E���𒴂����ꍇ�A�A���t�@�l��ZERO��ݒ�i��\���j
                if (((_WorldSpaceCameraPos.x / 18) + wave) < (i.worldpos.x / 18))
                {
                    col = fixed4(col.r, col.g, col.b, 0.0f);
                }

                return col;
            }
            ENDCG
        }
    }
}
