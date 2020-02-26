// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32409,y:32405,varname:node_4795,prsc:2|emission-7705-OUT,alpha-6074-A;n:type:ShaderForge.SFN_Tex2d,id:6074,x:30868,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:277fb3320492e1c4bbcd1306b196236a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31584,y:31964,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31472,y:32539,ptovrint:True,ptlb:OutlineColor,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8301887,c2:0.8301887,c3:0.8301887,c4:1;n:type:ShaderForge.SFN_TexCoord,id:1520,x:30397,y:32166,varname:node_1520,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:4449,x:30573,y:32166,varname:node_4449,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-1520-UVOUT;n:type:ShaderForge.SFN_Step,id:225,x:30750,y:32166,varname:node_225,prsc:2|A-4449-OUT,B-9802-Z;n:type:ShaderForge.SFN_OneMinus,id:2571,x:30911,y:31998,varname:node_2571,prsc:2|IN-225-OUT;n:type:ShaderForge.SFN_Multiply,id:2625,x:31377,y:32110,varname:node_2625,prsc:2|A-6280-OUT,B-6236-OUT;n:type:ShaderForge.SFN_Vector1,id:6236,x:30911,y:32129,varname:node_6236,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Add,id:4126,x:31615,y:32228,varname:node_4126,prsc:2|A-2625-OUT,B-9734-OUT;n:type:ShaderForge.SFN_Multiply,id:2,x:31232,y:32319,varname:node_2,prsc:2|A-225-OUT,B-6074-R;n:type:ShaderForge.SFN_Multiply,id:6280,x:31152,y:31998,varname:node_6280,prsc:2|A-2571-OUT,B-6074-R;n:type:ShaderForge.SFN_Multiply,id:3422,x:31810,y:32167,varname:node_3422,prsc:2|A-2053-RGB,B-4126-OUT;n:type:ShaderForge.SFN_Add,id:7705,x:32150,y:32556,varname:node_7705,prsc:2|A-3422-OUT,B-4878-OUT;n:type:ShaderForge.SFN_Multiply,id:4878,x:31677,y:32443,varname:node_4878,prsc:2|A-6074-B,B-797-RGB;n:type:ShaderForge.SFN_Multiply,id:9734,x:31416,y:32339,varname:node_9734,prsc:2|A-2-OUT,B-816-OUT;n:type:ShaderForge.SFN_ValueProperty,id:816,x:31177,y:32481,ptovrint:False,ptlb:Glow Intensity,ptin:_GlowIntensity,varname:node_816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_TexCoord,id:9802,x:30573,y:32324,varname:node_9802,prsc:2,uv:0,uaff:True;proporder:6074-797-816;pass:END;sub:END;*/

Shader "Shader Forge/SH_Selection" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("OutlineColor", Color) = (0.8301887,0.8301887,0.8301887,1)
        _GlowIntensity ("Glow Intensity", Float ) = 2
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
            uniform float4 _TintColor;
            uniform float _GlowIntensity;
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
                float node_225 = step(i.uv0.g,i.uv0.b);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = ((i.vertexColor.rgb*((((1.0 - node_225)*_MainTex_var.r)*0.25)+((node_225*_MainTex_var.r)*_GlowIntensity)))+(_MainTex_var.b*_TintColor.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,_MainTex_var.a);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
