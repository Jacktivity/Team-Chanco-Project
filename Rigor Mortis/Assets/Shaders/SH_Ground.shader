// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.2912514,fgcg:0.3294018,fgcb:0.3301887,fgca:1,fgde:0.02,fgrn:20,fgrf:60,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33891,y:32706,varname:node_2865,prsc:2|diff-5796-OUT,spec-8356-OUT,gloss-8356-OUT;n:type:ShaderForge.SFN_Multiply,id:764,x:31704,y:32863,varname:node_764,prsc:2|A-1531-OUT,B-7575-OUT;n:type:ShaderForge.SFN_Vector1,id:8356,x:32972,y:32359,varname:node_8356,prsc:2,v1:0;n:type:ShaderForge.SFN_Code,id:4872,x:31861,y:32862,varname:node_4872,prsc:2,code:IAAgAGYAbABvAGEAdAAzACAAcgBlAHQAIAA9ACAAMAA7AA0ACgAgACAAaQBuAHQAIABpAHQAZQByAGEAdABpAG8AbgBzACAAPQAgADIAOwANAAoAIAAgAGYAbwByACAAKABpAG4AdAAgAGkAIAA9ACAAMAA7ACAAaQAgADwAIABpAHQAZQByAGEAdABpAG8AbgBzADsAIABpACsAKwApAA0ACgAgACAAewANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAcAAgAD0AIABmAGwAbwBvAHIAKABVAFYAIAAqACAAKABpACsAMQApACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAyACAAZgAgAD0AIABmAHIAYQBjACgAVQBWACAAKgAgACgAaQArADEAKQApADsACgAgACAAIAAgACAAZgAgAD0AIABmACAAKgAgAGYAIAAqACAAKAAzAC4AMAAgAC0AIAAyAC4AMAAgACoAIABmACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAAgAG4AIAA9ACAAcAAuAHgAIAArACAAcAAuAHkAIAAqACAANQA3AC4AMAA7AA0ACgAgACAAIAAgACAAZgBsAG8AYQB0ADQAIABuAG8AaQBzAGUAIAA9ACAAZgBsAG8AYQB0ADQAKABuACwAIABuACAAKwAgADEALAAgAG4AIAArACAANQA3AC4AMAAsACAAbgAgACsAIAA1ADgALgAwACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAA0ACAAbgBvAGkAcwBlAFgAIAA9ACAAZgByAGEAYwAoAHMAaQBuACgAbgBvAGkAcwBlACkAKgBYACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAA0ACAAbgBvAGkAcwBlAFkAIAA9ACAAZgByAGEAYwAoAHMAaQBuACgAbgBvAGkAcwBlACkAKgBZACkAOwANAAoAIAAgACAAIAAgAGYAbABvAGEAdAA0ACAAbgBvAGkAcwBlAFoAIAA9ACAAZgByAGEAYwAoAC0AcwBpAG4AKABuAG8AaQBzAGUAKQAqAFoAKQA7AA0ACgAgACAAIAAgACAAcgBlAHQALgB4ACAAKwA9ACAAbABlAHIAcAAoAGwAZQByAHAAKABuAG8AaQBzAGUAWAAuAHgALAAgAG4AbwBpAHMAZQBYAC4AeQAsACAAZgAuAHgAKQAsACAAbABlAHIAcAAoAG4AbwBpAHMAZQBYAC4AegAsACAAbgBvAGkAcwBlAFgALgB3ACwAIABmAC4AeAApACwAIABmAC4AeQApACAAKgAgACgAIABpAHQAZQByAGEAdABpAG8AbgBzACAALwAgACgAaQArADEAKQApADsADQAKACAAIAAgACAAIAByAGUAdAAuAHkAIAArAD0AIABsAGUAcgBwACgAbABlAHIAcAAoAG4AbwBpAHMAZQBZAC4AeAAsACAAbgBvAGkAcwBlAFkALgB5ACwAIABmAC4AeAApACwAIABsAGUAcgBwACgAbgBvAGkAcwBlAFkALgB6ACwAIABuAG8AaQBzAGUAWQAuAHcALAAgAGYALgB4ACkALAAgAGYALgB5ACkAIAAqACAAKAAgAGkAdABlAHIAYQB0AGkAbwBuAHMAIAAvACAAKABpACsAMQApACkAOwAKACAAIAAgACAAIAByAGUAdAAuAHoAIAArAD0AIABsAGUAcgBwACgAbABlAHIAcAAoAG4AbwBpAHMAZQBaAC4AeAAsACAAbgBvAGkAcwBlAFoALgB5ACwAIABmAC4AeAApACwAIABsAGUAcgBwACgAbgBvAGkAcwBlAFoALgB6ACwAIABuAG8AaQBzAGUAWgAuAHcALAAgAGYALgB4ACkALAAgAGYALgB5ACkAIAAqACAAKAAgAGkAdABlAHIAYQB0AGkAbwBuAHMAIAAvACAAKABpACsAMQApACkAOwAKACAAIAB9AA0ACgAgACAAcgBlAHQAdQByAG4AIAByAGUAdAAvAGkAdABlAHIAYQB0AGkAbwBuAHMAOwA=,output:2,fname:Noise,width:936,height:276,input:1,input:0,input:0,input:0,input_1_label:UV,input_2_label:X,input_3_label:Y,input_4_label:Z|A-764-OUT,B-3546-OUT,C-4476-OUT,D-6063-OUT;n:type:ShaderForge.SFN_Tex2d,id:1645,x:32851,y:33180,ptovrint:False,ptlb:GLayer,ptin:_GLayer,varname:node_1645,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3c80981eef29454469ee944546953ca6,ntxv:0,isnm:False|UVIN-1531-OUT;n:type:ShaderForge.SFN_Tex2d,id:9656,x:32851,y:32999,ptovrint:False,ptlb:RLayer,ptin:_RLayer,varname:_node_1645_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:acea371903dcc2d4c82477f5fbacac52,ntxv:0,isnm:False|UVIN-1531-OUT;n:type:ShaderForge.SFN_Tex2d,id:1508,x:32851,y:33544,ptovrint:False,ptlb:Base,ptin:_Base,varname:node_1508,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d23e40a3fbc842b4f8a09f54b580177a,ntxv:0,isnm:False|UVIN-1531-OUT;n:type:ShaderForge.SFN_Tex2d,id:3316,x:32851,y:33362,ptovrint:False,ptlb:BLayer,ptin:_BLayer,varname:_GLayer_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3c80981eef29454469ee944546953ca6,ntxv:0,isnm:False|UVIN-1531-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9471,x:33226,y:32633,ptovrint:False,ptlb:Desaturate,ptin:_Desaturate,varname:node_9471,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Desaturate,id:5796,x:33610,y:32623,varname:node_5796,prsc:2|COL-4153-OUT,DES-9471-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7571,x:31157,y:32620,varname:node_7571,prsc:2;n:type:ShaderForge.SFN_ChannelBlend,id:4153,x:33504,y:32867,varname:node_4153,prsc:2,chbt:1|M-2715-OUT,R-9656-RGB,G-1645-RGB,B-3316-RGB,BTM-1508-RGB;n:type:ShaderForge.SFN_Append,id:1531,x:31354,y:32689,varname:node_1531,prsc:2|A-7571-X,B-7571-Z;n:type:ShaderForge.SFN_Clamp01,id:6951,x:32945,y:32861,varname:node_6951,prsc:2|IN-8036-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7575,x:31384,y:32983,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_7575,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Color,id:881,x:33164,y:33251,ptovrint:False,ptlb:node_881,ptin:_node_881,varname:node_881,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:9590,x:32874,y:32657,ptovrint:False,ptlb:node_9590,ptin:_node_9590,varname:node_9590,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ComponentMask,id:2715,x:33172,y:32861,varname:node_2715,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-6951-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3546,x:31461,y:33080,ptovrint:False,ptlb:XMult,ptin:_XMult,varname:node_3546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:37.58545;n:type:ShaderForge.SFN_ValueProperty,id:4476,x:31425,y:33165,ptovrint:False,ptlb:YMult,ptin:_YMult,varname:_XMult_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:37.58545;n:type:ShaderForge.SFN_ValueProperty,id:6063,x:31501,y:33252,ptovrint:False,ptlb:ZMult,ptin:_ZMult,varname:_YMult_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:37.58545;n:type:ShaderForge.SFN_OneMinus,id:8036,x:32764,y:32761,varname:node_8036,prsc:2|IN-4872-OUT;proporder:1645-9656-1508-3316-9471-7575-881-9590-3546-4476-6063;pass:END;sub:END;*/

