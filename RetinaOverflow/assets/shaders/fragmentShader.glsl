#version 330 core

in vec2 v_texCoords;
in vec3 v_normal;

uniform sampler2D diffuseTexture;

out vec4 color;

void main()
{
	//color = vec4(1.0, 1.0, 1.0, 1.0);
    color = vec4( texture(diffuseTexture, v_texCoords).rgb, 1.0);
}