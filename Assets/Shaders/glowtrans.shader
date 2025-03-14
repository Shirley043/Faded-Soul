Shader "Unlit/trans"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _EmissionTex ("Emission Texture (Glow)", 2D) = "white" {}
        _BaseGlowColor ("Base Glow Color", Color) = (1,1,1,1)
        _FresnelPower ("Fresnel Power", Range(0.1, 10)) = 2.0

        // Gradient Alpha
        _AlphaBot ("Bot Transparency", Range(0.0, 1.0)) = 0.1
        _AlphaTop ("Top Transparency", Range(0.0, 1.0)) = 0.5

        _GlowIntensity ("Glow Intensity", Range(0.0, 5.0)) = 2.0
        _GlowSpeed ("Glow Speed", Range(0.0, 2.0)) = 1.0

        // Wave effect
        _WaveAmplitude ("Wave Amplitude", Range(0.0, 1.0)) = 0.1
        _WaveFrequency ("Wave Frequency", Float) = 3.0
        _WaveSpeed ("Wave Speed", Float) = 2.0
        _MiddleBodyHeightMin ("Middle Body Min Height", Float) = 0.3
        _MiddleBodyHeightMax ("Middle Body Max Height", Float) = 0.5
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 worldNormal : TEXCOORD2;
                float gradientAlpha : TEXCOORD3; 
            };

            // Textures 
            sampler2D _MainTex;
            sampler2D _EmissionTex;
            float4 _BaseGlowColor;
            float _FresnelPower;
            float _AlphaBot; // Trans gradient Min
            float _AlphaTop;
            //float _Alpha;
            float _GlowIntensity;
            float _GlowSpeed;

            // Wave 
            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;
            float _MiddleBodyHeightMin;
            float _MiddleBodyHeightMax;

            v2f vert(appdata v)
            {
                v2f o;

                // World position of the vertex
                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

                // prevent zero effect
                float waveAmplitude = _WaveAmplitude > 0.001 ? _WaveAmplitude : 0.1;
                float waveFrequency = _WaveFrequency > 0.001 ? _WaveFrequency : 3.0;
                float waveSpeed = _WaveSpeed > 0.001 ? _WaveSpeed : 2.0;

                // Apply the wave for the middle body height range
                if (worldPos.y >= _MiddleBodyHeightMin && worldPos.y <= _MiddleBodyHeightMax)
                {
                    float wave = sin(v.vertex.y * waveFrequency + _Time.y * waveSpeed) * waveAmplitude;
                    
                    v.vertex.x += wave;
                }

                // Vertex 
                // Gradient Alpha
                float gradientFactor = saturate((worldPos.y - _MiddleBodyHeightMin) / (_MiddleBodyHeightMax - _MiddleBodyHeightMin));
                o.gradientAlpha = lerp(_AlphaBot, _AlphaTop, gradientFactor);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos.xyz);
                o.worldNormal = mul(unity_ObjectToWorld, float4(v.normal, 0.0)).xyz;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 baseColor = tex2D(_MainTex, i.uv);

                baseColor.a *= i.gradientAlpha;

                // Fresnel 
                float fresnel = pow(1.0 - saturate(dot(i.viewDir, normalize(i.worldNormal))), _FresnelPower);

                // glow
                fixed4 emission = tex2D(_EmissionTex, i.uv);

                // Color change 
                float time = _Time.y * _GlowSpeed; 
                float3 glowColor = _BaseGlowColor.rgb * (sin(time) * 0.5 + 0.5); 

                // Combine 
                fixed4 finalColor = baseColor + emission * fresnel * _GlowIntensity;
                finalColor.rgb += glowColor * fresnel * _GlowIntensity;

                finalColor.a = baseColor.a;

                return finalColor;
            }
            ENDCG
        }
    }
}