Shader "Shader Forge/SH_Ground" {
    Properties {
        _GLayer ("GLayer", 2D) = "white" {}
        _RLayer ("RLayer", 2D) = "white" {}
        _Base ("Base", 2D) = "white" {}
        _BLayer ("BLayer", 2D) = "white" {}
        _Desaturate ("Desaturate", Float ) = 0
        _Scale ("Scale", Float ) = 0.5
        _node_881 ("node_881", Color) = (1,1,1,1)
        _node_9590 ("node_9590", Color) = (1,1,1,1)
        _XMult ("XMult", Float ) = 37.58545
        _YMult ("YMult", Float ) = 37.58545
        _ZMult ("ZMult", Float ) = 37.58545
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            float3 Noise( float2 UV , float X , float Y , float Z ){
              float3 ret = 0;
              int iterations = 2;
              for (int i = 0; i < iterations; i++)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 float4 noiseX = frac(sin(noise)*X);
                 float4 noiseY = frac(sin(noise)*Y);
                 float4 noiseZ = frac(-sin(noise)*Z);
                 ret.x += lerp(lerp(noiseX.x, noiseX.y, f.x), lerp(noiseX.z, noiseX.w, f.x), f.y) * ( iterations / (i+1));
                 ret.y += lerp(lerp(noiseY.x, noiseY.y, f.x), lerp(noiseY.z, noiseY.w, f.x), f.y) * ( iterations / (i+1));
                 ret.z += lerp(lerp(noiseZ.x, noiseZ.y, f.x), lerp(noiseZ.z, noiseZ.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform sampler2D _GLayer; uniform float4 _GLayer_ST;
            uniform sampler2D _RLayer; uniform float4 _RLayer_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _BLayer; uniform float4 _BLayer_ST;
            uniform float _Desaturate;
            uniform float _Scale;
            uniform float _XMult;
            uniform float _YMult;
            uniform float _ZMult;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float node_8356 = 0.0;
                float gloss = node_8356;
                float perceptualRoughness = 1.0 - node_8356;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = node_8356;
                float specularMonochrome;
                float2 node_1531 = float2(i.posWorld.r,i.posWorld.b);
                float3 node_4872 = Noise( (node_1531*_Scale) , _XMult , _YMult , _ZMult );
                float3 node_2715 = saturate((1.0 - node_4872)).rgb;
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(node_1531, _Base));
                float4 _RLayer_var = tex2D(_RLayer,TRANSFORM_TEX(node_1531, _RLayer));
                float4 _GLayer_var = tex2D(_GLayer,TRANSFORM_TEX(node_1531, _GLayer));
                float4 _BLayer_var = tex2D(_BLayer,TRANSFORM_TEX(node_1531, _BLayer));
                float3 diffuseColor = lerp((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),dot((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),float3(0.3,0.59,0.11)),_Desaturate); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            float3 Noise( float2 UV , float X , float Y , float Z ){
              float3 ret = 0;
              int iterations = 2;
              for (int i = 0; i < iterations; i++)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 float4 noiseX = frac(sin(noise)*X);
                 float4 noiseY = frac(sin(noise)*Y);
                 float4 noiseZ = frac(-sin(noise)*Z);
                 ret.x += lerp(lerp(noiseX.x, noiseX.y, f.x), lerp(noiseX.z, noiseX.w, f.x), f.y) * ( iterations / (i+1));
                 ret.y += lerp(lerp(noiseY.x, noiseY.y, f.x), lerp(noiseY.z, noiseY.w, f.x), f.y) * ( iterations / (i+1));
                 ret.z += lerp(lerp(noiseZ.x, noiseZ.y, f.x), lerp(noiseZ.z, noiseZ.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform sampler2D _GLayer; uniform float4 _GLayer_ST;
            uniform sampler2D _RLayer; uniform float4 _RLayer_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _BLayer; uniform float4 _BLayer_ST;
            uniform float _Desaturate;
            uniform float _Scale;
            uniform float _XMult;
            uniform float _YMult;
            uniform float _ZMult;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float node_8356 = 0.0;
                float gloss = node_8356;
                float perceptualRoughness = 1.0 - node_8356;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = node_8356;
                float specularMonochrome;
                float2 node_1531 = float2(i.posWorld.r,i.posWorld.b);
                float3 node_4872 = Noise( (node_1531*_Scale) , _XMult , _YMult , _ZMult );
                float3 node_2715 = saturate((1.0 - node_4872)).rgb;
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(node_1531, _Base));
                float4 _RLayer_var = tex2D(_RLayer,TRANSFORM_TEX(node_1531, _RLayer));
                float4 _GLayer_var = tex2D(_GLayer,TRANSFORM_TEX(node_1531, _GLayer));
                float4 _BLayer_var = tex2D(_BLayer,TRANSFORM_TEX(node_1531, _BLayer));
                float3 diffuseColor = lerp((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),dot((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),float3(0.3,0.59,0.11)),_Desaturate); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            float3 Noise( float2 UV , float X , float Y , float Z ){
              float3 ret = 0;
              int iterations = 2;
              for (int i = 0; i < iterations; i++)
              {
                 float2 p = floor(UV * (i+1));
                 float2 f = frac(UV * (i+1));
                 f = f * f * (3.0 - 2.0 * f);
                 float n = p.x + p.y * 57.0;
                 float4 noise = float4(n, n + 1, n + 57.0, n + 58.0);
                 float4 noiseX = frac(sin(noise)*X);
                 float4 noiseY = frac(sin(noise)*Y);
                 float4 noiseZ = frac(-sin(noise)*Z);
                 ret.x += lerp(lerp(noiseX.x, noiseX.y, f.x), lerp(noiseX.z, noiseX.w, f.x), f.y) * ( iterations / (i+1));
                 ret.y += lerp(lerp(noiseY.x, noiseY.y, f.x), lerp(noiseY.z, noiseY.w, f.x), f.y) * ( iterations / (i+1));
                 ret.z += lerp(lerp(noiseZ.x, noiseZ.y, f.x), lerp(noiseZ.z, noiseZ.w, f.x), f.y) * ( iterations / (i+1));
              }
              return ret/iterations;
            }
            
            uniform sampler2D _GLayer; uniform float4 _GLayer_ST;
            uniform sampler2D _RLayer; uniform float4 _RLayer_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _BLayer; uniform float4 _BLayer_ST;
            uniform float _Desaturate;
            uniform float _Scale;
            uniform float _XMult;
            uniform float _YMult;
            uniform float _ZMult;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float2 node_1531 = float2(i.posWorld.r,i.posWorld.b);
                float3 node_4872 = Noise( (node_1531*_Scale) , _XMult , _YMult , _ZMult );
                float3 node_2715 = saturate((1.0 - node_4872)).rgb;
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(node_1531, _Base));
                float4 _RLayer_var = tex2D(_RLayer,TRANSFORM_TEX(node_1531, _RLayer));
                float4 _GLayer_var = tex2D(_GLayer,TRANSFORM_TEX(node_1531, _GLayer));
                float4 _BLayer_var = tex2D(_BLayer,TRANSFORM_TEX(node_1531, _BLayer));
                float3 diffColor = lerp((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),dot((lerp( lerp( lerp( _Base_var.rgb, _RLayer_var.rgb, node_2715.r ), _GLayer_var.rgb, node_2715.g ), _BLayer_var.rgb, node_2715.b )),float3(0.3,0.59,0.11)),_Desaturate);
                float specularMonochrome;
                float3 specColor;
                float node_8356 = 0.0;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, node_8356, specColor, specularMonochrome );
                float roughness = 1.0 - node_8356;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
