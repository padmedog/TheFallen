///translate_point_x(x,y,rotation);
var _x, _y, _rot, _dist, _dir, _result;
_x = argument0;
_y = argument1;
_rot = argument2;
_dist = point_distance(0,0,_x,_y);
_dir = point_direction(0,0,_x,_y) + _rot;
_x = lengthdir_x(_dist,_dir);
return _x;

