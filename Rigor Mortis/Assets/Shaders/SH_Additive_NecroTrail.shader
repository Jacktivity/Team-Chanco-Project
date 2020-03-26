// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5849056,fgcg:0.5849056,fgcb:0.5849056,fgca:1,fgde:1,fgrn:33.31,fgrf:2191.3,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32986,y:31723,varname:node_4795,prsc:2|diff-3237-RGB,emission-2883-OUT,alpha-1760-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31605,y:32496,ptovrint:False,ptlb:Blood Tex,ptin:_BloodTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3234d4418e1343e4a90e58f815236622,ntxv:0,isnm:False|UVIN-6991-OUT;n:type:ShaderForge.SFN_TexCoord,id:6169,x:30126,y:32780,varname:node_6169,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:8670,x:30366,y:32754,varname:node_8670,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6169-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:2551,x:30366,y:32890,varname:node_2551,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-6169-UVOUT;n:type:ShaderForge.SFN_Add,id:9451,x:30936,y:32754,varname:node_9451,prsc:2|A-7880-OUT,B-885-W;n:type:ShaderForge.SFN_Vector1,id:7595,x:30510,y:33013,varname:node_7595,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:6991,x:31142,y:32884,varname:node_6991,prsc:2|A-8670-OUT,B-9451-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1381,x:30420,y:32426,ptovrint:False,ptlb:Gew Speed,ptin:_GewSpeed,varname:node_1381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:1481,x:30606,y:32572,varname:node_1481,prsc:2;n:type:ShaderForge.SFN_Vector1,id:4505,x:30420,y:32491,varname:node_4505,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:2460,x:30606,y:32426,varname:node_2460,prsc:2|A-1381-OUT,B-4505-OUT;n:type:ShaderForge.SFN_Multiply,id:6809,x:30835,y:32426,varname:node_6809,prsc:2|A-2460-OUT,B-1481-T;n:type:ShaderForge.SFN_Add,id:823,x:31320,y:32374,varname:node_823,prsc:2|A-6809-OUT,B-6991-OUT;n:type:ShaderForge.SFN_Multiply,id:6782,x:31948,y:32151,varname:node_6782,prsc:2|A-7436-RGB,B-6074-RGB;n:type:ShaderForge.SFN_TexCoord,id:885,x:30710,y:32993,varname:node_885,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Tex2d,id:7661,x:31903,y:31768,ptovrint:False,ptlb:Cloud Tex,ptin:_CloudTex,varname:node_7661,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e5150d3dbfd764f49881f1cd58346dfb,ntxv:0,isnm:False|UVIN-2563-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6269,x:31262,y:31708,ptovrint:False,ptlb:Cloud Speed,ptin:_CloudSpeed,varname:node_6269,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_Append,id:3767,x:31480,y:31690,varname:node_3767,prsc:2|A-6269-OUT,B-6184-OUT;n:type:ShaderForge.SFN_Vector1,id:6184,x:31208,y:31978,varname:node_6184,prsc:2,v1:0;n:type:ShaderForge.SFN_Time,id:9108,x:31467,y:31851,varname:node_9108,prsc:2;n:type:ShaderForge.SFN_Color,id:3237,x:31681,y:31348,ptovrint:False,ptlb:Cloud Texture,ptin:_CloudTexture,varname:node_3237,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8078432,c2:0.4117647,c3:0.4117647,c4:1;n:type:ShaderForge.SFN_Add,id:8039,x:32223,y:32336,varname:node_8039,prsc:2|A-7661-RGB,B-6074-RGB;n:type:ShaderForge.SFN_Tex2d,id:3283,x:32237,y:32519,ptovrint:False,ptlb:node_3283,ptin:_node_3283,varname:node_3283,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:83a665b4548b91a4c974cf09b2367ce0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8537,x:32450,y:32365,varname:node_8537,prsc:2|A-8039-OUT,B-3283-RGB;n:type:ShaderForge.SFN_TexCoord,id:2103,x:31346,y:31482,varname:node_2103,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:847,x:31678,y:31719,varname:node_847,prsc:2|A-3767-OUT,B-9108-T;n:type:ShaderForge.SFN_Add,id:2563,x:31727,y:31574,varname:node_2563,prsc:2|A-2103-UVOUT,B-847-OUT;n:type:ShaderForge.SFN_TexCoord,id:6983,x:32204,y:31724,varname:node_6983,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_VertexColor,id:7436,x:31730,y:32014,varname:node_7436,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2883,x:32531,y:31889,varname:node_2883,prsc:2|A-6782-OUT,B-6983-Z;n:type:ShaderForge.SFN_Multiply,id:8018,x:32617,y:32278,varname:node_8018,prsc:2|A-5053-U,B-8537-OUT;n:type:ShaderForge.SFN_TexCoord,id:5053,x:32385,y:32124,varname:node_5053,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_ComponentMask,id:1760,x:32791,y:32142,varname:node_1760,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8018-OUT;n:type:ShaderForge.SFN_Subtract,id:7880,x:30721,y:32861,varname:node_7880,prsc:2|A-7134-OUT,B-7595-OUT;n:type:ShaderForge.SFN_OneMinus,id:7134,x:30546,y:32861,varname:node_7134,prsc:2|IN-2551-OUT;proporder:6074-1381-7661-6269-3237-3283;pass:END;sub:END;*/

