// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33117,y:32670,varname:node_4795,prsc:2|emission-7286-OUT,alpha-4827-OUT;n:type:ShaderForge.SFN_Panner,id:2203,x:31648,y:32765,varname:node_2203,prsc:2,spu:0,spv:-0.2|UVIN-5099-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5099,x:31447,y:32803,varname:node_5099,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3837,x:31840,y:32807,ptovrint:False,ptlb:node_3837,ptin:_node_3837,varname:node_3837,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:11dff3ab4a1cc7945aa0d07b416f7daa,ntxv:0,isnm:False|UVIN-2203-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:2239,x:32051,y:32584,varname:node_2239,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7112,x:32491,y:32625,varname:node_7112,prsc:2|A-2239-RGB,B-1638-OUT;n:type:ShaderForge.SFN_Tex2d,id:2647,x:31844,y:33094,ptovrint:False,ptlb:node_2647,ptin:_node_2647,varname:node_2647,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:11dff3ab4a1cc7945aa0d07b416f7daa,ntxv:0,isnm:False|UVIN-9655-UVOUT;n:type:ShaderForge.SFN_Panner,id:9655,x:31634,y:33084,varname:node_9655,prsc:2,spu:0.2,spv:-0.2|UVIN-1925-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1925,x:31415,y:33105,varname:node_1925,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:1827,x:32083,y:33215,ptovrint:False,ptlb:node_1827,ptin:_node_1827,varname:node_1827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:11dff3ab4a1cc7945aa0d07b416f7daa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5819,x:32423,y:32985,varname:node_5819,prsc:2|A-1638-OUT,B-1827-A;n:type:ShaderForge.SFN_ComponentMask,id:4827,x:32608,y:32985,varname:node_4827,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5819-OUT;n:type:ShaderForge.SFN_Add,id:1638,x:32205,y:32841,varname:node_1638,prsc:2|A-7390-OUT,B-9909-OUT;n:type:ShaderForge.SFN_Multiply,id:7390,x:32040,y:32720,varname:node_7390,prsc:2|A-3837-RGB,B-1179-OUT;n:type:ShaderForge.SFN_Multiply,id:9909,x:32083,y:33044,varname:node_9909,prsc:2|A-37-OUT,B-2647-R;n:type:ShaderForge.SFN_ValueProperty,id:1179,x:31803,y:32699,ptovrint:False,ptlb:Intensity 1,ptin:_Intensity1,varname:node_1179,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:37,x:31844,y:33006,ptovrint:False,ptlb:Intensity 2,ptin:_Intensity2,varname:node_37,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7286,x:32825,y:32658,varname:node_7286,prsc:2|A-7112-OUT,B-2143-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2143,x:32579,y:32806,ptovrint:False,ptlb:Glow Intensity,ptin:_GlowIntensity,varname:node_2143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:3837-2647-1827-1179-37-2143;pass:END;sub:END;*/

Shader "Shader Forge/SH_GodRay" {
    Properties {
        _node_3837 ("node_3837", 2D) = "white" {}
        _node_2647 ("node_2647", 2D) = "white" {}
        _node_1827 ("node_1827", 2D) = "white" {}
        _Intensity1 ("Intensity 1", Float ) = 0.5
        _Intensity2 ("Intensity 2", Float ) = 0.5
        _GlowIntensity ("Glow Intensity", Float ) = 1
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
            Blend SrcAlpha One
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
            uniform sampler2D _node_3837; uniform float4 _node_3837_ST;
            uniform sampler2D _node_2647; uniform float4 _node_2647_ST;
            uniform sampler2D _node_1827; uniform float4 _node_1827_ST;
            uniform float _Intensity1;
            uniform float _Intensity2;
            uniform float _GlowIntensity;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7018 = _Time;
                float2 node_2203 = (i.uv0+node_7018.g*float2(0,-0.2));
                float4 _node_3837_var = tex2D(_node_3837,TRANSFORM_TEX(node_2203, _node_3837));
                float2 node_9655 = (i.uv0+node_7018.g*float2(0.2,-0.2));
                float4 _node_2647_var = tex2D(_node_2647,TRANSFORM_TEX(node_9655, _node_2647));
                float3 node_1638 = ((_node_3837_var.rgb*_Intensity1)+(_Intensity2*_node_2647_var.r));
                float3 emissive = ((i.vertexColor.rgb*node_1638)*_GlowIntensity);
                float3 finalColor = emissive;
                float4 _node_1827_var = tex2D(_node_1827,TRANSFORM_TEX(i.uv0, _node_1827));
                fixed4 finalRGBA = fixed4(finalColor,(node_1638*_node_1827_var.a).r);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
