// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33180,y:32551,varname:node_4795,prsc:2|emission-4683-OUT,clip-884-OUT;n:type:ShaderForge.SFN_TexCoord,id:3130,x:31102,y:33406,varname:node_3130,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:902,x:31289,y:33406,varname:node_902,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3130-UVOUT;n:type:ShaderForge.SFN_Length,id:6970,x:31809,y:33688,varname:node_6970,prsc:2|IN-902-OUT;n:type:ShaderForge.SFN_Floor,id:1348,x:32001,y:33688,varname:node_1348,prsc:2|IN-6970-OUT;n:type:ShaderForge.SFN_OneMinus,id:8887,x:32175,y:33688,varname:node_8887,prsc:2|IN-1348-OUT;n:type:ShaderForge.SFN_Add,id:1660,x:32001,y:33555,varname:node_1660,prsc:2|A-8722-OUT,B-6970-OUT;n:type:ShaderForge.SFN_Floor,id:7084,x:32175,y:33555,varname:node_7084,prsc:2|IN-1660-OUT;n:type:ShaderForge.SFN_Multiply,id:4271,x:32370,y:33555,varname:node_4271,prsc:2|A-7084-OUT,B-8887-OUT;n:type:ShaderForge.SFN_ArcTan2,id:7715,x:31631,y:33084,varname:node_7715,prsc:2,attp:2|A-7992-G,B-7992-R;n:type:ShaderForge.SFN_ComponentMask,id:7992,x:31432,y:33109,varname:node_7992,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-902-OUT;n:type:ShaderForge.SFN_Ceil,id:2350,x:32138,y:33130,varname:node_2350,prsc:2|IN-8423-OUT;n:type:ShaderForge.SFN_Subtract,id:8423,x:31974,y:33130,varname:node_8423,prsc:2|A-1229-OUT,B-4331-W;n:type:ShaderForge.SFN_OneMinus,id:119,x:32301,y:33130,varname:node_119,prsc:2|IN-2350-OUT;n:type:ShaderForge.SFN_Multiply,id:884,x:32597,y:33129,varname:node_884,prsc:2|A-119-OUT,B-4271-OUT;n:type:ShaderForge.SFN_TexCoord,id:4331,x:31813,y:33285,varname:node_4331,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Slider,id:8722,x:31656,y:33563,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_8722,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:3467,x:31900,y:32200,ptovrint:False,ptlb:Texture_copy,ptin:_Texture_copy,varname:_Texture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:98b5924828fff2d4ba528d3d11b065a8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:9087,x:31900,y:32373,varname:node_9087,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:3489,x:31900,y:32512,varname:node_3489,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Multiply,id:4683,x:32412,y:32201,varname:node_4683,prsc:2|A-3467-RGB,B-9087-RGB,C-9087-A,D-3489-Z;n:type:ShaderForge.SFN_OneMinus,id:1229,x:31813,y:33094,varname:node_1229,prsc:2|IN-7715-OUT;proporder:8722-3467;pass:END;sub:END;*/

Shader "Shader Forge/SH_ClockMask" {
    Properties {
        _Size ("Size", Range(0, 1)) = 0
        _Texture_copy ("Texture_copy", 2D) = "white" {}
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
            uniform float _Size;
            uniform sampler2D _Texture_copy; uniform float4 _Texture_copy_ST;
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
                float2 node_902 = (i.uv0*2.0+-1.0);
                float2 node_7992 = node_902.rg;
                float node_6970 = length(node_902);
                float node_884 = ((1.0 - ceil(((1.0 - ((atan2(node_7992.g,node_7992.r)/6.28318530718)+0.5))-i.uv0.a)))*(floor((_Size+node_6970))*(1.0 - floor(node_6970))));
                clip(node_884 - 0.5);
////// Lighting:
////// Emissive:
                float4 _Texture_copy_var = tex2D(_Texture_copy,TRANSFORM_TEX(i.uv0, _Texture_copy));
                float3 emissive = (_Texture_copy_var.rgb*i.vertexColor.rgb*i.vertexColor.a*i.uv0.b);
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
