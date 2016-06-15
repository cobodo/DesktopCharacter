#version 430

uniform float Width;
uniform float Height;

layout (binding=0, rgba8) uniform readonly image2D srcTex;

layout (std430, binding=1) buffer Buffer
{
	uint color[];
} outBuffer;

void swap(inout float a, inout float b) {
    float tmp = a;
    a = b;
    b = tmp;
}

layout (local_size_x = 16, local_size_y = 16) in;
void main() {
	uvec2 screenSize = uvec2( Width, Height );
    uvec2 pos = gl_GlobalInvocationID.xy;
	vec4 param = imageLoad( srcTex, ivec2( screenSize.x - pos.x, screenSize.y - pos.y ) );
	swap( param.r, param.b );
    uint index = gl_GlobalInvocationID.y * screenSize.x + gl_GlobalInvocationID.x;
	outBuffer.color[ index ] = packUnorm4x8( param );
}
