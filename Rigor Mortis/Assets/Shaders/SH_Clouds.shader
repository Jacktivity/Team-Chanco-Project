// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5849056,fgcg:0.5849056,fgcb:0.5849056,fgca:1,fgde:1,fgrn:33.31,fgrf:2191.3,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32988,y:32610,varname:node_4795,prsc:2|diff-2212-OUT,emission-3192-OUT,alpha-8217-OUT;n:type:ShaderForge.SFN_VertexColor,id:8617,x:31755,y:32429,varname:node_8617,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2212,x:32487,y:32352,varname:node_2212,prsc:2|A-8617-RGB,B-287-RGB;n:type:ShaderForge.SFN_Tex2d,id:6066,x:31755,y:32606,ptovrint:False,ptlb:Clouds,ptin:_Clouds,varname:node_6066,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ad75867e602899e4c9fda5a8a9c5d2fe,ntxv:0,isnm:False|UVIN-122-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:287,x:31755,y:32816,ptovrint:False,ptlb:Clouds2,ptin:_Clouds2,varname:node_287,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ad75867e602899e4c9fda5a8a9c5d2fe,ntxv:0,isnm:False|UVIN-2531-UVOUT;n:type:ShaderForge.SFN_Power,id:5793,x:32214,y:32716,varname:node_5793,prsc:2|VAL-6066-RGB,EXP-3012-OUT;n:type:ShaderForge.SFN_Vector1,id:3012,x:31993,y:32880,varname:node_3012,prsc:2,v1:0.75;n:type:ShaderForge.SFN_Panner,id:122,x:31569,y:32606,varname:node_122,prsc:2,spu:0.5,spv:-0.25|UVIN-8651-UVOUT;n:type:ShaderForge.SFN_Panner,id:2531,x:31569,y:32816,varname:node_2531,prsc:2,spu:0.5,spv:0|UVIN-8651-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8651,x:31355,y:32606,varname:node_8651,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3192,x:32486,y:32638,varname:node_3192,prsc:2|A-8617-RGB,B-5793-OUT;n:type:ShaderForge.SFN_Tex2d,id:2597,x:31766,y:33133,ptovrint:False,ptlb:CloudsAlpha,ptin:_CloudsAlpha,varname:node_2597,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:285722766d98dab45a8c70dd696adf10,ntxv:0,isnm:False|UVIN-2531-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:1647,x:32165,y:33233,varname:node_1647,prsc:2|IN-2597-R;n:type:ShaderForge.SFN_Add,id:2628,x:31993,y:32961,varname:node_2628,prsc:2|A-287-RGB,B-7048-OUT;n:type:ShaderForge.SFN_Vector1,id:7048,x:31803,y:33017,varname:node_7048,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1996,x:32353,y:33017,varname:node_1996,prsc:2|A-2628-OUT,B-1647-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2548,x:32612,y:33118,varname:node_2548,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1996-OUT;n:type:ShaderForge.SFN_Multiply,id:8217,x:32784,y:32871,varname:node_8217,prsc:2|A-8617-A,B-2548-OUT;proporder:287-6066-2597;pass:END;sub:END;*/

Shader "Shader Forge/SH_Clouds" {
    Properties {
        _Clouds2 ("Clouds2", 2D) = "white" {}
        _Clouds ("Clouds", 2D) = "white" {}
        _CloudsAlpha ("CloudsAlpha", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform sampler2D _CloudsAlpha; uniform float4 _CloudsAlpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_8632 = _Time;
                float2 node_2531 = (i.uv0+node_8632.g*float2(0.5,0));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_2531, _Clouds2));
                float3 node_2212 = (i.vertexColor.rgb*_Clouds2_var.rgb);
                float3 diffuseColor = node_2212;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_122 = (i.uv0+node_8632.g*float2(0.5,-0.25));
                float4 _Clouds_var = tex2D(_Clouds,TRANSFORM_TEX(node_122, _Clouds));
                float3 node_3192 = (i.vertexColor.rgb*pow(_Clouds_var.rgb,0.75));
                float3 emissive = node_3192;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _CloudsAlpha_var = tex2D(_CloudsAlpha,TRANSFORM_TEX(node_2531, _CloudsAlpha));
                fixed4 finalRGBA = fixed4(finalColor,(i.vertexColor.a*((_Clouds2_var.rgb+0.5)*(1.0 - _CloudsAlpha_var.r)).r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform sampler2D _CloudsAlpha; uniform float4 _CloudsAlpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_743 = _Time;
                float2 node_2531 = (i.uv0+node_743.g*float2(0.5,0));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_2531, _Clouds2));
                float3 node_2212 = (i.vertexColor.rgb*_Clouds2_var.rgb);
                float3 diffuseColor = node_2212;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _CloudsAlpha_var = tex2D(_CloudsAlpha,TRANSFORM_TEX(node_2531, _CloudsAlpha));
                fixed4 finalRGBA = fixed4(finalColor * (i.vertexColor.a*((_Clouds2_var.rgb+0.5)*(1.0 - _CloudsAlpha_var.r)).r),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
