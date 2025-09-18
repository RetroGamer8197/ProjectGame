#version 330 core
out vec4 outputColor;

in vec2 texCoord;
in float shaderdir;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D textureAtlas;
uniform float tintEnable;
uniform vec4 tintColor;
//in vec4 vertexColor;
void main() {
    
    vec4 texColor = (( 2 * (texture(textureAtlas, texCoord)) + (tintEnable * tintColor)) / (2+tintEnable)) * shaderdir;//mix(texture(texture1, texCoord), texture(texture2, texCoord), 0.4);
    if (texColor.a < 0.1f) {
        discard;
    } else {
        outputColor = texColor;
    }
}