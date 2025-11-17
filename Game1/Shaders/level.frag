#version 330 core
out vec4 outputColor;

in vec2 texCoord;
in float shaderdir;
uniform sampler2D textureAtlas;
uniform float tintEnable;
uniform vec4 tintColor;

void main() {
    
    vec4 texColor = (( 2 * (texture(textureAtlas, texCoord)) + (tintEnable * tintColor)) / (2+tintEnable)) * shaderdir;
    if (texColor.a < 0.1f) {
        discard;
    } else {
        outputColor = texColor;
    }
}