Shader "Unlit/RGBtoYIQ"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_LUTTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{

		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			static half4 kRGBToYPrime = half4 (0.299, 0.587, 0.114, 0.0);
			static half4  kRGBToI     = half4 (0.596, -0.275, -0.321, 0.0);
			static half4  kRGBToQ     = half4 (0.212, -0.523, 0.311, 0.0);

			static half4  kYIQToR   = half4 (1.0, 0.956, 0.621, 0.0);
			static half4  kYIQToG   = half4 (1.0, -0.272, -0.647, 0.0);
			static half4  kYIQToB   = half4 (1.0, -1.107, 1.704, 0.0);
			uniform float Tuning_Strength;
			uniform float GradingRes;
			uniform sampler2D _MainTex = sampler_state
			{
				Texture = (_MainTex);
				MinFilter = Point;
				MagFilter = Point;
				MipFilter = None;
				AddressU = CLAMP;
				AddressV = CLAMP;
			};
			uniform sampler2D _LUTTex = sampler_state
			{
				Texture = (_LUTTex);
				MinFilter = Point;
				MagFilter = Point;
				MipFilter = None;
				AddressU = CLAMP;
				AddressV = CLAMP;
			};
			struct vsNTSCOut
			{
				half4 pos		: POSITION;
				half2 uv		: TEXCOORD0;
			};
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			vsNTSCOut vert (appdata v)
			{
				vsNTSCOut o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.uv;
				return o;
			}

			half4 DoPost(half4 InColor){
				
				// Some of this stuff could be precomputed offline for perf.
				half Res = GradingRes;
				half RcpRes = 1.0f / Res;
				half ResSq = Res*Res;
				half RcpResSq = 1.0f / ResSq;
				half HalfRcpResSq = 0.5f * RcpResSq;
				half HalfRcpRes = 0.5f * RcpRes;
				
				half ResMinusOne = Res - 1.0f;
				half ResMinusOneOverRes = ResMinusOne / Res;
				half ResMinusOneOverResSq = ResMinusOne / ResSq;
				
				half2 graduv_lo;
				half2 graduv_hi;
				
				half b_lo = floor(InColor.b * ResMinusOne);
				half b_hi = ceil(InColor.b * ResMinusOne);
				half b_alpha = ((InColor.b * ResMinusOne) - b_lo);
				
				graduv_lo.x = (b_lo * RcpRes) + InColor.r * ResMinusOneOverResSq + HalfRcpResSq;
				graduv_lo.y = InColor.g * ResMinusOneOverRes + HalfRcpRes;
				
				graduv_hi.x = (b_hi * RcpRes) + InColor.r * ResMinusOneOverResSq + HalfRcpResSq;
				graduv_hi.y = InColor.g * ResMinusOneOverRes + HalfRcpRes;
				
				half4 postgrad_lo = tex2D(_LUTTex, graduv_lo);
				half4 postgrad_hi = tex2D(_LUTTex, graduv_hi);
				
				half4 postgrad = lerp(postgrad_lo, postgrad_hi, b_alpha);


				// half   YPrime  = dot(InColor, kRGBToYPrime);
				// half   I      = dot(InColor, kRGBToI);
				// half   Q      = dot(InColor, kRGBToQ);

				// float   hue     = atan2 (Q, I);
    			// float   chroma  = sqrt (I * I + Q * Q);

				// half4    yIQ   = half4 (YPrime, I, Q, 0.0);

				// half4 OutColor = half4(0, 0, 0, 1);
				// OutColor.r = dot(yIQ, kYIQToR);
				// OutColor.g = dot(yIQ, kYIQToG);
				// OutColor.b = dot(yIQ, kYIQToB);
				return lerp(InColor, postgrad, Tuning_Strength);
			}

			
			half4 frag (vsNTSCOut i) : SV_Target
			{
				return DoPost(tex2D(_MainTex, i.uv));
			}

			ENDCG
		}
	}
}
