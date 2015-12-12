var dat_sock, dat_buff, dat_id;
dat_sock = argument0;
dat_buff = argument1;
dat_id   = buffer_read(dat_buff,buffer_u16);
switch(dat_id)
{
    case 0: //client input
        var dir_ = buffer_read(dat_buff,buffer_s16);
        var pit_ = buffer_read(dat_buff,buffer_s16);
        var lr_  = buffer_read(dat_buff,buffer_u8 );
        var ud_  = buffer_read(dat_buff,buffer_u8 );
        var pl_  = get_playerobject(dat_sock);
        pl_.dir = dir_;
        pl_.pit = pit_;
        pl_.bxspeed = lengthdir_x(sign(lr_),pl_.dir)+lengthdir_x(sign(ud_),pl_.dir+90);
        pl_.byspeed = lengthdir_y(sign(lr_),pl_.dir)+lengthdir_y(sign(ud_),pl_.dir+90);
        break;
    default:
        break;
}
