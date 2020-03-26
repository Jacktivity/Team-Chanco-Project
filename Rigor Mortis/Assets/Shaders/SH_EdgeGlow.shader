// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33145,y:32657,varname:node_4795,prsc:2|emission-9048-OUT,alpha-3931-OUT;n:type:ShaderForge.SFN_Color,id:9533,x:32558,y:32381,ptovrint:False,ptlb:Colour,ptin:_Colour,varname:node_9533,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.7960785,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:4280,x:32772,y:32560,varname:node_4280,prsc:2|A-9533-RGB,B-6216-OUT;n:type:ShaderForge.SFN_Tex2d,id:446,x:32199,y:32570,ptovrint:False,ptlb:Texture 1,ptin:_Texture1,varname:node_446,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a8e366d6d4c00f149bbff2e08889ce00,ntxv:0,isnm:False|UVIN-5331-UVOUT;n:type:ShaderForge.SFN_Panner,id:5331,x:32032,y:32570,varname:node_5331,prsc:2,spu:-0.05,spv:0.4|UVIN-7806-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7806,x:31858,y:32570,varname:node_7806,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:1143,x:31653,y:32796,varname:node_1143,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:8794,x:31858,y:32796,varname:node_8794,prsc:2|A-1143-UVOUT,B-3029-OUT;n:type:ShaderForge.SFN_Vector1,id:3029,x:31653,y:32952,varname:node_3029,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Panner,id:8934,x:32032,y:32796,varname:node_8934,prsc:2,spu:1,spv:1|UVIN-8794-OUT;n:type:ShaderForge.SFN_Tex2d,id:4339,x:32199,y:32796,ptovrint:False,ptlb:Texture 2,ptin:_Texture2,varname:node_4339,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a8e366d6d4c00f149bbff2e08889ce00,ntxv:0,isnm:False|UVIN-8934-UVOUT;n:type:ShaderForge.SFN_Add,id:6216,x:32428,y:32587,varname:node_6216,prsc:2|A-446-RGB,B-4339-RGB;n:type:ShaderForge.SFN_Multiply,id:9048,x:32966,y:32708,varname:node_9048,prsc:2|A-4280-OUT,B-3931-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3931,x:32771,y:32914,varname:node_3931,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8084-OUT;n:type:ShaderForge.SFN_Smoothstep,id:8084,x:32664,y:33181,varname:node_8084,prsc:2|A-4707-OUT,B-7062-OUT,V-8416-RGB;n:type:ShaderForge.SFN_Slider,id:4707,x:32045,y:33166,ptovrint:False,ptlb:Curve,ptin:_Curve,varname:node_4707,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2097189,max:1;n:type:ShaderForge.SFN_Slider,id:9412,x:32045,y:33253,ptovrint:False,ptlb:Soft,ptin:_Soft,varname:node_9412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6316116,max:1;n:type:ShaderForge.SFN_Add,id:7062,x:32467,y:33208,varname:node_7062,prsc:2|A-4707-OUT,B-9412-OUT;n:type:ShaderForge.SFN_Tex2d,id:8416,x:32080,y:32964,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_8416,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6465040c4b9fa1468f87722c2235599,ntxv:0,isnm:False|UVIN-9452-UVOUT;n:type:ShaderForge.SFN_Panner,id:9452,x:31884,y:32964,varname:node_9452,prsc:2,spu:0.1,spv:0|UVIN-6559-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6559,x:31673,y:33071,varname:node_6559,prsc:2,uv:0,uaff:False;proporder:9533-446-4339-4707-9412-8416;pass:END;sub:END;*/

Shader "Shader Forge/SH_EdgeGlow" {
    Properties {
        _Colour ("Colour", Color) = (1,0.7960785,0,1)
        _Texture1 ("Texture 1", 2D) = "white" {}
        _Texture2 ("Texture 2", 2D) = "white" {}
        _Curve ("Curve", Range(0, 1)) = 0.2097189
        _Soft ("Soft", Range(0, 1)) = 0.6316116
        _Alpha ("Alpha", 2D) = "white" {}
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
            Blend One One
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
            uniform float4 _Colour;
            uniform sampler2D _Texture1; uniform float4 _Texture1_ST;
            uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
            uniform float _Curve;
            uniform float _Soft;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7486 = _Time;
                float2 node_5331 = (i.uv0+node_7486.g*float2(-0.05,0.4));
                float4 _Texture1_var = tex2D(_Texture1,TRANSFORM_TEX(node_5331, _Texture1));
                float2 node_8934 = ((i.uv0*0.5)+node_7486.g*float2(1,1));
                float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(node_8934, _Texture2));
                float node_7062 = (_Curve+_Soft);
                float2 node_9452 = (i.uv0+node_7486.g*float2(0.1,0));
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(node_9452, _Alpha));
                float node_3931 = smoothstep( float3(_Curve,_Curve,_Curve), float3(node_7062,node_7062,node_7062), _Alpha_var.rgb ).r;
                float3 emissive = ((_Colour.rgb*(_Texture1_var.rgb+_Texture2_var.rgb))*node_3931);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_3931);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
