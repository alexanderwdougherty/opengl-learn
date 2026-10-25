// shader.fs

# version 330 core

out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

uniform sampler2D texture1;

uniform vec3 objectColor;
uniform vec3 lightColor;

void main()
{
    FragColor = vec4(texture(texture1, TexCoord).rgb * objectColor * lightColor, 1.0f);
}