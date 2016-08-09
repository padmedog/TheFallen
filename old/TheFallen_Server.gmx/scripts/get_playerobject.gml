///get_playerobject(socket);
global.array[0] = argument0;
with(obj_playerobject)
{
    if(global.array[0] > 0)
        if(global.array[0] == socket)
        {
            global.array[0] = -id;
        }
}
return -global.array[0];
