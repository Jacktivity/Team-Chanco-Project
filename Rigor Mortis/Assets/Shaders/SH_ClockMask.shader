// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:4,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:1,fgcb:1,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32966,y:33135,varname:node_4795,prsc:2|emission-884-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31737,y:32230,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32106,y:32232,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31656,y:32439,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Lerp,id:6958,x:32326,y:32232,varname:node_6958,prsc:2|A-8003-OUT,B-2393-OUT,T-2797-OUT;n:type:ShaderForge.SFN_Multiply,id:2797,x:32026,y:32489,varname:node_2797,prsc:2|A-6074-A,B-2053-A;n:type:ShaderForge.SFN_Vector1,id:8003,x:32037,y:32155,varname:node_8003,prsc:2,v1:1;n:type:ShaderForge.SFN_TexCoord,id:3130,x:30974,y:33405,varname:node_3130,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:2355,x:31633,y:33402,ptovrint:False,ptlb:Health,ptin:_Health,varname:node_2355,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:902,x:31174,y:33405,varname:node_902,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3130-UVOUT;n:type:ShaderForge.SFN_Length,id:6970,x:31809,y:33688,varname:node_6970,prsc:2|IN-902-OUT;n:type:ShaderForge.SFN_Floor,id:1348,x:32001,y:33688,varname:node_1348,prsc:2|IN-6970-OUT;n:type:ShaderForge.SFN_OneMinus,id:8887,x:32175,y:33688,varname:node_8887,prsc:2|IN-1348-OUT;n:type:ShaderForge.SFN_Add,id:1660,x:32001,y:33555,varname:node_1660,prsc:2|A-8080-OUT,B-6970-OUT;n:type:ShaderForge.SFN_Vector1,id:8080,x:31809,y:33555,varname:node_8080,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Floor,id:7084,x:32175,y:33555,varname:node_7084,prsc:2|IN-1660-OUT;n:type:ShaderForge.SFN_Multiply,id:4271,x:32370,y:33555,varname:node_4271,prsc:2|A-7084-OUT,B-8887-OUT;n:type:ShaderForge.SFN_ArcTan2,id:7715,x:31813,y:33231,varname:node_7715,prsc:2,attp:2|A-7992-G,B-7992-R;n:type:ShaderForge.SFN_ComponentMask,id:7992,x:31633,y:33231,varname:node_7992,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-902-OUT;n:type:ShaderForge.SFN_Ceil,id:2350,x:32147,y:33231,varname:node_2350,prsc:2|IN-8423-OUT;n:type:ShaderForge.SFN_Subtract,id:8423,x:31983,y:33231,varname:node_8423,prsc:2|A-7715-OUT,B-2355-OUT;n:type:ShaderForge.SFN_OneMinus,id:119,x:32309,y:33231,varname:node_119,prsc:2|IN-2350-OUT;n:type:ShaderForge.SFN_Multiply,id:884,x:32628,y:33234,varname:node_884,prsc:2|A-119-OUT,B-4271-OUT;proporder:6074-2355;pass:END;sub:END;*/

Shader "Shader Forge/SH_ClockMask" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Health ("Health", Range(0, 1)) = 1
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
            Blend DstColor Zero
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
            uniform float _Health;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_902 = (i.uv0*2.0+-1.0);
                float2 node_7992 = node_902.rg;
                float node_7715 = ((atan2(node_7992.g,node_7992.r)/6.28318530718)+0.5);
                float node_6970 = length(node_902);
                float node_7084 = floor((0.3+node_6970));
                float node_884 = ((1.0 - ceil((node_7715-_Health)))*(node_7084*(1.0 - floor(node_6970))));
                float3 emissive = float3(node_884,node_884,node_884);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(1,1,1,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
