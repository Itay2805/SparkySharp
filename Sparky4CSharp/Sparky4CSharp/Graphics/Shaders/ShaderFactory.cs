using SP.Graphics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Graphics.Shaders
{
    public class ShaderFactory
    {

        private const string batchRendererShaderGL = @"(
#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 uv;
layout (location = 2) in vec2 mask_uv;
layout (location = 3) in float tid;
layout (location = 4) in float mid;
layout (location = 5) in vec4 color;

uniform mat4 sys_ProjectionMatrix;
uniform mat4 sys_ViewMatrix;
uniform mat4 sys_ModelMatrix;

uniform mat4 sys_MaskMatrix;

out DATA
{
	vec4 position;
	vec2 uv;
	vec2 mask_uv;
	float tid;
	float mid;
	vec4 color;
} vs_out;

void main()
{
	gl_Position = sys_ProjectionMatrix * position;
	vs_out.position = position;
	vs_out.uv = uv;
	vs_out.tid = tid;
	vs_out.mid = mid;
	vs_out.color = color;
	vs_out.mask_uv = mask_uv;
};

#shader fragment
#version 330 core

layout (location = 0) out vec4 color;

in DATA
{
	vec4 position;
	vec2 uv;
	vec2 mask_uv;
	float tid;
	float mid;
	vec4 color;
} fs_in;

uniform sampler2D textures[32];

void main()
{
	vec4 texColor = fs_in.color;
	vec4 maskColor = vec4(1.0, 1.0, 1.0, 1.0);
	if (fs_in.tid > 0.0)
	{
		int tid = int(fs_in.tid - 0.5);
		texColor = fs_in.color * texture(textures[tid], fs_in.uv);
	}
	if (fs_in.mid > 0.0)
	{
		int mid = int(fs_in.mid - 0.5);
		maskColor = texture(textures[mid], fs_in.mask_uv);
	}
	color = texColor * maskColor; // vec4(1.0 - maskColor.x, 1.0 - maskColor.y, 1.0 - maskColor.z, 1.0);
};
)";
        private const string batchRendererShaderD3D = "";

        private const string simpleShader = @"(
#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 uv;
layout (location = 2) in vec2 mask_uv;
layout (location = 3) in float tid;
layout (location = 4) in float mid;
layout (location = 5) in vec4 color;

uniform mat4 pr_matrix;

out DATA
{
	vec2 uv;
} vs_out;

void main()
{
	gl_Position = pr_matrix * position;
	vs_out.uv = uv;
};

#shader fragment
#version 330 core

layout (location = 0) out vec4 color;

uniform sampler2D u_Texture;

in DATA
{
	vec2 uv;
} fs_in;

void main()
{
	color = texture(u_Texture, fs_in.uv);
};
)";

        private const string basicLightShader = @"(
#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 uv;
layout (location = 2) in float tid;
layout (location = 3) in vec4 color;

uniform mat4 pr_matrix;
uniform mat4 vw_matrix = mat4(1.0);
uniform mat4 ml_matrix = mat4(1.0);

out DATA
{
	vec4 position;
	vec2 uv;
	float tid;
	vec4 color;
} vs_out;

void main()
{
	gl_Position = pr_matrix * vw_matrix * ml_matrix * position;
	vs_out.position = ml_matrix * position;
	vs_out.uv = uv;
	vs_out.tid = tid;
	vs_out.color = color;
};

#shader fragment
#version 330 core

layout (location = 0) out vec4 color;

uniform vec4 colour;
uniform vec2 light_pos;

in DATA
{
	vec4 position;
	vec2 uv;
	float tid;
	vec4 color;
} fs_in;

uniform sampler2D textures[32];

void main()
{
	float intensity = 1.0 / length(fs_in.position.xy - light_pos);
	vec4 texColor = fs_in.color;
	if (fs_in.tid > 0.0)
	{
		int32 tid = int32(fs_in.tid - 0.5);
		texColor = fs_in.color * texture(textures[tid], fs_in.uv);
	}
	color = texColor * intensity;
};
)";

        private const string geometryPassShader = @"(
