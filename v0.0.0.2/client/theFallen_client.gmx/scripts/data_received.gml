///data_received(buffer,socket);
var _buff, _sock, _msgid;
_buff = argument0;
_sock = argument1;
/*var sz_ = buffer_get_size(_buff);
if(sz_ < 2)
{
    show_debug_message("size: " + string(sz_));
    exit;
}*/

_msgid = buffer_read(_buff,buffer_u16);
switch(_msgid)
{
    case 0: //update entity
        var _num;
        _num = buffer_read(_buff,buffer_s32);
        for(i = 0; i < _num; i += 1 )
        {
            var id_, x_, y_, z_, w_, h_, d_, p_, e_;
            id_ = buffer_read(_buff,buffer_s32);
            x_ = buffer_read(_buff,buffer_f64);
            y_ = buffer_read(_buff,buffer_f64);
            z_ = buffer_read(_buff,buffer_f64);
            w_ = buffer_read(_buff,buffer_f64);
            h_ = buffer_read(_buff,buffer_f64);
            d_ = buffer_read(_buff,buffer_f32);
            p_ = buffer_read(_buff,buffer_f32);
            e_ = get_entity(id_);
            e_.x = x_;
            e_.y = y_;
            e_.z = z_;
            e_.width = w_;
            e_.height = h_;
            e_.dir = d_;
            e_.pit = p_;
        }
        break;
    case 1: //create object
        var inst_, x_, y_, z_, w_, h_, l_, id_;
        id_ = buffer_read(_buff,buffer_u32);
        x_ = buffer_read(_buff,buffer_f64);
        y_ = buffer_read(_buff,buffer_f64);
        z_ = buffer_read(_buff,buffer_f64);
        w_ = buffer_read(_buff,buffer_f64); //x
        h_ = buffer_read(_buff,buffer_f64); //y
        l_ = buffer_read(_buff,buffer_f64); //z
        inst_ = instance_create(x_,y_,obj_object);
        inst_.objId = id_;
        inst_.z = z_;
        inst_.width = w_;
        inst_.height = h_;
        inst_.length = l_;
        break;
    case 2: //destroy object
        var id_ = buffer_read(_buff,buffer_u32);
        with(get_object(id_)) instance_destroy();
        break;
    case 3: //on player join
        obj_client.entId = buffer_read(_buff,buffer_s32);
        var num_,
        num_ = buffer_read(_buff,buffer_u32);
        for(i = 0; i < num_; i += 1 )
        {
            var id_, x_, y_, z_, w_, h_, d_, p_, inst_;
            id_ = buffer_read(_buff,buffer_s32);
            x_ = buffer_read(_buff,buffer_f64);
            y_ = buffer_read(_buff,buffer_f64);
            z_ = buffer_read(_buff,buffer_f64);
            w_ = buffer_read(_buff,buffer_f64);
            h_ = buffer_read(_buff,buffer_f64);
            d_ = buffer_read(_buff,buffer_f32);
            p_ = buffer_read(_buff,buffer_f32);
            inst_ = instance_create(x_,y_,obj_entity);
            inst_.entityId = id_;
            inst_.z = z_;
            inst_.width = w_;
            inst_.height = h_;
            inst_.dir = d_;
            inst_.pit = p_;
        }
        num_ = buffer_read(_buff,buffer_u32);
        for(i = 0; i < num_; i += 1 )
        {
            var id_ = buffer_read(_buff,buffer_s32),
                x_ = buffer_read(_buff,buffer_f64),
                y_ = buffer_read(_buff,buffer_f64),
                z_ = buffer_read(_buff,buffer_f64),
                w_ = buffer_read(_buff,buffer_f64),
                h_ = buffer_read(_buff,buffer_f64),
                l_ = buffer_read(_buff,buffer_f64),
                inst_ = instance_create(x_,y_,obj_object);
            inst_.entityId = id_;
            inst_.z = z_;
            inst_.width = w_;
            inst_.height = h_;
            inst_.length = l_;
        }
        break;
    case 4: //entity created
        var id_, x_, y_, z_, w_, h_, d_, p_, inst_;
        id_ = buffer_read(_buff,buffer_s32);
        x_ = buffer_read(_buff,buffer_f64);
        y_ = buffer_read(_buff,buffer_f64);
        z_ = buffer_read(_buff,buffer_f64);
        w_ = buffer_read(_buff,buffer_f64);
        h_ = buffer_read(_buff,buffer_f64);
        d_ = buffer_read(_buff,buffer_f32);
        p_ = buffer_read(_buff,buffer_f32);
        inst_ = instance_create(x_,y_,obj_entity);
        inst_.entityId = id_;
        inst_.z = z_;
        inst_.width = w_;
        inst_.height = h_;
        inst_.dir = d_;
        inst_.pit = p_;
        break;
    case 5: //entity destroyed
        var id_ = buffer_read(_buff,buffer_s32);
        var ent_ = get_entity(id_);
        with(ent_) instance_destroy();
        break;
    case 6: //pinged back
        ping = timeout-obj_client.alarm[0];
        obj_client.alarm[0] = 1;
        show_debug_message(ping);
        break;
}
