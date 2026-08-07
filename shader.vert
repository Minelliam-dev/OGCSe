#version 140

in vec3 aPosition;
in vec2 aTexCoord;
in float aTexture;

out vec2 texCoord;
flat out float Texture;

void main()
{
    texCoord = aTexCoord;
    Texture = aTexture;

    gl_Position = vec4(aPosition, 1.0);
}