#version 140

in vec2 texCoord;
flat in float Texture;

uniform sampler2D texture0;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;
uniform sampler2D texture5;
uniform sampler2D texture6;
uniform sampler2D texture7;
uniform sampler2D texture8;
uniform sampler2D texture9;
uniform sampler2D texture10;
uniform sampler2D texture11;
uniform sampler2D texture12;
uniform sampler2D texture13;

out vec4 FragColor;

void main()
{
    FragColor = vec4(texCoord.x, texCoord.y, texCoord.y, Texture/10);
    
    if (Texture == 0.0)
    {
        FragColor = texture(texture0, texCoord);
    }
    else if (Texture == 1.0)
    {
        FragColor = texture(texture1, texCoord);
    }
    else if (Texture == 2.0)
    {
        FragColor = texture(texture2, texCoord);
    }
    else if (Texture == 3.0)
    {
        FragColor = texture(texture3, texCoord);
    }
    else if (Texture == 4.0)
    {
        FragColor = texture(texture4, texCoord);
    }
    else if (Texture == 5.0)
    {
        FragColor = texture(texture5, texCoord);
    }
    else if (Texture == 6.0)
    {
        FragColor = texture(texture6, texCoord);
    }
    else if (Texture == 7.0)
    {
        FragColor = texture(texture7, texCoord);
    }
    else if (Texture == 8.0)
    {
        FragColor = texture(texture8, texCoord);
    }
    else if (Texture == 9.0)
    {
        FragColor = texture(texture9, texCoord);
    }
    else if (Texture == 10.0)
    {
        FragColor = texture(texture10, texCoord);
    }
    else if (Texture == 11.0)
    {
        FragColor = texture(texture11, texCoord);
    }
    else if (Texture == 12.0)
    {
        FragColor = texture(texture12, texCoord);
    }
    else if (Texture == 13.0)
    {
        FragColor = texture(texture13, texCoord);
    }
}