#shader vertex
#version 330 core

layout(location = 0) in vec4 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 uv;

uniform mat4 pr_matrix;
uniform mat4 vw_matrix = mat4(1.0);
uniform mat4 ml_matrix = mat4(1.0);

out DATA
{
	vec3 position;
	vec3 normal;
	vec2 uv;
} vs_out;

void main()
{
	gl_Position = pr_matrix * vw_matrix * ml_matrix * position;
	vs_out.position = vec3(ml_matrix * position);
	vs_out.normal = vec3(ml_matrix * vec4(normal, 0.0));
	vs_out.uv = uv;
};

#shader fragment
#version 330 core

layout(location = 0) out vec3 position;   
layout(location = 1) out vec3 diffuse;     
layout(location = 2) out vec3 normal;     
layout(location = 3) out vec3 uv;    

uniform sampler2D u_Texture;

in DATA
{
	vec3 position;
	vec3 normal;
	vec2 uv;
} fs_in;

void main()
{
	position = fs_in.position;
	diffuse = vec3(texture(u_Texture, fs_in.uv));
	normal = normalize(fs_in.normal);
	uv = vec3(fs_in.uv, 0.0);
};
)";

        private const string debugShader = @"(
#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 uv;
layout (location = 2) in vec2 mask_uv;
layout (location = 3) in float tid;
layout (location = 4) in float mid;
layout (location = 5) in vec4 color;

uniform mat4 pr_matrix;
uniform mat4 mask_matrix;

out DATA
{
	vec4 position;
	vec2 uv;
	vec2 mask_uv;
	float tid;
	float mid;
	vec4 color;
} vs_out;

void main()
{
	gl_Position = pr_matrix * position;
	vs_out.position = position;
	vs_out.uv = uv;
	vs_out.tid = tid;
	vs_out.mid = mid;
	vs_out.color = color;
	vs_out.mask_uv = mask_uv;
};

#shader fragment
#version 330 core

layout (location = 0) out vec4 color;

in DATA
{
	vec4 position;
	vec2 uv;
	vec2 mask_uv;
	float tid;
	float mid;
	vec4 color;
} fs_in;

uniform sampler2D textures[32];

void main()
{
	vec4 texColor = fs_in.color;
	vec4 maskColor = vec4(1.0, 1.0, 1.0, 1.0);
	if (fs_in.tid > 0.0)
	{
		int tid = int(fs_in.tid - 0.5);
		texColor = fs_in.color * texture(textures[tid], fs_in.uv);
	}
	if (fs_in.mid > 0.0)
	{
		int mid = int(fs_in.mid - 0.5);
		maskColor = texture(textures[mid], fs_in.mask_uv);
	}
	color = texColor /* maskColor*/; // vec4(1.0 - maskColor.x, 1.0 - maskColor.y, 1.0 - maskColor.z, 1.0);
};
)";


        public static Shader BatchRendererShader()
        {
            switch(Context.GetRenderAPI())
            {
                case RenderAPI.OPENGL: return Shader.CreateFromSource("BatchRenderer", batchRendererShaderGL);
                case RenderAPI.DIRECT3D: return Shader.CreateFromSource("BatchRenderer", batchRendererShaderD3D);
            }
            return null;
        }

        public static Shader SimpleShader()
        {
            return Shader.CreateFromSource("Simple Shader", simpleShader);
        }

        public static Shader BasicLightShader()
        {
            return Shader.CreateFromSource("Basic Light Shader", basicLightShader);
        }

        public static Shader GeometryPassShader()
        {
            return Shader.CreateFromSource("Geometry Pass Shader", geometryPassShader);
        }

        public static Shader DebugShader()
        {
            return Shader.CreateFromSource("Debug Shader", debugShader);
        }

    }
}
