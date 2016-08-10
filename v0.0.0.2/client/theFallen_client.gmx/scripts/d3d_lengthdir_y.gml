///d3d_lengthdir_y(length,direction,pitch);
var dir = degtorad(argument1),
	pit = degtorad(argument2),
	length = argument0;
return sin(dir)*cos(pit)*length;

