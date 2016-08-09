///network_create_emitter(tex,x,y,z,width,height,decay);
var _x, _y, _z, _t, _w, _h, _d, nm_, _buff, tll_, l_;
_t = argument0;
_x = argument1;
_y = argument2;
_z = argument3;
_w = argument4;
_h = argument5;
_d = argument6;
_buff = buffer_create(28,buffer_fixed,2);
buffer_seek(_buff,buffer_seek_start,0);
buffer_write(_buff,buffer_u16,12);
buffer_write(_buff,buffer_u16,_t);
buffer_write(_buff,buffer_s32,_x);
buffer_write(_buff,buffer_s32,_y);
buffer_write(_buff,buffer_s32,_z);
buffer_write(_buff,buffer_s32,_w);
buffer_write(_buff,buffer_s32,_h);
buffer_write(_buff,buffer_s32,_d);
tll_ = buffer_tell(_buff);
global.array[0] = ds_list_create();
with(obj_playerobject)
{
    ds_list_add(global.array[0],socket);
}
nm_ = ds_list_size(global.array[0]);
show_debug_message("sending emitter data");
for(i = 0; i < nm_; i += 1 )
{
    network_send_packet(ds_list_find_value(global.array[0],i),_buff,tll_);
}
ds_list_destroy(global.array[0]);
buffer_delete(_buff);
