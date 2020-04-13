// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32635,y:32644,varname:node_4795,prsc:2|emission-1409-OUT,alpha-9706-OUT;n:type:ShaderForge.SFN_Rotator,id:7876,x:31865,y:32858,varname:node_7876,prsc:2|UVIN-9121-UVOUT,SPD-9939-OUT;n:type:ShaderForge.SFN_TexCoord,id:9121,x:31667,y:32858,varname:node_9121,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:9939,x:31667,y:33089,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9939,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Tex2d,id:569,x:32050,y:32858,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2f4ac0a27d6b755499bdf82b47526d01,ntxv:0,isnm:False|UVIN-7876-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:9366,x:32067,y:32671,varname:node_9366,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8981,x:32293,y:32714,varname:node_8981,prsc:2|A-9366-RGB,B-569-RGB;n:type:ShaderForge.SFN_Multiply,id:9706,x:32406,y:32883,varname:node_9706,prsc:2|A-9366-A,B-569-R;n:type:ShaderForge.SFN_Multiply,id:1409,x:32468,y:32631,varname:node_1409,prsc:2|A-5732-OUT,B-8981-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5732,x:32230,y:32604,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_5732,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:9939-569-5732;pass:END;sub:END;*/

Shader "Shader Forge/SH_BasicRotator" {
    Properties {
        _Speed ("Speed", Float ) = 2
        _Texture ("Texture", 2D) = "white" {}
        _Glow ("Glow", Float ) = 1
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
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Glow;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
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
                float4 node_2062 = _Time;
                float node_7876_ang = node_2062.g;
                float node_7876_spd = _Speed;
                float node_7876_cos = cos(node_7876_spd*node_7876_ang);
                float node_7876_sin = sin(node_7876_spd*node_7876_ang);
                float2 node_7876_piv = float2(0.5,0.5);
                float2 node_7876 = (mul(i.uv0-node_7876_piv,float2x2( node_7876_cos, -node_7876_sin, node_7876_sin, node_7876_cos))+node_7876_piv);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_7876, _Texture));
                float3 emissive = (_Glow*(i.vertexColor.rgb*_Texture_var.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(i.vertexColor.a*_Texture_var.r));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
