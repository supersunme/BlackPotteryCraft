﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

    Shader "Unlit/ImageAlpha"
    {
    	Properties
    	{
    		_MainTex ("Texture", 2D) = "white" {}

			_AlphaLX("RangeAlphaLY",Float) = 1
			_AlphaRX("RangeAlphaRY",Float) = 1
    		_AlphaTY("RangeAlphaTY",Float) = 1
    		_AlphaBY("RangeAlphaBY",Float) = 0

			_MaskTop("MaskUp",Range(0,1)) = 0
			_MaskButton("MaskDown",Range(0,1)) = 0


    		_AlphaPower("Power",Float) = 0 //透明度变化范围
    	}
    	SubShader
    	{
    		Tags { "RenderType"="Transparent"  }
    		Blend SrcAlpha OneMinusSrcAlpha
    		Cull back
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
                   // float4 worldpos : SV_POSITION;

    				float2 uv : TEXCOORD1;
    				float4 vertex : SV_POSITION;
    			};
     
    			sampler2D _MainTex;
    			float4 _MainTex_ST;
    			float _AlphaPower;
    			sampler2D _AlphaTex;

				float _AlphaLX;
				float _AlphaRX;
    			float _AlphaTY;
    			float _AlphaBY;
				float _MaskTop;
    			float _MaskButton;

    			v2f vert (appdata v)
    			{
    				v2f o;

                  // o.vertex = mul(unity_ObjectToWorld, v.vertex);

    				o.vertex = UnityObjectToClipPos(v.vertex);
    				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    				return o;
    			}
    			//此方法取自Unity默认Sprite的Shader
    			fixed4 SampleSpriteTexture (float2 uv)
    			{
    				fixed4 color = tex2D (_MainTex, uv);
     
    #if ETC1_EXTERNAL_ALPHA
    				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
    				color.a = tex2D (_AlphaTex, uv).r;
    #endif //ETC1_EXTERNAL_ALPHA
     
    				return color;
    			}
     
    			fixed4 frag (v2f i) : SV_Target
    			{

    				fixed4 col = SampleSpriteTexture(i.uv);

    				//四个方向只是对坐标的取值和正反方向不同，原理一致
    				fixed alphalx = col.a * lerp(1,_AlphaPower,(_AlphaLX-i.uv.x));
    				col.a = saturate(lerp(alphalx,col.a,step(_AlphaLX,i.uv.x)));
     
    				fixed alpharx = col.a * lerp(1,_AlphaPower,(i.uv.x-_AlphaRX));
    				col.a = saturate(lerp(col.a,alpharx,step(_AlphaRX,i.uv.x)));
     
    				fixed alphaby = col.a * lerp(1,_AlphaPower,(_AlphaBY-i.uv.y));
    				col.a = saturate(lerp(alphaby,col.a,step(_AlphaBY,i.uv.y)));
     
    				fixed alphaty = col.a * lerp(1,_AlphaPower,(i.uv.y-_AlphaTY));
    				col.a = saturate(lerp(col.a,alphaty,step(_AlphaTY,i.uv.y)));

					if(i.uv.y>_MaskTop )
					 col.a=0;
					 if(i.uv.y<_MaskButton )
					 col.a=0;


    				return col;
    			}
    			ENDCG
    		}
    	}
    }