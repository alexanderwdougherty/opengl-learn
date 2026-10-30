// shader.fs

# version 330 core

out vec4 FragColor;

in vec2 TexCoord;
in vec3 Normal;
in vec3 FragPos;

struct Material {
    vec3 ambient;
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform sampler2D texture1;
uniform vec3 viewPos;

uniform Material material;
uniform Light light;

void main()
{
    vec3 diffuseTexColor = vec3(texture(material.diffuse, TexCoord));
    vec3 specularTexColor = vec3(texture(material.specular, TexCoord));
    
    vec3 ambient = light.ambient * diffuseTexColor * material.ambient;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * diffuseTexColor;

    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * specularTexColor;

    vec3 lightResult = ambient + diffuse + specular;
    vec3 result = texture(texture1, TexCoord).rgb;

    FragColor = vec4(result * lightResult, 1.0f);
}