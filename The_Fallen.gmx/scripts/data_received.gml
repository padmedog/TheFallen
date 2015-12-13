var dat_sock, dat_buff, dat_id;
dat_sock = argument0;
dat_buff = argument1;
dat_id   = buffer_read(dat_buff,buffer_u16);
switch(dat_id)
{
    case 0: //update player
        var dir_ = buffer_read(dat_buff,buffer_f32);
        var pit_ = buffer_read(dat_buff,buffer_f32);
        var x_   = buffer_read(dat_buff,buffer_f32);
        var y_   = buffer_read(dat_buff,buffer_f32);
        var z_   = buffer_read(dat_buff,buffer_f32);
        obj_player.dir = dir_;
        obj_player.pit = pit_;
        obj_player.x   = x_;
        obj_player.y   = y_;
        obj_player.z   = z_;
        break;
    case 1: //update playerobject
        var socket_ = buffer_read(dat_buff,buffer_s16);
        var dir_    = buffer_read(dat_buff,buffer_f32);
        var pit_    = buffer_read(dat_buff,buffer_f32);
        var x_      = buffer_read(dat_buff,buffer_f32);
        var y_      = buffer_read(dat_buff,buffer_f32);
        var z_      = buffer_read(dat_buff,buffer_f32);
        var plobj_  = get_playerobject(socket_);
        plobj_.dir = dir_;
        plobj_.pit = pit_;
        plobj_.x   = x_;
        plobj_.y   = y_;
        plobj_.z   = z_;
        break;
    case 2: //create playerobject
        var socket_ = buffer_read(dat_buff,buffer_s16);
        var dir_    = buffer_read(dat_buff,buffer_f32);
        var pit_    = buffer_read(dat_buff,buffer_f32);
        var x_      = buffer_read(dat_buff,buffer_f32);
        var y_      = buffer_read(dat_buff,buffer_f32);
        var z_      = buffer_read(dat_buff,buffer_f32);
        var inst_   = instance_create(x_,y_,obj_playerobject);
        inst_.z      = z_;
        inst_.socket = socket_;
        inst_.dir    = dir_;
        inst_.pit    = pit_;
        break;
    case 3: //destroy playerobject
        var socket_ = buffer_read(dat_buff,buffer_s16);
        var inst_   = get_playerobject(socket_);
        with(inst_) instance_destroy();
        break;
    case 4: //i'm created
        var dir_  = buffer_read(dat_buff,buffer_f32);
        var pit_  = buffer_read(dat_buff,buffer_f32);
        var x_    = buffer_read(dat_buff,buffer_f32);
        var y_    = buffer_read(dat_buff,buffer_f32);
        var z_    = buffer_read(dat_buff,buffer_f32);
        var inst_ = instance_create(x_,y_,obj_player);
        inst_.dir = dir_;
        inst_.pit = pit_;
        inst_.z   = z_;
        
        //create the players
        var nm_ = buffer_read(dat_buff,buffer_u16);
        for(i = 0; i < nm_; i++)
        {
            var socket_  = buffer_read(dat_buff,buffer_s16);
            dir_         = buffer_read(dat_buff,buffer_f32);
            pit_         = buffer_read(dat_buff,buffer_f32);
            x_           = buffer_read(dat_buff,buffer_f32);
            y_           = buffer_read(dat_buff,buffer_f32);
            z_           = buffer_read(dat_buff,buffer_f32);
            inst_        = instance_create(x_,y_,obj_playerobject);
            inst_.socket = socket_;
            inst_.dir    = dir_;
            inst_.pit    = pit_;
            inst_.z      = z_;
        }
    default:
        break;
}
