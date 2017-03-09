#version 330 core

in vec2 v_texCoords;
in vec3 v_normal;
in vec3 v_vertex;

uniform sampler2D diffuseTexture;

uniform struct Light {
   vec3 position;
   vec3 intensity;
} light;

uniform mat4 modelMatrix = mat4(0);

out vec4 color;

void main()
{
	//calculate normal in world coordinates
	mat3 normalMatrix = transpose(inverse(mat3(modelMatrix))); // TODO: Do this once on the cpu
	vec3 normal = normalize(normalMatrix * v_normal);

	
    //calculate the location of this fragment (pixel) in world coordinates
	vec3 fragPosition = vec3(modelMatrix * vec4(v_vertex, 1));

	//calculate the vector from this pixels surface to the light source
	vec3 surfaceToLight = light.position - fragPosition;

	//calculate the cosine of the angle of incidence
	float brightness = dot(normal, surfaceToLight) / (length(surfaceToLight) * length(normal));
	//brightness = clamp(brightness, 0, 1);

	vec4 surfaceColor = texture(diffuseTexture, v_texCoords);
	color = vec4(brightness * light.intensity * surfaceColor.rgb, surfaceColor.a);
    //olor = vec4(normal, 1.0);
	//color = vec4( texture(diffuseTexture, v_texCoords).rgb, 1.0);
}