// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5849056,fgcg:0.5849056,fgcb:0.5849056,fgca:1,fgde:1,fgrn:33.31,fgrf:2191.3,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32231,y:32275,varname:node_4795,prsc:2|emission-7728-OUT;n:type:ShaderForge.SFN_Vector4Property,id:2654,x:29411,y:32797,ptovrint:False,ptlb:SeqSettings,ptin:_SeqSettings,cmnt:XCols YRows ZSpeed,varname:_SeqSettings,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4,v2:4,v3:0.5,v4:0;n:type:ShaderForge.SFN_Multiply,id:2252,x:30022,y:33061,cmnt:CurrentFrame,varname:node_2252,prsc:2|A-3222-OUT,B-6694-OUT;n:type:ShaderForge.SFN_Multiply,id:3222,x:29667,y:32983,cmnt:TotalNumFrames,varname:node_3222,prsc:2|A-424-OUT,B-259-OUT;n:type:ShaderForge.SFN_Relay,id:259,x:29601,y:32876,cmnt:NumCols,varname:node_259,prsc:2|IN-2654-Y;n:type:ShaderForge.SFN_Relay,id:424,x:29601,y:32797,cmnt:NumRows,varname:node_424,prsc:2|IN-2654-X;n:type:ShaderForge.SFN_Divide,id:1835,x:30585,y:33079,varname:node_1835,prsc:2|A-861-OUT,B-424-OUT;n:type:ShaderForge.SFN_Floor,id:1286,x:30773,y:32966,cmnt:CurrentRow,varname:node_1286,prsc:2|IN-1835-OUT;n:type:ShaderForge.SFN_Divide,id:3145,x:30966,y:32862,cmnt:VOffset,varname:node_3145,prsc:2|A-1286-OUT,B-259-OUT;n:type:ShaderForge.SFN_OneMinus,id:8799,x:31133,y:32862,cmnt:TopToBottom,varname:node_8799,prsc:2|IN-3145-OUT;n:type:ShaderForge.SFN_Append,id:9673,x:31342,y:32719,varname:node_9673,prsc:2|A-9071-OUT,B-8799-OUT;n:type:ShaderForge.SFN_Divide,id:9071,x:30966,y:32719,cmnt:UOffset,varname:node_9071,prsc:2|A-2343-OUT,B-424-OUT;n:type:ShaderForge.SFN_Fmod,id:2343,x:30615,y:32614,cmnt:CurrentCol,varname:node_2343,prsc:2|A-861-OUT,B-424-OUT;n:type:ShaderForge.SFN_Append,id:499,x:29929,y:32530,varname:node_499,prsc:2|A-424-OUT,B-259-OUT;n:type:ShaderForge.SFN_TexCoord,id:2628,x:29972,y:32350,varname:node_2628,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Divide,id:2851,x:30183,y:32350,varname:node_2851,prsc:2|A-2628-UVOUT,B-499-OUT;n:type:ShaderForge.SFN_Add,id:100,x:31485,y:32481,varname:node_100,prsc:2|A-2851-OUT,B-9673-OUT;n:type:ShaderForge.SFN_Tex2d,id:4895,x:31818,y:32388,ptovrint:False,ptlb:SequenceTex,ptin:_SequenceTex,varname:_SequenceTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3196496d97c68544ab55106f3f5d811f,ntxv:0,isnm:False|UVIN-100-OUT;n:type:ShaderForge.SFN_Multiply,id:7728,x:32037,y:32375,varname:node_7728,prsc:2|A-4895-RGB,B-3856-RGB,C-8411-Z;n:type:ShaderForge.SFN_VertexColor,id:3856,x:31818,y:32575,varname:node_3856,prsc:2;n:type:ShaderForge.SFN_Ceil,id:861,x:30362,y:32943,varname:node_861,prsc:2|IN-2252-OUT;n:type:ShaderForge.SFN_Frac,id:6694,x:29747,y:33253,varname:node_6694,prsc:2|IN-3205-W;n:type:ShaderForge.SFN_TexCoord,id:3205,x:29546,y:33280,varname:node_3205,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_TexCoord,id:8411,x:31818,y:32709,varname:node_8411,prsc:2,uv:0,uaff:True;proporder:2654-4895;pass:END;sub:END;*/

Shader "Shader Forge/SH_Additive_CustomFlipbook" {
    Properties {
        _SeqSettings ("SeqSettings", Vector) = (4,4,0.5,0)
        _SequenceTex ("SequenceTex", 2D) = "white" {}
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
                float node_424 = _SeqSettings.r; // NumRows
                float node_259 = _SeqSettings.g; // NumCols
                float node_861 = ceil(((node_424*node_259)*frac(i.uv0.a)));
                float2 node_100 = ((i.uv0/float2(node_424,node_259))+float2((fmod(node_861,node_424)/node_424),(1.0 - (floor((node_861/node_424))/node_259))));
                float4 _SequenceTex_var = tex2D(_SequenceTex,TRANSFORM_TEX(node_100, _SequenceTex));
                float3 emissive = (_SequenceTex_var.rgb*i.vertexColor.rgb*i.uv0.b);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5849056,0.5849056,0.5849056,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
