Shader "Custom/Texture Creator"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
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

			static const fixed3 WHITE_COLOR3 = fixed3(1,1,1);
			static const fixed4 BLACK_COLOR = fixed4(0,0,0,1);
			static const fixed4 BLACK_COLOR_TR = fixed4(0,0,0,0);

            fixed4 frag(fragmentIn i) : SV_Target
            {
			    // i.texcoord = [0;1]
				float cx = i.texcoord.x - 0.5;
				float cy = i.texcoord.y - 0.5;
				float cr = cx * cx + cy * cy;

				if (cr > 0.25)
				    return BLACK_COLOR_TR;

				if (cr > 0.23)
				    return BLACK_COLOR;

				fixed4 c;
				
				if (cr < 0.02)
				    c.a = 0.8;
				else
					c.a = 1;

				// gets [1;0] from [0;0.25] radial distance
				cr = 1 - 4 * cr;
				c.rgb = _Color.rgb * cr + (WHITE_COLOR3 - _Color.rgb)* (1 - cr);
				
                return c;
            }
            
            ENDCG
        }
    }
}