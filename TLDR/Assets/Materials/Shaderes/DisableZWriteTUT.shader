Shader "Custom/DisableZWriteTUT"

{
	subShader{
		Tags{
		  "RenderType" = "Opaque"
		}
		
		Pass{
			ZWrite Off
	    }
	}
}