Shader "Shader Forge/SH_Additive_NecroTrial" {
    Properties {
        _BloodTex ("Blood Tex", 2D) = "white" {}
        _GewSpeed ("Gew Speed", Float ) = 0
        _CloudTex ("Cloud Tex", 2D) = "white" {}
        _CloudSpeed ("Cloud Speed", Float ) = 2.5
        _CloudTexture ("Cloud Texture", Color) = (0.8078432,0.4117647,0.4117647,1)
        _node_3283 ("node_3283", 2D) = "white" {}
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
            uniform sampler2D _BloodTex; uniform float4 _BloodTex_ST;
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _CloudSpeed;
            uniform float4 _CloudTexture;
            uniform sampler2D _node_3283; uniform float4 _node_3283_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
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
                float3 diffuseColor = _CloudTexture.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
                float2 node_6991 = float2(i.uv0.r,(((1.0 - i.uv0.g)-1.0)+i.uv0.a));
                float4 _BloodTex_var = tex2D(_BloodTex,TRANSFORM_TEX(node_6991, _BloodTex));
                float3 emissive = ((i.vertexColor.rgb*_BloodTex_var.rgb)*i.uv0.b);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 node_9108 = _Time;
                float2 node_2563 = (i.uv0+(float2(_CloudSpeed,0.0)*node_9108.g));
                float4 _CloudTex_var = tex2D(_CloudTex,TRANSFORM_TEX(node_2563, _CloudTex));
                float4 _node_3283_var = tex2D(_node_3283,TRANSFORM_TEX(i.uv0, _node_3283));
                fixed4 finalRGBA = fixed4(finalColor,(i.uv1.r*((_CloudTex_var.rgb+_BloodTex_var.rgb)*_node_3283_var.rgb)).r);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5849056,0.5849056,0.5849056,1));
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
            uniform sampler2D _BloodTex; uniform float4 _BloodTex_ST;
            uniform sampler2D _CloudTex; uniform float4 _CloudTex_ST;
            uniform float _CloudSpeed;
            uniform float4 _CloudTexture;
            uniform sampler2D _node_3283; uniform float4 _node_3283_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
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
                float3 diffuseColor = _CloudTexture.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 node_9108 = _Time;
                float2 node_2563 = (i.uv0+(float2(_CloudSpeed,0.0)*node_9108.g));
                float4 _CloudTex_var = tex2D(_CloudTex,TRANSFORM_TEX(node_2563, _CloudTex));
                float2 node_6991 = float2(i.uv0.r,(((1.0 - i.uv0.g)-1.0)+i.uv0.a));
                float4 _BloodTex_var = tex2D(_BloodTex,TRANSFORM_TEX(node_6991, _BloodTex));
                float4 _node_3283_var = tex2D(_node_3283,TRANSFORM_TEX(i.uv0, _node_3283));
                fixed4 finalRGBA = fixed4(finalColor * (i.uv1.r*((_CloudTex_var.rgb+_BloodTex_var.rgb)*_node_3283_var.rgb)).r,0);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5849056,0.5849056,0.5849056,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
