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
            // �I�u�W�F�N�g�`�揇�i�A���t�@�l�����j
            "Queue"="Transparent" 
            // ���ߕ`��
            "RenderType"="Transparent" 
            // �v���r���[�\���i�j
            "PreviewType"="Plane"
        }

        // �S�ʕ\��
        Cull Off
        // �[�x�o�b�t�@����
        ZWrite Off
        // �[�x�e�X�g�̐ݒ�iGUI�ݒ�l�j
        ZTest[unity_GUIZTestMode]
        // ���ߐݒ�
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
