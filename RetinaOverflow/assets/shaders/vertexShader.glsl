	#version 330 core
	layout(location = 0) in vec3 position;
	layout(location = 1) in vec3 normal;
	layout(location = 2) in vec2 texCoords;    
            
	uniform mat4 viewMatrix = mat4(0);
	uniform mat4 projectionMatrix = mat4(0);
	uniform mat4 modelMatrix = mat4(0);
    
	out vec2 v_texCoords;
	out vec3 v_normal;
	         
	void main(){
		gl_Position =  projectionMatrix * viewMatrix * modelMatrix * vec4(position, 1.0); 
		v_texCoords = texCoords;  
		v_normal = normal;  
	}   