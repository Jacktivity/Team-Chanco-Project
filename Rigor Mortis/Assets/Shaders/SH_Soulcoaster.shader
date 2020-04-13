// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:3,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34083,y:32692,varname:node_4795,prsc:2|emission-6098-OUT;n:type:ShaderForge.SFN_TexCoord,id:4369,x:31735,y:32646,varname:node_4369,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:5310,x:31961,y:32646,varname:node_5310,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4369-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:3392,x:31961,y:32793,varname:node_3392,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-4369-UVOUT;n:type:ShaderForge.SFN_Add,id:2075,x:32160,y:32793,varname:node_2075,prsc:2|A-3392-OUT,B-990-OUT;n:type:ShaderForge.SFN_Vector1,id:990,x:31961,y:32940,varname:node_990,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:6133,x:32342,y:32793,varname:node_6133,prsc:2|A-2075-OUT,B-895-Z;n:type:ShaderForge.SFN_TexCoord,id:895,x:32160,y:32940,varname:node_895,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Append,id:9899,x:32504,y:32646,varname:node_9899,prsc:2|A-5310-OUT,B-6133-OUT;n:type:ShaderForge.SFN_Tex2d,id:8675,x:32708,y:32646,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_8675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9b2ecb2d5b1630f4eb7a10596ddd0841,ntxv:0,isnm:False|UVIN-9899-OUT;n:type:ShaderForge.SFN_Subtract,id:6868,x:32896,y:32779,varname:node_6868,prsc:2|A-8675-R,B-895-W;n:type:ShaderForge.SFN_Multiply,id:6894,x:33152,y:32837,varname:node_6894,prsc:2|A-6868-OUT,B-6218-RGB;n:type:ShaderForge.SFN_VertexColor,id:6218,x:32896,y:32921,varname:node_6218,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7964,x:33435,y:32768,varname:node_7964,prsc:2|A-664-OUT,B-6894-OUT;n:type:ShaderForge.SFN_ValueProperty,id:664,x:33152,y:32768,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_664,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7597,x:33435,y:32924,ptovrint:False,ptlb:Base Alpha,ptin:_BaseAlpha,varname:node_7597,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac6b22b2f2524fb449b363aefec149a0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5967,x:33685,y:32768,varname:node_5967,prsc:2|A-7964-OUT,B-7597-RGB;n:type:ShaderForge.SFN_Tex2d,id:8800,x:33685,y:32935,ptovrint:False,ptlb:Texture 2,ptin:_Texture2,varname:node_8800,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9b2ecb2d5b1630f4eb7a10596ddd0841,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6098,x:33855,y:32836,varname:node_6098,prsc:2|A-5967-OUT,B-8800-RGB;proporder:8675-664-7597-8800;pass:END;sub:END;*/

Shader "Shader Forge/SH_Soulcoaster" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _Glow ("Glow", Float ) = 1
        _BaseAlpha ("Base Alpha", 2D) = "white" {}
        _Texture2 ("Texture 2", 2D) = "white" {}
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
            Blend One SrcAlpha
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
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Glow;
            uniform sampler2D _BaseAlpha; uniform float4 _BaseAlpha_ST;
            uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_9899 = float2(i.uv0.r,((i.uv0.g+1.0)+i.uv0.b));
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(node_9899, _MainTexture));
                float4 _BaseAlpha_var = tex2D(_BaseAlpha,TRANSFORM_TEX(i.uv0, _BaseAlpha));
                float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(i.uv0, _Texture2));
                float3 emissive = (((_Glow*((_MainTexture_var.r-i.uv0.a)*i.vertexColor.rgb))*_BaseAlpha_var.rgb)*_Texture2_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
