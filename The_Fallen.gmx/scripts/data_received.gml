var dat_sock, dat_buff, dat_id;
dat_sock = argument0;
dat_buff = argument1;
dat_id   = buffer_read(dat_buff,buffer_u16);
dat_tell = buffer_tell(dat_buff);
//show_message_async(dat_tell);
switch(dat_id)
{
    case 0: //update player
        var dir_  = buffer_read(dat_buff,buffer_f32);
        var pit_  = buffer_read(dat_buff,buffer_f32);
        var x_    = buffer_read(dat_buff,buffer_f32);
        var y_    = buffer_read(dat_buff,buffer_f32);
        var z_    = buffer_read(dat_buff,buffer_f32);
        var item_ = buffer_read(dat_buff,buffer_u16);
        var hlth_ = buffer_read(dat_buff,buffer_s32);
        var engy_ = buffer_read(dat_buff,buffer_s32);
        var hngr_ = buffer_read(dat_buff,buffer_s32);
        var thst_ = buffer_read(dat_buff,buffer_s32);
        var temp_ = buffer_read(dat_buff,buffer_s32);
        obj_player.adir         = dir_;
        obj_player.apit         = pit_;
        obj_player.x            = x_;
        obj_player.y            = y_;
        obj_player.z            = z_;
        obj_player.current_item = item_;
        obj_player.hlth         = hlth_;
        obj_player.engy         = engy_;
        obj_player.hngr         = hngr_;
        obj_player.thst         = thst_;
        obj_player.temp         = temp_;
        break;
    case 1: //update playerobject
        var socket_ = buffer_read(dat_buff,buffer_s16);
        var dir_    = buffer_read(dat_buff,buffer_f32);
        var pit_    = buffer_read(dat_buff,buffer_f32);
        var x_      = buffer_read(dat_buff,buffer_f32);
        var y_      = buffer_read(dat_buff,buffer_f32);
        var z_      = buffer_read(dat_buff,buffer_f32);
        var item_   = buffer_read(dat_buff,buffer_u16);
        var plobj_  = get_playerobject(socket_);
        plobj_.dir          = dir_;
        plobj_.pit          = pit_;
        plobj_.x            = x_;
        plobj_.y            = y_;
        plobj_.z            = z_;
        plobj_.current_item = item_;
        break;
    case 2: //create playerobject
        var socket_ = buffer_read(dat_buff,buffer_s16);
        var dir_    = buffer_read(dat_buff,buffer_f32);
        var pit_    = buffer_read(dat_buff,buffer_f32);
        var x_      = buffer_read(dat_buff,buffer_f32);
        var y_      = buffer_read(dat_buff,buffer_f32);
        var z_      = buffer_read(dat_buff,buffer_f32);
        var item_   = buffer_read(dat_buff,buffer_u16);
        var inst_   = instance_create(x_,y_,obj_playerobject);
        inst_.z            = z_;
        inst_.socket       = socket_;
        inst_.dir          = dir_;
        inst_.pit          = pit_;
        inst_.current_item = item_;
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
        var item_ = buffer_read(dat_buff,buffer_u16);
        var hlth_ = buffer_read(dat_buff,buffer_s32);
        var engy_ = buffer_read(dat_buff,buffer_s32);
        var hngr_ = buffer_read(dat_buff,buffer_s32);
        var thst_ = buffer_read(dat_buff,buffer_s32);
        var temp_ = buffer_read(dat_buff,buffer_s32);
        var inst_ = instance_create(x_,y_,obj_player);
        inst_.dir          = dir_;
        inst_.pit          = pit_;
        inst_.z            = z_;
        inst_.current_item = item_;
        inst_.hlth         = hlth_;
        inst_.engy         = engy_;
        inst_.hngr         = hngr_;
        inst_.thst         = thst_;
        inst_.temp         = temp_;
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
            item_        = buffer_read(dat_buff,buffer_u16);
            hlth_        = buffer_read(dat_buff,buffer_s32);
            engy_        = buffer_read(dat_buff,buffer_s32);
            hngr_        = buffer_read(dat_buff,buffer_s32);
            thst_        = buffer_read(dat_buff,buffer_s32);
            temp_        = buffer_read(dat_buff,buffer_s32);
            inst_        = instance_create(x_,y_,obj_playerobject);
            inst_.socket       = socket_;
            inst_.dir          = dir_;
            inst_.pit          = pit_;
            inst_.z            = z_;
            inst_.current_item = item_;
            inst_.hlth         = hlth_;
            inst_.engy         = engy_;
            inst_.hngr         = hngr_;
            inst_.thst         = thst_;
            inst_.temp         = temp_;
        }
        
        //create the objects
        nm_ = buffer_read(dat_buff,buffer_u32);
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
            inst_.zheight      = zh_;
            inst_.image_yscale = ht_;
            inst_.tex          = tex_;
            inst_.obj_id       = id_;
        }
        //set the environment
        var e_ = buffer_read(dat_buff,buffer_bool);
        var c_ = buffer_read(dat_buff,buffer_s32 );
        var s_ = buffer_read(dat_buff,buffer_s32 );
        var d_ = buffer_read(dat_buff,buffer_s32 );
        obj_control.fog_enabled = e_;
        obj_control.fog_color   = c_;
        obj_control.fog_start   = s_;
        obj_control.fog_end     = d_;
        
        //create the item objects
        nm_ = buffer_read(dat_buff,buffer_u16);
        for(i = 0; i < nm_; i++)
        {
            var id_   = buffer_read(dat_buff,buffer_u16);
            x_        = buffer_read(dat_buff,buffer_s32);
            y_        = buffer_read(dat_buff,buffer_s32);
            z_        = buffer_read(dat_buff,buffer_s32);
            item_     = buffer_read(dat_buff,buffer_u16);
            var inst_ = instance_create(x_,y_,obj_item);
            inst_.z    = z_;
            inst_._id_ = id_;
            inst_.item = item_;
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
    case 7: //environment update
        var e_ = buffer_read(dat_buff,buffer_bool);
        var c_ = buffer_read(dat_buff,buffer_s32 );
        var s_ = buffer_read(dat_buff,buffer_s32 );
        var d_ = buffer_read(dat_buff,buffer_s32 );
        obj_control.fog_enabled = e_;
        obj_control.fog_color   = c_;
        obj_control.fog_start   = s_;
        obj_control.fog_end     = d_;
        break;
    case 8: //it be a ping
        
        break;
    case 9: //item object creation
        var id_   = buffer_read(dat_buff,buffer_u16);
        var x_    = buffer_read(dat_buff,buffer_s32);
        var y_    = buffer_read(dat_buff,buffer_s32);
        var z_    = buffer_read(dat_buff,buffer_s32);
        var item_ = buffer_read(dat_buff,buffer_u16);
        var inst_ = instance_create(x_,y_,obj_item);
        inst_.z    = z_;
        inst_._id_ = id_;
        inst_.item = item_;
        break;
    case 10: //item object delete
        tempArray[0] = buffer_read(dat_buff,buffer_u16);
        with(obj_item)
        {
            if(_id_ == other.tempArray[0])
            {
                instance_destroy();
            }
        }
        break;
    case 11: //sound create
        var x_  = buffer_read(dat_buff,buffer_s32 );
        var y_  = buffer_read(dat_buff,buffer_s32 );
        var z_  = buffer_read(dat_buff,buffer_s32 );
        var id_ = buffer_read(dat_buff,buffer_s16 );
        var pr_ = buffer_read(dat_buff,buffer_u8  );
        var lp_ = buffer_read(dat_buff,buffer_bool);
        var inst_ = instance_create(x_,y_,obj_soundemitter);
        inst_.z          = z_;
        inst_.sound_id   = id_;
        inst_._priority_ = pr_;
        inst_._loop_     = lp_;
        break;
    case 12: //emitter create
        var type_   = buffer_read(dat_buff,buffer_u16);
        var x_      = buffer_read(dat_buff,buffer_s32);
        var y_      = buffer_read(dat_buff,buffer_s32);
        var z_      = buffer_read(dat_buff,buffer_s32);
        var decay_  = buffer_read(dat_buff,buffer_u32);
        var width_  = buffer_read(dat_buff,buffer_s32);
        var height_ = buffer_read(dat_buff,buffer_s32);
        var length_ = buffer_read(dat_buff,buffer_s32);
        var dir_    = buffer_read(dat_buff,buffer_f32);
        var pit_    = buffer_read(dat_buff,buffer_f32);
        var inst_ = instance_create(x_,y_,obj_emitter);
        inst_.type         = type_
        inst_.z            = z_;
        inst_.alarm[0]     = decay_;
        inst_.image_xscale = width_;
        inst_.image_yscale = height_;
        break;
    default:
        break;
}
