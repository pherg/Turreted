//Clamps lighting values into "cartoony" lighting
//and renders a black line around the object
Shader "Custom/Cel Shader" 
{
	Properties 
	{
		_Color ("Diffuse Color", Color) = (1,1,1,1)
		_UnlitColor ("Unlit Diffuse Color", Color) = (0.5,0.5,0.5,1)
		_DiffuseThreshold ("Threshold for Diffuse Colors", Range(0,1)) = 0.1
		
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_LitOutlineThickness ("Lit Outline Thickness", Range(0,1)) = 0.1
		_UnlitOutlineThickness("Unlit Outline Thickness", Range(0,1)) = 0.4
		
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Float) = 10
	}
	SubShader 
	{
		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			
			GLSLPROGRAM
			
			// User-specified properties
			uniform vec4 _Color;
			uniform vec4 _UnlitColor;
			uniform float _DiffuseThreshold;
			uniform vec4 _OutlineColor;
			uniform float _LitOutlineThickness;
			uniform float _UnlitOutlineThickness;
			uniform vec4 _SpecColor;
			uniform float _Shininess;
			
			// Built in uniforms (except _LightColor0)
			// Found in #include "UnityCG.glslinc"
			
			// Camera in world space
			uniform vec3 _WorldSpaceCameraPos;
			
			// Model matrix
			uniform mat4 _Object2World;
			// Inverse model matrix
			uniform mat4 _World2Object;
			
			// Direction to or position of light source
			uniform vec4 _WorldSpaceLightPos0;
			
			// Color of the light source (from "Lighting.cginc)
			uniform vec4 _LightColor0;
			
			// position of the vertex (and fragment) in world space
			varying vec4 position;
			
			// surface normal vector in world space
			varying vec3 varyingNormalDirection;
			
			#ifdef VERTEX
			
			// Vertex main
			void main()
			{
				mat4 modelMatrix = _Object2World;
				mat4 modelMatrixInverse = _World2Object;
				
				position = modelMatrix * gl_Vertex;
				varyingNormalDirection = normalize(vec3(vec4(gl_Normal, 0.0) * modelMatrixInverse));
				
				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			}
			
			#endif
			
			#ifdef FRAGMENT
			
			// Fragment main
			void main()
			{
				vec3 normalDirection = normalize(varyingNormalDirection);
				vec3 viewDirection = normalize(_WorldSpaceCameraPos - vec3(position));
				vec3 lightDirection;
				float attenuation;
				
				// Directional Light
				if ( _WorldSpaceLightPos0.w == 0 )
				{
					attenuation = 1.0;
					lightDirection = normalize(vec3(_WorldSpaceLightPos0));
				}
				// Point or Spot Light
				else
				{
					vec3 vertexToLightSource = vec3(_WorldSpaceLightPos0 - position);
					
					float distance = length(vertexToLightSource);
					
					// Lineaer attenuation
					attenuation = 1.0 / distance;
					lightDirection = normalize(vertexToLightSource);
				}
				
				// default: unlit
				vec3 fragmentColor = vec3(_UnlitColor);
				
				// Low priority : diffuse illumination
				if ( attenuation * max(0.0, dot(normalDirection, lightDirection)) >= _DiffuseThreshold )
				{
					fragmentColor = vec3(_LightColor0) * vec3(_Color);
				}
				
				// Higher priority : Outline
				if ( dot(viewDirection, normalDirection) < 
					 mix(_UnlitOutlineThickness, _LitOutlineThickness, max( 0.0, dot(normalDirection, lightDirection))))
				{
					fragmentColor = vec3(_LightColor0) * vec3(_OutlineColor);
				}
				
				// Highest priority : Highlights
				if ( dot(normalDirection, lightDirection) > 0.0 &&
				attenuation * 
				pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess) > 0.5 )
				{
					fragmentColor = _SpecColor.a * vec3(_LightColor0) * vec3(_SpecColor)
									+ (1.0 - _SpecColor.a) * fragmentColor;
				}
				
				gl_FragColor = vec4(fragmentColor, 1.0);
			}
			
			#endif
			
			ENDGLSL
		}
		
		Pass
		{
			// Pass for additional light sources
			Tags { "LightMode" = "ForwardAdd" }
			
			// Blend Specular highlights over framebuffer
			Blend SrcAlpha OneMinusSrcAlpha
			
			GLSLPROGRAM
			
			// User-specified properties
			uniform vec4 _Color;
			uniform vec4 _UnlitColor;
			uniform float _DiffuseThreshold;
			uniform vec4 _OutlineColor;
			uniform float _LitOutlineThickness;
			uniform float _UnlitOutlineThickness;
			uniform vec4 _SpecColor;
			uniform float _Shininess;
			
			// Built in uniforms (except _LightColor0)
			// Found in #include "UnityCG.glslinc"
			
			// Camera in world space
			uniform vec3 _WorldSpaceCameraPos;
			
			// Model matrix
			uniform mat4 _Object2World;
			// Inverse model matrix
			uniform mat4 _World2Object;
			
			// Direction to or position of light source
			uniform vec4 _WorldSpaceLightPos0;
			
			// Color of the light source (from "Lighting.cginc)
			uniform vec4 _LightColor0;
			
			// position of the vertex (and fragment) in world space
			varying vec4 position;
			
			// surface normal vector in world space
			varying vec3 varyingNormalDirection;
			
			#ifdef VERTEX
			
			void main()
			{
				mat4 modelMatrix = _Object2World;
				mat4 modelMatrixInverse = _World2Object;
				
				position = modelMatrix * gl_Vertex;
				varyingNormalDirection = normalize(vec3(vec4(gl_Normal, 0.0) * modelMatrixInverse));
				
				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			}
			
			#endif
			
			#ifdef FRAGMENT
			
			void main()
			{
				vec3 normalDirection = normalize(varyingNormalDirection);
				vec3 viewDirection = normalize(_WorldSpaceCameraPos - vec3(position));
				vec3 lightDirection;
				float attenuation;
				
				// Directional Light
				if ( _WorldSpaceLightPos0.w == 0 )
				{
					attenuation = 1.0;
					lightDirection = normalize(vec3(_WorldSpaceLightPos0));
				}
				// Point or Spot Light
				else
				{
					vec3 vertexToLightSource = vec3(_WorldSpaceLightPos0 - position);
					float distance = length(vertexToLightSource);
					
					attenuation = 1.0 / distance;
					lightDirection = normalize(vertexToLightSource);
				}
				
				vec4 fragmentColor = vec4(0.0,0.0,0.0,0.0);
				
				if ( dot(normalDirection, lightDirection) > 0.0 &&
					attenuation *
					pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess) > 0.5 )
				{
					fragmentColor = vec4(_LightColor0.rgb, 1.0) * _SpecColor;
				}
				
				gl_FragColor = fragmentColor;
			}
			
			#endif
			
			ENDGLSL
			
		}
	} 
	//Do not use Fallback shaders for development
	//FallBack "Specular"
}
