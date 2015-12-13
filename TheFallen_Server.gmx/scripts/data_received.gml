var dat_sock, dat_buff, dat_id;
dat_sock = argument0;
dat_buff = argument1;
dat_id   = buffer_read(dat_buff,buffer_u16);
switch(dat_id)
{
    case 0: //client input
        var dir_ =  buffer_read(dat_buff,buffer_f32 );
        var pit_ =  buffer_read(dat_buff,buffer_f32 );
        var lr_  = -buffer_read(dat_buff,buffer_s8  );
        var ud_  = -buffer_read(dat_buff,buffer_s8  );
        var ju_  =  buffer_read(dat_buff,buffer_bool);
        var pl_  =  get_playerobject(dat_sock);
        pl_.dir = dir_;
        pl_.pit = pit_;
        pl_.bxspeed = lengthdir_x(sign(lr_),pl_.dir+90)+lengthdir_x(sign(ud_),pl_.dir);
        pl_.byspeed = lengthdir_y(sign(lr_),pl_.dir+90)+lengthdir_y(sign(ud_),pl_.dir);
        if(ju_)
        {
            var wd_ = pl_.sprite_width/2;
            var ht_ = pl_.sprite_height/2;
            if(box_in_col(pl_.x-wd_,pl_.y-ht_,pl_.z-1,pl_.x+wd_,pl_.y+ht_,pl_.z+pl_.zheight-1))
            {
                pl_.zspeed += 8;
            }
        }
        break;
    default:
        break;
}
