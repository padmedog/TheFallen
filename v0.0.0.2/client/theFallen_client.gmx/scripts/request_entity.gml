///request_entity(id);
var _id = argument0,
    _buff = buffer_create(8,buffer_fixed,1);
buffer_write(_buff,buffer_u16,4);
buffer_write(_buff,buffer_s32,_id);
network_send_packet(obj_client.socket,_buff,buffer_tell(_buff));
buffer_delete(_buff);
