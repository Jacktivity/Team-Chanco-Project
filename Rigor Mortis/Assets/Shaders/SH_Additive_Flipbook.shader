// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33363,y:31490,varname:node_4795,prsc:2|emission-219-OUT;n:type:ShaderForge.SFN_Vector4Property,id:8384,x:29763,y:32495,ptovrint:False,ptlb:SeqSettings,ptin:_SeqSettings,cmnt:XCols YRows ZSpeed,varname:_SeqSettings,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4,v2:4,v3:0.5,v4:0;n:type:ShaderForge.SFN_Relay,id:4690,x:30231,y:32785,cmnt:Speed,varname:node_4690,prsc:2|IN-8384-Z;n:type:ShaderForge.SFN_Multiply,id:6473,x:30825,y:32790,cmnt:CurrentFrame,varname:node_6473,prsc:2|A-9732-OUT,B-8134-OUT;n:type:ShaderForge.SFN_Multiply,id:9732,x:30591,y:32713,cmnt:TotalNumFrames,varname:node_9732,prsc:2|A-2203-OUT,B-7860-OUT;n:type:ShaderForge.SFN_Relay,id:7860,x:30316,y:32613,cmnt:NumCols,varname:node_7860,prsc:2|IN-8384-Y;n:type:ShaderForge.SFN_Relay,id:2203,x:30361,y:32394,cmnt:NumRows,varname:node_2203,prsc:2|IN-8384-X;n:type:ShaderForge.SFN_Divide,id:9548,x:31211,y:32669,varname:node_9548,prsc:2|A-2872-OUT,B-2203-OUT;n:type:ShaderForge.SFN_Round,id:632,x:30972,y:32758,cmnt:CurrentIndex,varname:node_632,prsc:2|IN-6473-OUT;n:type:ShaderForge.SFN_Floor,id:2082,x:31387,y:32669,cmnt:CurrentRow,varname:node_2082,prsc:2|IN-9548-OUT;n:type:ShaderForge.SFN_Divide,id:8128,x:31661,y:32578,cmnt:VOffset,varname:node_8128,prsc:2|A-2082-OUT,B-7860-OUT;n:type:ShaderForge.SFN_OneMinus,id:3571,x:31824,y:32566,cmnt:TopToBottom,varname:node_3571,prsc:2|IN-8128-OUT;n:type:ShaderForge.SFN_Append,id:2333,x:32012,y:32480,varname:node_2333,prsc:2|A-1191-OUT,B-3571-OUT;n:type:ShaderForge.SFN_Divide,id:1191,x:31639,y:32368,cmnt:UOffset,varname:node_1191,prsc:2|A-3179-OUT,B-2203-OUT;n:type:ShaderForge.SFN_Fmod,id:3179,x:31087,y:32484,cmnt:CurrentCol,varname:node_3179,prsc:2|A-2872-OUT,B-2203-OUT;n:type:ShaderForge.SFN_Append,id:5153,x:31305,y:32169,varname:node_5153,prsc:2|A-2203-OUT,B-7860-OUT;n:type:ShaderForge.SFN_TexCoord,id:1382,x:31277,y:31967,varname:node_1382,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Divide,id:7649,x:31529,y:31990,varname:node_7649,prsc:2|A-1382-UVOUT,B-5153-OUT;n:type:ShaderForge.SFN_Add,id:7114,x:32183,y:32265,varname:node_7114,prsc:2|A-7649-OUT,B-2333-OUT;n:type:ShaderForge.SFN_Tex2d,id:1684,x:32484,y:31840,ptovrint:False,ptlb:SequenceTex,ptin:_SequenceTex,varname:_SequenceTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3196496d97c68544ab55106f3f5d811f,ntxv:0,isnm:False|UVIN-7114-OUT;n:type:ShaderForge.SFN_Frac,id:8134,x:30641,y:32920,varname:node_8134,prsc:2|IN-9457-OUT;n:type:ShaderForge.SFN_Multiply,id:9457,x:30409,y:32893,varname:node_9457,prsc:2|A-4690-OUT,B-2032-T;n:type:ShaderForge.SFN_Time,id:2032,x:30161,y:32975,varname:node_2032,prsc:2;n:type:ShaderForge.SFN_Multiply,id:219,x:32758,y:31839,varname:node_219,prsc:2|A-1684-RGB,B-6308-RGB,C-2665-OUT;n:type:ShaderForge.SFN_VertexColor,id:6308,x:32539,y:32039,varname:node_6308,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2665,x:32552,y:32204,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_2665,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Floor,id:2671,x:31049,y:32969,varname:node_2671,prsc:2|IN-6473-OUT;n:type:ShaderForge.SFN_Ceil,id:2872,x:30972,y:33131,varname:node_2872,prsc:2|IN-6473-OUT;proporder:8384-1684-2665;pass:END;sub:END;*/

Shader "Shader Forge/Additive_Flipbook" {
    Properties {
        _SeqSettings ("SeqSettings", Vector) = (4,4,0.5,0)
        _SequenceTex ("SequenceTex", 2D) = "white" {}
        _Glow ("Glow", Float ) = 0
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
            uniform float4 _SeqSettings;
            uniform sampler2D _SequenceTex; uniform float4 _SequenceTex_ST;
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
                float node_2203 = _SeqSettings.r; // NumRows
                float node_7860 = _SeqSettings.g; // NumCols
                float4 node_2032 = _Time;
                float node_6473 = ((node_2203*node_7860)*frac((_SeqSettings.b*node_2032.g))); // CurrentFrame
                float node_2872 = ceil(node_6473);
                float2 node_7114 = ((i.uv0/float2(node_2203,node_7860))+float2((fmod(node_2872,node_2203)/node_2203),(1.0 - (floor((node_2872/node_2203))/node_7860))));
                float4 _SequenceTex_var = tex2D(_SequenceTex,TRANSFORM_TEX(node_7114, _SequenceTex));
                float3 emissive = (_SequenceTex_var.rgb*i.vertexColor.rgb*_Glow);
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
