Shader "Pic/Pic_eyeBall_shader" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 3)) = 0.078125
	_Gloss ("Gloss", Range (0, 100)) = 50
	_MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
}
 
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 400
		GrabPass{"_ScreenTex"}
	 
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha
	 
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _ScreenTex;
		fixed4 _Color;
		half _Shininess;
		half _Gloss;
	
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float4 screenPos;
		};
	 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 bg = tex2D(_ScreenTex,IN.screenPos.xy/IN.screenPos.w);
			o.Albedo = bg.rgb*tex.rgb * _Color.rgb;
			o.Alpha = bg.a*tex.a * _Color.a;
			o.Gloss = _Gloss;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap , IN.uv_BumpMap));
		}
		ENDCG
	}
 
FallBack "Transparent/VertexLit"
}
