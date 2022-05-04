// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//	Animated Galaxy Skybox Shader by:
//	Bartosz Kulesza
//	http://code-phi.com
//	http://u3d.as/wER
//	https://www.facebook.com/CodePhi/
//
//	Noise functions by:
//	Brian Sharpe
//	https://github.com/BrianSharpe
//
//	Modified for CG

Shader "AGS/AGS Skybox" 
{
	Properties
	{
		_SkyColor("Sky Color", Color) = (0.019, 0.019, 0.019)

		_NebulaDensity("Nebula Density", Range(0, 1)) = 0.75
		_NebulaOffset("Nebula Seed", Range(0, 1000)) = 0
		_NebulaAnimSpeed("Animation Speed", Range(0, 0.5)) = 0.15
		_NebulaColor("Nebula Color", Color) = (0.184, 0.447, 0.701)

		_CloudDensity("Cloud Density", Range(0, 10)) = 8
		_CloudVisibility("Cloud Visibility", Range(1, 10)) = 7
		_CloudSharpness("Cloud Sharpness", Range(0.5, 1.5)) = 1
		_CloudOffset("Cloud Seed", Range(0, 1000)) = 0
		_CloudAnimSpeed("Animation Speed", Range(0, 0.5)) = 0.1
		_CloudColor("Clouds Color", Color) = (0, 0.956, 1)

		_TrailComplexity("Trail Complexity", Range(0.5, 3)) = 2
		_TrailVisibility("Trail Visibility", Range(0, 0.9)) = 0.4
		_TrailSize("Trail Size", Range(0, 1)) = 0.5
		_TrailOffset("Trail Seed", Range(0, 1000)) = 0
		_TrailAnimSpeed("Animation Speed", Range(0, 2)) = 0.7
		_TrailColor("Trail Color", Color) = (0.384, 0.458, 0.827)

		_StarsAmount("Stars Amount", Range(0, 1)) = 0.7
		_StarsLightRandomness("Brightness Variation", Range(0, 1)) = 0.5
		_StarsSize("Stars Size", Range(0, 1)) = 0.6
		_StarsColorVariation("Stars Color Variation", Range(0, 1)) = 0.6
		_StarsBlinkingSpeed("Stars Blinking Speed", Range(0, 1)) = 0.3
		_StarsColorOne("Stars Color One", Color) = (0.674, 0.725, 0.850)
		_StarsColorTwo("Stars Color Two", Color) = (0.4, 0.15, 0.25)
	}

	SubShader
	{
		Tags
		{ 
			"Queue" = "Background" 
		}

		Pass
		{
			ZWrite Off
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile AGS_USE_NEBULA_ON AGS_USE_NEBULA_OFF
			#pragma multi_compile AGS_USE_CLOUDS_ON AGS_USE_CLOUDS_OFF
			#pragma multi_compile AGS_USE_TRAILS_ON AGS_USE_TRAILS_OFF
			#pragma multi_compile AGS_USE_STARS_ON AGS_USE_STARS_OFF

			float4 _SkyColor;

			float _NebulaDensity;
			float _NebulaOffset;
			float _NebulaAnimSpeed;
			float4 _NebulaColor;

			float _CloudDensity;
			float _CloudVisibility;
			float _CloudSharpness;
			float _CloudOffset;
			float _CloudAnimSpeed;
			float4 _CloudColor;

			float _TrailComplexity;
			float _TrailVisibility;
			float _TrailSize;
			float _TrailOffset;			
			float _TrailAnimSpeed;
			float4 _TrailColor;

			float _StarsAmount;
			float _StarsLightRandomness;
			float _StarsSize;
			float _StarsColorVariation;
			float _StarsBlinkingSpeed;
			float4 _StarsColorOne;
			float4 _StarsColorTwo;

			struct vertexInput 
			{
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct vertexOutput 
			{
				float4 vertex : SV_POSITION;
				float3 texcoord : TEXCOORD0;
			};

			float4 FAST32_hash_3D_Cell(float3 gridcell)
			{
				const float2 OFFSET = float2(50.0, 161.0);
				const float DOMAIN = 69.0;
				const float4 SOMELARGEFLOATS = float4(635.298681, 682.357502, 668.926525, 588.255119);
				const float4 ZINC = float4(48.500388, 65.294118, 63.934599, 63.279683);

				gridcell.xyz = gridcell - floor(gridcell * (1.0 / DOMAIN)) * DOMAIN;
				gridcell.xy += OFFSET.xy;
				gridcell.xy *= gridcell.xy;
				return frac((gridcell.x * gridcell.y) * (1.0 / (SOMELARGEFLOATS + gridcell.zzzz * ZINC)));
			}

			void FAST32_2_hash_4D(float4 gridcell,
				out float4 z0w0_hash_0,
				out float4 z0w0_hash_1,
				out float4 z0w0_hash_2,
				out float4 z0w0_hash_3,
				out float4 z1w0_hash_0,
				out float4 z1w0_hash_1,
				out float4 z1w0_hash_2,
				out float4 z1w0_hash_3,
				out float4 z0w1_hash_0,
				out float4 z0w1_hash_1,
				out float4 z0w1_hash_2,
				out float4 z0w1_hash_3,
				out float4 z1w1_hash_0,
				out float4 z1w1_hash_1,
				out float4 z1w1_hash_2,
				out float4 z1w1_hash_3)
			{
				const float4 OFFSET = float4(16.841230, 18.774548, 16.873274, 13.664607);
				const float DOMAIN = 69.0;
				const float4 SOMELARGEFLOATS = float4(56974.746094, 47165.636719, 55049.667969, 49901.273438);
				const float4 SCALE = float4(0.102007, 0.114473, 0.139651, 0.084550);

				gridcell = gridcell - floor(gridcell * (1.0 / DOMAIN)) * DOMAIN;
				float4 gridcell_inc1 = step(gridcell, float4(DOMAIN - 1.5, DOMAIN - 1.5, DOMAIN - 1.5, DOMAIN - 1.5)) * (gridcell + 1.0);

				gridcell = (gridcell * SCALE) + OFFSET;
				gridcell_inc1 = (gridcell_inc1 * SCALE) + OFFSET;
				gridcell *= gridcell;
				gridcell_inc1 *= gridcell_inc1;

				float4 x0y0_x1y0_x0y1_x1y1 = float4(gridcell.x, gridcell_inc1.x, gridcell.x, gridcell_inc1.x) * float4(gridcell.yy, gridcell_inc1.yy);
				float4 z0w0_z1w0_z0w1_z1w1 = float4(gridcell.z, gridcell_inc1.z, gridcell.z, gridcell_inc1.z) * float4(gridcell.ww, gridcell_inc1.ww);

				float4 hashval = x0y0_x1y0_x0y1_x1y1 * z0w0_z1w0_z0w1_z1w1.xxxx;
				z0w0_hash_0 = frac(hashval * (1.0 / SOMELARGEFLOATS.x));
				z0w0_hash_1 = frac(hashval * (1.0 / SOMELARGEFLOATS.y));
				z0w0_hash_2 = frac(hashval * (1.0 / SOMELARGEFLOATS.z));
				z0w0_hash_3 = frac(hashval * (1.0 / SOMELARGEFLOATS.w));
				hashval = x0y0_x1y0_x0y1_x1y1 * z0w0_z1w0_z0w1_z1w1.yyyy;
				z1w0_hash_0 = frac(hashval * (1.0 / SOMELARGEFLOATS.x));
				z1w0_hash_1 = frac(hashval * (1.0 / SOMELARGEFLOATS.y));
				z1w0_hash_2 = frac(hashval * (1.0 / SOMELARGEFLOATS.z));
				z1w0_hash_3 = frac(hashval * (1.0 / SOMELARGEFLOATS.w));
				hashval = x0y0_x1y0_x0y1_x1y1 * z0w0_z1w0_z0w1_z1w1.zzzz;
				z0w1_hash_0 = frac(hashval * (1.0 / SOMELARGEFLOATS.x));
				z0w1_hash_1 = frac(hashval * (1.0 / SOMELARGEFLOATS.y));
				z0w1_hash_2 = frac(hashval * (1.0 / SOMELARGEFLOATS.z));
				z0w1_hash_3 = frac(hashval * (1.0 / SOMELARGEFLOATS.w));
				hashval = x0y0_x1y0_x0y1_x1y1 * z0w0_z1w0_z0w1_z1w1.wwww;
				z1w1_hash_0 = frac(hashval * (1.0 / SOMELARGEFLOATS.x));
				z1w1_hash_1 = frac(hashval * (1.0 / SOMELARGEFLOATS.y));
				z1w1_hash_2 = frac(hashval * (1.0 / SOMELARGEFLOATS.z));
				z1w1_hash_3 = frac(hashval * (1.0 / SOMELARGEFLOATS.w));
			}

			float Falloff_Xsq_C1(float xsq)
			{
				xsq = 1.0 - xsq;
				return xsq*xsq;
			}

			float4 Interpolation_C2(float4 x)
			{
				return x * x * x * (x * (x * 6.0 - 15.0) + 10.0);
			}

			float Perlin4D(float4 P)
			{
				float4 Pi = floor(P);
				float4 Pf = P - Pi;
				float4 Pf_min1 = Pf - 1.0;

				float4 lowz_loww_hash_0, lowz_loww_hash_1, lowz_loww_hash_2, lowz_loww_hash_3;
				float4 highz_loww_hash_0, highz_loww_hash_1, highz_loww_hash_2, highz_loww_hash_3;
				float4 lowz_highw_hash_0, lowz_highw_hash_1, lowz_highw_hash_2, lowz_highw_hash_3;
				float4 highz_highw_hash_0, highz_highw_hash_1, highz_highw_hash_2, highz_highw_hash_3;
				FAST32_2_hash_4D(
					Pi,
					lowz_loww_hash_0, lowz_loww_hash_1, lowz_loww_hash_2, lowz_loww_hash_3,
					highz_loww_hash_0, highz_loww_hash_1, highz_loww_hash_2, highz_loww_hash_3,
					lowz_highw_hash_0, lowz_highw_hash_1, lowz_highw_hash_2, lowz_highw_hash_3,
					highz_highw_hash_0, highz_highw_hash_1, highz_highw_hash_2, highz_highw_hash_3);

				lowz_loww_hash_0 -= 0.49999;
				lowz_loww_hash_1 -= 0.49999;
				lowz_loww_hash_2 -= 0.49999;
				lowz_loww_hash_3 -= 0.49999;
				highz_loww_hash_0 -= 0.49999;
				highz_loww_hash_1 -= 0.49999;
				highz_loww_hash_2 -= 0.49999;
				highz_loww_hash_3 -= 0.49999;
				lowz_highw_hash_0 -= 0.49999;
				lowz_highw_hash_1 -= 0.49999;
				lowz_highw_hash_2 -= 0.49999;
				lowz_highw_hash_3 -= 0.49999;
				highz_highw_hash_0 -= 0.49999;
				highz_highw_hash_1 -= 0.49999;
				highz_highw_hash_2 -= 0.49999;
				highz_highw_hash_3 -= 0.49999;

				float4 grad_results_lowz_loww = rsqrt(lowz_loww_hash_0 * lowz_loww_hash_0 + lowz_loww_hash_1 * lowz_loww_hash_1 + lowz_loww_hash_2 * lowz_loww_hash_2 + lowz_loww_hash_3 * lowz_loww_hash_3);
				grad_results_lowz_loww *= (float2(Pf.x, Pf_min1.x).xyxy * lowz_loww_hash_0 + float2(Pf.y, Pf_min1.y).xxyy * lowz_loww_hash_1 + Pf.zzzz * lowz_loww_hash_2 + Pf.wwww * lowz_loww_hash_3);

				float4 grad_results_highz_loww = rsqrt(highz_loww_hash_0 * highz_loww_hash_0 + highz_loww_hash_1 * highz_loww_hash_1 + highz_loww_hash_2 * highz_loww_hash_2 + highz_loww_hash_3 * highz_loww_hash_3);
				grad_results_highz_loww *= (float2(Pf.x, Pf_min1.x).xyxy * highz_loww_hash_0 + float2(Pf.y, Pf_min1.y).xxyy * highz_loww_hash_1 + Pf_min1.zzzz * highz_loww_hash_2 + Pf.wwww * highz_loww_hash_3);

				float4 grad_results_lowz_highw = rsqrt(lowz_highw_hash_0 * lowz_highw_hash_0 + lowz_highw_hash_1 * lowz_highw_hash_1 + lowz_highw_hash_2 * lowz_highw_hash_2 + lowz_highw_hash_3 * lowz_highw_hash_3);
				grad_results_lowz_highw *= (float2(Pf.x, Pf_min1.x).xyxy * lowz_highw_hash_0 + float2(Pf.y, Pf_min1.y).xxyy * lowz_highw_hash_1 + Pf.zzzz * lowz_highw_hash_2 + Pf_min1.wwww * lowz_highw_hash_3);

				float4 grad_results_highz_highw = rsqrt(highz_highw_hash_0 * highz_highw_hash_0 + highz_highw_hash_1 * highz_highw_hash_1 + highz_highw_hash_2 * highz_highw_hash_2 + highz_highw_hash_3 * highz_highw_hash_3);
				grad_results_highz_highw *= (float2(Pf.x, Pf_min1.x).xyxy * highz_highw_hash_0 + float2(Pf.y, Pf_min1.y).xxyy * highz_highw_hash_1 + Pf_min1.zzzz * highz_highw_hash_2 + Pf_min1.wwww * highz_highw_hash_3);

				float4 blend = Interpolation_C2(Pf);
				float4 res0 = grad_results_lowz_loww + (grad_results_lowz_highw - grad_results_lowz_loww) * blend.wwww;
				float4 res1 = grad_results_highz_loww + (grad_results_highz_highw - grad_results_highz_loww) * blend.wwww;
				res0 = res0 + (res1 - res0) * blend.zzzz;
				blend.zw = float2(1.0, 1.0) - blend.xy;
				return dot(res0, blend.zxzx * blend.wwyy);
			}

			float PerlinNormal(float4 p, int octaves, float4 offset, float frequency, float amplitude, float lacunarity, float persistence)
			{
				float sum = 0;
				for (int i = 0; i < octaves; i++)
				{
					float h = 0;
					h = Perlin4D((p + offset) * frequency);
					sum += h*amplitude;
					frequency *= lacunarity;
					amplitude *= persistence;
				}
				return saturate(sum * 0.5 + 0.5);
			}

			float PerlinBillowed(float4 p, int octaves, float4 offset, float frequency, float amplitude, float lacunarity, float persistence)
			{
				float sum = 0;
				for (int i = 0; i < octaves; i++)
				{
					float h = 0;
					h = abs(Perlin4D((p + offset) * frequency));
					sum += h*amplitude;
					frequency *= lacunarity;
					amplitude *= persistence;
				}
				return sum;
			}

			float PerlinRidged(float4 p, int octaves, float4 offset, float frequency, float amplitude, float lacunarity, float persistence, float ridgeOffset)
			{
				float sum = 0;
				for (int i = 0; i < octaves; i++)
				{
					float h = 0;
					h = 0.5 * (ridgeOffset - abs(4 * Perlin4D((p + offset) * frequency)));
					sum += h*amplitude;
					frequency *= lacunarity;
					amplitude *= persistence;
				}
				return saturate(sum * 0.5 + 0.5);
			}

			float Stars3D(float3 P, float probability_threshold, float max_dimness, float two_over_radius)
			{
				float3 Pi = floor(P);
				float3 Pf = P - Pi;

				float4 hash = FAST32_hash_3D_Cell(Pi);
				float VALUE = 1.0 - max_dimness * hash.z;

				Pf *= two_over_radius;
				Pf -= (two_over_radius - 1.0);
				Pf += hash.xyz * (two_over_radius - 2.0);
				return (hash.w < probability_threshold) ? (Falloff_Xsq_C1(min(dot(Pf, Pf), 1.0)) * VALUE) : 0.0;
			}

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.texcoord = input.texcoord;
				return output;
			}

			fixed4 frag(vertexOutput input) : COLOR
			{
				float4 color = _SkyColor;
				float4 uv = float4((0.5 + (input.texcoord / 2)), 0);

#ifdef AGS_USE_NEBULA_ON
				float nebula = PerlinBillowed(float4(uv.xyz, _Time.x * _NebulaAnimSpeed), 4, float4(0, 0, 0, _NebulaOffset), 3.5, 0.8, 1.8, 0.5);
				nebula = pow(nebula, -4 * _NebulaDensity + 5.0);
				color += nebula * _NebulaColor;
#endif

#ifdef AGS_USE_CLOUDS_ON
				float cloudsNormal = PerlinNormal(float4(uv.xyz, 0), 6, float4(0, 0, 0, _CloudOffset), 1.3, _CloudSharpness, 3.0, 0.8);
				float cloudsBillowed = PerlinBillowed(float4(uv.xyz, _Time.x * _CloudAnimSpeed), 4, float4(0, 0, 0, _CloudOffset), 11, 1.01, 1.56, 0.57);
				cloudsNormal = pow(cloudsNormal, 10 - _CloudVisibility);
				cloudsBillowed = pow(cloudsBillowed, 10 - _CloudDensity);
				float clouds = cloudsNormal * cloudsBillowed;
				color += clouds * _CloudColor;
#endif

#ifdef AGS_USE_TRAILS_ON
				float trailNoise = PerlinNormal(float4(uv.xyz, _Time.x * _TrailAnimSpeed), 3, float4(0, 0, 0, _TrailOffset), 1, 1, 2, 2.2);
				float trails = PerlinRidged(uv, 2, float4(0, 0, 0, _TrailOffset), -17 *_TrailSize + 20.0, 1.2, _TrailComplexity, _TrailVisibility, 0.7);
				trails = pow(trails, 13.2);
				color += trails * trailNoise * _TrailColor;
#endif

#ifdef AGS_USE_STARS_ON
				float starsSize = 5 + 10 * (1 - _StarsSize);
				float starsNoise = PerlinNormal(float4(uv.xyz, _Time.x * _StarsBlinkingSpeed), 4, float4(0, 0, 0, 0), 3.2, 2.5, 2, 1);
				float stars = Stars3D(uv * 100, _StarsAmount, _StarsLightRandomness, starsSize + (0.2 * starsNoise * starsSize));
				float4 starsColor = lerp(_StarsColorOne, _StarsColorTwo, starsNoise *_StarsColorVariation);
				color += stars * starsColor;
#endif

				return color;
			}
			ENDCG
		}
	}

	CustomEditor "AGSSkyboxMaterialEditor"
}

