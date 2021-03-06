// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32973,y:32729,varname:node_4795,prsc:2|emission-1676-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:6010,x:31994,y:32771,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:_MainTexture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:55f330f5e6e638b4a8d35e8db535b1d5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4164,x:32174,y:32788,varname:node_4164,prsc:2,tex:55f330f5e6e638b4a8d35e8db535b1d5,ntxv:0,isnm:False|TEX-6010-TEX;n:type:ShaderForge.SFN_VertexColor,id:2238,x:31994,y:32965,varname:node_2238,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1676,x:32516,y:32817,varname:node_1676,prsc:2|A-4164-RGB,B-2238-RGB,C-2238-A,D-6689-OUT;n:type:ShaderForge.SFN_TexCoord,id:8599,x:32068,y:33116,varname:node_8599,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_ValueProperty,id:6689,x:32255,y:33073,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_6689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:6010-6689;pass:END;sub:END;*/

Shader "Shader Forge/Additive_Base" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Glow ("Glow", Float ) = 1
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
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
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
                float4 node_4164 = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 emissive = (node_4164.rgb*i.vertexColor.rgb*i.vertexColor.a*_Glow);
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
