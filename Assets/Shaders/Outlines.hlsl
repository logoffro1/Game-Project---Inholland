#ifndef OUTLINES_INCLUDED
#define OUTLINES_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes {
	float4 positionOS    : POSITION;
	float3 normalOS      : NORMAL;


	};

	struct VertexOutput {
		float4 positionCS    : SV_POSITION;
		};



		float _Thickness;
		float4 _Color;
		float _DepthOffset;

		VertexOutput Vertex(Attributes input){
			VertexOutput output = (VertexOutput)0;

			float3 normalOS = input.normalOS;


			float3 posOS = input.positionOS.xyz + normalOS * _Thickness;

			output.positionCS = GetVertexPositionInputs(posOS).positionCS;

			float depthOffset = _DepthOffset;

			#ifdef UNITY_REVERSED_Z
			depthOffset = -depthOffset;
			#endif
			output.positionCS.z += depthOffset;


			return output;
			}

			float4 Fragment(VertexOutput input) : SV_TARGET {
				return _Color;
				}

#endif