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
        obj_player.adir = dir_;
        obj_player.apit = pit_;
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
        
        //create the objects
        nm_ = buffer_read(dat_buff,buffer_s32);
        for(i = 0; i < nm_; i++)
        {
            var id_  = buffer_read(dat_buff,buffer_u32);
            var x_   = buffer_read(dat_buff,buffer_s32);
            var y_   = buffer_read(dat_buff,buffer_s32);
            var z_   = buffer_read(dat_buff,buffer_s32);
            var wd_  = buffer_read(dat_buff,buffer_f32);
            var ht_  = buffer_read(dat_buff,buffer_f32);
            var zh_  = buffer_read(dat_buff,buffer_s32);
            var tex_ = buffer_read(dat_buff,buffer_u16);
            var inst_ = instance_create(x_,y_,obj_box);
            inst_.z = z_;
            inst_.image_xscale = wd_;
            inst_.image_yscale = ht_;
            inst_.zheight      = zh_;
            inst_.tex          = tex_;
            inst_.obj_id       = id_;
        }
        
    case 5: //new object
        var id_  = buffer_read(dat_buff,buffer_u32);
        var x_   = buffer_read(dat_buff,buffer_s32);
        var y_   = buffer_read(dat_buff,buffer_s32);
        var z_   = buffer_read(dat_buff,buffer_s32);
        var wd_  = buffer_read(dat_buff,buffer_f32);
        var ht_  = buffer_read(dat_buff,buffer_f32);
        var zh_  = buffer_read(dat_buff,buffer_s32);
        var tex_ = buffer_read(dat_buff,buffer_u16);
        var inst_ = instance_create(x_,y_,obj_box);
        inst_.z = z_;
        inst_.image_xscale = wd_;
        inst_.image_yscale = ht_;
        inst_.zheight      = zh_;
        inst_.tex          = tex_;
        inst_.obj_id       = id_;
        break;
    case 6: //destroy object
        tempArray[0] = buffer_read(dat_buff,buffer_u32);
        with(obj_box)
        {
            if(obj_id == other.tempArray[0])
            {
                instance_destroy();
            }
        }
        break;
    default:
        break;
}
