// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33588,y:32594,varname:node_4795,prsc:2|emission-1742-OUT,alpha-827-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32054,y:32438,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5a33cf346f67d7c4db56058014762581,ntxv:0,isnm:False|UVIN-420-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32389,y:32240,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Rotator,id:420,x:31904,y:32391,varname:node_420,prsc:2|UVIN-3089-UVOUT,SPD-1690-OUT;n:type:ShaderForge.SFN_TexCoord,id:3089,x:31598,y:32190,varname:node_3089,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:1690,x:31621,y:32469,varname:node_1690,prsc:2,v1:10;n:type:ShaderForge.SFN_Tex2d,id:3030,x:32129,y:32783,ptovrint:False,ptlb:SecondaryTex,ptin:_SecondaryTex,varname:node_3030,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5a33cf346f67d7c4db56058014762581,ntxv:0,isnm:False|UVIN-1061-OUT;n:type:ShaderForge.SFN_RemapRange,id:941,x:31313,y:32626,varname:node_941,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-9187-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:660,x:30881,y:32559,varname:node_660,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7329,x:31527,y:32780,varname:node_7329,prsc:2|A-941-OUT,B-7680-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7680,x:31250,y:32852,ptovrint:False,ptlb:node_7680,ptin:_node_7680,varname:node_7680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_Add,id:1061,x:31728,y:32889,varname:node_1061,prsc:2|A-7329-OUT,B-60-OUT;n:type:ShaderForge.SFN_Vector2,id:60,x:31510,y:32945,varname:node_60,prsc:2,v1:0.75,v2:0.75;n:type:ShaderForge.SFN_Multiply,id:8112,x:32391,y:32526,varname:node_8112,prsc:2|A-6074-RGB,B-3030-RGB;n:type:ShaderForge.SFN_Multiply,id:9151,x:32611,y:32310,varname:node_9151,prsc:2|A-2053-RGB,B-8112-OUT;n:type:ShaderForge.SFN_Tex2d,id:591,x:32526,y:32869,ptovrint:False,ptlb:node_591,ptin:_node_591,varname:node_591,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:58a84c4a63e0e194084fb03a20f33e6c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Rotator,id:9187,x:31137,y:32588,varname:node_9187,prsc:2|UVIN-660-UVOUT,SPD-1210-OUT;n:type:ShaderForge.SFN_Vector1,id:1210,x:30916,y:32791,varname:node_1210,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:5469,x:32924,y:32806,varname:node_5469,prsc:2|A-8112-OUT,B-591-RGB;n:type:ShaderForge.SFN_ComponentMask,id:827,x:33095,y:32837,varname:node_827,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5469-OUT;n:type:ShaderForge.SFN_Multiply,id:1742,x:33305,y:32630,varname:node_1742,prsc:2|A-9151-OUT,B-827-OUT,C-5034-Z;n:type:ShaderForge.SFN_TexCoord,id:5034,x:32924,y:32619,varname:node_5034,prsc:2,uv:0,uaff:True;proporder:6074-3030-7680-591;pass:END;sub:END;*/

Shader "Shader Forge/SH_Additive_Rotator" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _SecondaryTex ("SecondaryTex", 2D) = "white" {}
        _node_7680 ("node_7680", Float ) = 0.8
        _node_591 ("node_591", 2D) = "white" {}
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _SecondaryTex; uniform float4 _SecondaryTex_ST;
            uniform float _node_7680;
            uniform sampler2D _node_591; uniform float4 _node_591_ST;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_9532 = _Time;
                float node_420_ang = node_9532.g;
                float node_420_spd = 10.0;
                float node_420_cos = cos(node_420_spd*node_420_ang);
                float node_420_sin = sin(node_420_spd*node_420_ang);
                float2 node_420_piv = float2(0.5,0.5);
                float2 node_420 = (mul(i.uv0-node_420_piv,float2x2( node_420_cos, -node_420_sin, node_420_sin, node_420_cos))+node_420_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_420, _MainTex));
                float node_9187_ang = node_9532.g;
                float node_9187_spd = 10.0;
                float node_9187_cos = cos(node_9187_spd*node_9187_ang);
                float node_9187_sin = sin(node_9187_spd*node_9187_ang);
                float2 node_9187_piv = float2(0.5,0.5);
                float2 node_9187 = (mul(i.uv0-node_9187_piv,float2x2( node_9187_cos, -node_9187_sin, node_9187_sin, node_9187_cos))+node_9187_piv);
                float2 node_1061 = (((node_9187*1.0+-0.5)*_node_7680)+float2(0.75,0.75));
                float4 _SecondaryTex_var = tex2D(_SecondaryTex,TRANSFORM_TEX(node_1061, _SecondaryTex));
                float3 node_8112 = (_MainTex_var.rgb*_SecondaryTex_var.rgb);
                float4 _node_591_var = tex2D(_node_591,TRANSFORM_TEX(i.uv0, _node_591));
                float node_827 = (node_8112*_node_591_var.rgb).r;
                float3 emissive = ((i.vertexColor.rgb*node_8112)*node_827*i.uv0.b);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_827);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
