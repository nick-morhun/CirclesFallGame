Shader "Custom/Texture Creator"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,0.5)
        _MainTex ("Texture", 2D) = "white" { }
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct vertexIn
            {
                float4 position : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            
            struct fragmentIn
            {
                float4 position : SV_Position;
                float2 texcoord : TEXCOORD0;
            };
            
            fragmentIn vert(vertexIn v)
            {
                fragmentIn o;
                o.position = mul(UNITY_MATRIX_MVP, v.position);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(fragmentIn i) : SV_Target
            {
			    // i.texcoord = [0;1]
				float cx = i.texcoord.x - 0.5;
				float cy = i.texcoord.y - 0.5;
				float cr = cx * cx + cy * cy;

				if (cr > 0.25f)
				    return fixed4(0,0,0,0);

				if (cr > 0.23f)
				    return fixed4(0,0,0,1);

				if (cr < 0.02f)
				    return fixed4(.9,.9,.9,.1);

				// result in [0;1]
				cr = 1 - 4 * cr;

				fixed4 c;
				c.rgb = _Color.rgb * cr;
                return c;
            }
            
            ENDCG
        }
    }
}