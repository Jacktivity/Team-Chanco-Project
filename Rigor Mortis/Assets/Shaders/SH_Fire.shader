// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5849056,fgcg:0.5849056,fgcb:0.5849056,fgca:1,fgde:1,fgrn:33.31,fgrf:2191.3,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34838,y:32178,varname:node_4795,prsc:2|emission-3554-OUT,alpha-1100-OUT;n:type:ShaderForge.SFN_TexCoord,id:5228,x:31170,y:32529,varname:node_5228,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:5357,x:31977,y:32529,varname:node_5357,prsc:2|A-3435-RGB,B-6902-RGB;n:type:ShaderForge.SFN_Tex2d,id:3435,x:31748,y:32529,ptovrint:False,ptlb:SmallBubbles,ptin:_SmallBubbles,varname:node_3435,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:736e7e7ce2dff2248bc7338091d7d84d,ntxv:0,isnm:False|UVIN-467-UVOUT;n:type:ShaderForge.SFN_Panner,id:467,x:31545,y:32529,varname:node_467,prsc:2,spu:0,spv:-1|UVIN-3537-OUT;n:type:ShaderForge.SFN_Tex2d,id:6902,x:31749,y:32796,ptovrint:False,ptlb:BigBubbles,ptin:_BigBubbles,varname:_node_3435_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:736e7e7ce2dff2248bc7338091d7d84d,ntxv:0,isnm:False|UVIN-3761-UVOUT;n:type:ShaderForge.SFN_Panner,id:3761,x:31550,y:32796,varname:node_3761,prsc:2,spu:0,spv:-0.5|UVIN-3671-OUT;n:type:ShaderForge.SFN_TexCoord,id:2523,x:31170,y:32796,varname:node_2523,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3671,x:31366,y:32796,varname:node_3671,prsc:2|A-2523-UVOUT,B-4478-OUT;n:type:ShaderForge.SFN_Vector1,id:4478,x:31170,y:32951,varname:node_4478,prsc:2,v1:0.75;n:type:ShaderForge.SFN_Multiply,id:3537,x:31368,y:32529,varname:node_3537,prsc:2|A-5228-UVOUT,B-206-OUT;n:type:ShaderForge.SFN_Vector1,id:206,x:31170,y:32682,varname:node_206,prsc:2,v1:1.2;n:type:ShaderForge.SFN_Tex2d,id:2221,x:31962,y:32269,ptovrint:False,ptlb:FireMask,ptin:_FireMask,varname:node_2221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a233d55946b5cb4ea3cfa19b485c858,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:5029,x:32470,y:32310,varname:node_5029,prsc:2|A-951-OUT,B-2221-RGB;n:type:ShaderForge.SFN_Multiply,id:951,x:32259,y:32529,varname:node_951,prsc:2|A-5357-OUT,B-2221-RGB;n:type:ShaderForge.SFN_ComponentMask,id:4783,x:32654,y:32310,varname:node_4783,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5029-OUT;n:type:ShaderForge.SFN_If,id:4447,x:33478,y:32345,varname:node_4447,prsc:2|A-4783-R,B-8868-OUT,GT-3851-OUT,EQ-4783-R,LT-74-OUT;n:type:ShaderForge.SFN_If,id:3227,x:33014,y:32635,varname:node_3227,prsc:2|A-4783-G,B-8412-OUT,GT-9225-OUT,EQ-4783-G,LT-9792-OUT;n:type:ShaderForge.SFN_Slider,id:8412,x:32547,y:32660,ptovrint:False,ptlb:OuterFlame,ptin:_OuterFlame,varname:node_8412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2539177,max:1;n:type:ShaderForge.SFN_Slider,id:8868,x:33002,y:32169,ptovrint:False,ptlb:InnerFlame,ptin:_InnerFlame,varname:node_8868,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Vector3,id:9225,x:32704,y:32739,varname:node_9225,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:9792,x:32704,y:32833,varname:node_9792,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:3851,x:33240,y:32486,varname:node_3851,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Vector3,id:74,x:33240,y:32583,varname:node_74,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:7029,x:32978,y:32821,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_7029,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector3,id:8845,x:33330,y:32020,varname:node_8845,prsc:2,v1:1,v2:0.9369487,v3:0;n:type:ShaderForge.SFN_Multiply,id:3587,x:33899,y:32229,varname:node_3587,prsc:2|A-8845-OUT,B-4447-OUT;n:type:ShaderForge.SFN_Multiply,id:8015,x:33376,y:32770,varname:node_8015,prsc:2|A-3227-OUT,B-7029-OUT;n:type:ShaderForge.SFN_OneMinus,id:2214,x:33775,y:31979,varname:node_2214,prsc:2|IN-4447-OUT;n:type:ShaderForge.SFN_Vector3,id:8536,x:33357,y:31869,varname:node_8536,prsc:2,v1:1,v2:0.7017373,v3:0;n:type:ShaderForge.SFN_Multiply,id:6011,x:33582,y:31717,varname:node_6011,prsc:2|A-222-OUT,B-8536-OUT;n:type:ShaderForge.SFN_TexCoord,id:8014,x:33027,y:31653,varname:node_8014,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:8227,x:33197,y:31653,varname:node_8227,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-8014-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:222,x:33357,y:31653,varname:node_222,prsc:2|IN-8227-OUT;n:type:ShaderForge.SFN_Multiply,id:1691,x:34027,y:31840,varname:node_1691,prsc:2|A-6011-OUT,B-2214-OUT;n:type:ShaderForge.SFN_Add,id:2825,x:34231,y:32008,varname:node_2825,prsc:2|A-1691-OUT,B-3587-OUT;n:type:ShaderForge.SFN_Multiply,id:3554,x:34465,y:32169,varname:node_3554,prsc:2|A-2825-OUT,B-8015-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1100,x:34085,y:32670,varname:node_1100,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8015-OUT;proporder:3435-6902-2221-8868-8412-7029;pass:END;sub:END;*/

