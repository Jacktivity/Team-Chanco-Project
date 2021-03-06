// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32883,y:32771,varname:node_4795,prsc:2|emission-9039-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9427,x:32045,y:32815,ptovrint:False,ptlb:MainTexture_copy,ptin:_MainTexture_copy,varname:_MainTexture_copy,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:14cbf817095c90040b10949946b5f35e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4292,x:32355,y:32797,varname:node_4164,prsc:2,tex:14cbf817095c90040b10949946b5f35e,ntxv:0,isnm:False|UVIN-9270-OUT,TEX-9427-TEX;n:type:ShaderForge.SFN_VertexColor,id:2064,x:32186,y:33157,varname:node_2064,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9039,x:32632,y:32800,varname:node_9039,prsc:2|A-4292-RGB,B-2064-RGB,C-2064-A,D-1055-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1055,x:32417,y:33281,ptovrint:False,ptlb:Glow Intensity_copy,ptin:_GlowIntensity_copy,varname:_GlowIntensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:5967,x:31442,y:32920,varname:node_5967,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:9594,x:31648,y:32484,varname:node_9594,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7654,x:31617,y:32784,varname:node_7654,prsc:2|A-7599-OUT,B-5967-T;n:type:ShaderForge.SFN_ValueProperty,id:3010,x:31196,y:32676,ptovrint:False,ptlb:U,ptin:_U,varname:node_3010,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2540,x:31196,y:32775,ptovrint:False,ptlb:V,ptin:_V,varname:node_2540,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:7599,x:31452,y:32627,varname:node_7599,prsc:2|A-3010-OUT,B-2540-OUT;n:type:ShaderForge.SFN_Add,id:9270,x:31917,y:32567,varname:node_9270,prsc:2|A-9594-UVOUT,B-7654-OUT;proporder:9427-1055-3010-2540;pass:END;sub:END;*/

Shader "Shader Forge/SH_Additive_Panner" {
    Properties {
        _MainTexture_copy ("MainTexture_copy", 2D) = "white" {}
        _GlowIntensity_copy ("Glow Intensity_copy", Float ) = 1
        _U ("U", Float ) = 0
        _V ("V", Float ) = 1
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
            uniform sampler2D _MainTexture_copy; uniform float4 _MainTexture_copy_ST;
            uniform float _GlowIntensity_copy;
            uniform float _U;
            uniform float _V;
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
                float4 node_5967 = _Time;
                float2 node_9270 = (i.uv0+(float2(_U,_V)*node_5967.g));
                float4 node_4164 = tex2D(_MainTexture_copy,TRANSFORM_TEX(node_9270, _MainTexture_copy));
                float3 emissive = (node_4164.rgb*i.vertexColor.rgb*i.vertexColor.a*_GlowIntensity_copy);
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
