//room_goto_fade(room,speed);
var _rm, _spd, _inst;
_rm = argument0;
_spd = argument1;
if(instance_exists(obj_fadeto_)) return -1;
_inst = instance_create(0,0,obj_fadeto_);
_inst.rm  = _rm;
_inst.spd = _spd;