Shader "Shader Forge/SH_Fire" {
    Properties {
        _SmallBubbles ("SmallBubbles", 2D) = "white" {}
        _BigBubbles ("BigBubbles", 2D) = "white" {}
        _FireMask ("FireMask", 2D) = "white" {}
        _InnerFlame ("InnerFlame", Range(0, 1)) = 0.5
        _OuterFlame ("OuterFlame", Range(0, 1)) = 0.2539177
        _Opacity ("Opacity", Range(0, 1)) = 1
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
            uniform sampler2D _SmallBubbles; uniform float4 _SmallBubbles_ST;
            uniform sampler2D _BigBubbles; uniform float4 _BigBubbles_ST;
            uniform sampler2D _FireMask; uniform float4 _FireMask_ST;
            uniform float _OuterFlame;
            uniform float _InnerFlame;
            uniform float _Opacity;
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
                float4 node_7895 = _Time;
                float2 node_467 = ((i.uv0*1.2)+node_7895.g*float2(0,-1));
                float4 _SmallBubbles_var = tex2D(_SmallBubbles,TRANSFORM_TEX(node_467, _SmallBubbles));
                float2 node_3761 = ((i.uv0*0.75)+node_7895.g*float2(0,-0.5));
                float4 _BigBubbles_var = tex2D(_BigBubbles,TRANSFORM_TEX(node_3761, _BigBubbles));
                float4 _FireMask_var = tex2D(_FireMask,TRANSFORM_TEX(i.uv0, _FireMask));
                float2 node_4783 = (((_SmallBubbles_var.rgb*_BigBubbles_var.rgb)*_FireMask_var.rgb)+_FireMask_var.rgb).rg;
                float node_4447_if_leA = step(node_4783.r,_InnerFlame);
                float node_4447_if_leB = step(_InnerFlame,node_4783.r);
                float3 node_4447 = lerp((node_4447_if_leA*float3(0,0,0))+(node_4447_if_leB*float3(1,1,1)),node_4783.r,node_4447_if_leA*node_4447_if_leB);
                float node_3227_if_leA = step(node_4783.g,_OuterFlame);
                float node_3227_if_leB = step(_OuterFlame,node_4783.g);
                float3 node_8015 = (lerp((node_3227_if_leA*float3(0,0,0))+(node_3227_if_leB*float3(1,1,1)),node_4783.g,node_3227_if_leA*node_3227_if_leB)*_Opacity);
                float3 emissive = (((((1.0 - i.uv0.g)*float3(1,0.7017373,0))*(1.0 - node_4447))+(float3(1,0.9369487,0)*node_4447))*node_8015);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_8015.r);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5849056,0.5849056,0.5849056,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
