///network_play_sound(x,y,z,snd_id,priority,loop?);
var _x, _y, _z, _id, _pr, _lp, nm_, _buff, tll_;
_x  = argument0;
_y  = argument1;
_z  = argument2;
_id = argument3;
_pr = argument4;
_lp = argument5;
_buff = buffer_create(18,buffer_fixed,1);
buffer_seek(_buff,buffer_seek_start,0);
buffer_write(_buff,buffer_u16 ,11 );
buffer_write(_buff,buffer_s32 ,_x );
buffer_write(_buff,buffer_s32 ,_y );
buffer_write(_buff,buffer_s32 ,_z );
buffer_write(_buff,buffer_s16 ,_id);
buffer_write(_buff,buffer_u8  ,_pr);
buffer_write(_buff,buffer_bool,_lp);
tll_ = buffer_tell(_buff);
global.array[0] = ds_list_create();
with(obj_playerobject)
{
    ds_list_add(global.array[0],socket);
}
nm_ = ds_list_size(global.array[0]);
show_debug_message("sending sound data");
for(i = 0; i < nm_; i += 1 )
{
    network_send_packet(ds_list_find_value(global.array[0],i),_buff,tll_);
}
ds_list_destroy(global.array[0]);
buffer_delete(_buff);
