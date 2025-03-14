Shader "Unlit/distortion"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {} 
        _Color ("Color", Color) = (1,1,1,1)   
        _Speed ("Distortion Speed", Range(0,5)) = 1
        _Amount ("Distortion Amount", Range(0,1)) = 0.5
        _ScrollSpeed ("Scroll Speed", Range(0,5.0)) = 1
        _TextureScale ("Texture Scale", Range(0.1, 5)) = 1.0 // Set in StairsTilling Script
        _AlphaTexture("Transparent Mask", 2D) = "white" {}
        _AlphaScale ("Transparent Tiling", Range(0,5.0)) = 1
    }

    SubShader {
        Tags { "Queue" = "Transparent"
                "RenderType" = "Transparent"
                }
        Blend SrcAlpha One
        ZWrite On
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
        
            // Input Structure
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Output Stucture
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 alphaPos : TEXCOORD1;
            };

            // Shader Variables
            sampler2D _MainTex;
            sampler2D _AlphaTexture;     
            float4 _MainTex_ST;
            float4 _Color;
            float _Speed;
            float _Amount;
            half _ScrollSpeed;
            float _TextureScale;
            uniform float4x4 _WorldToView;
            half _AlphaScale;


            // Vertex shader
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                //Transparent Mask Scrolling
                o.alphaPos = mul(_WorldToView, v.vertex);
                o.alphaPos.y += _Time.y * _ScrollSpeed;

                return o;
            }

            // Distortion function based on time and position
            float2 distortion(float2 uv, float time) {
                float frequency = 15.0;
                float amplitude = 0.1;
                float distortion = sin(uv.y * frequency + time) * amplitude;

                // Apply only horizontal
                return float2(distortion , 0);
            }

            // Fragment shader
            float4 frag (v2f i) : SV_Target {
                float scrollTime = _Time.y * _ScrollSpeed;
                float2 scrolledUV = i.uv + float2(0, scrollTime);  // Scroll the texture vertically

                // Add tilling
                float xTiling = 0.6 * _TextureScale;
                scrolledUV.x *= xTiling;

                // Distort Calculation
                float time = _Time.y * _Speed;
                float2 distortionAmount = distortion(i.uv, time);

                // Offset the UV coordinates with the distortion
                float2 distortedUV = scrolledUV + distortionAmount * _Amount;

                float4 color = tex2D(_MainTex, distortedUV);
                float4 alphaColor = tex2D(_AlphaTexture, i.alphaPos.xy*_AlphaScale);

                color.a = alphaColor.a;

                float4 finalColor = _Color * color;

                return finalColor;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
