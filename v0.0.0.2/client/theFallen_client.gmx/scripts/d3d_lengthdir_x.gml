///d3d_lengthdir_x(length,direction,pitch);
var dir = degtorad(argument1),
	pit = degtorad(argument2),
	length = argument0;
return cos(dir)*cos(pit)*length;

