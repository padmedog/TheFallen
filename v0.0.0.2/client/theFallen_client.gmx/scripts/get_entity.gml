///get_entity(id);
global.tempArray[0] = argument0;
global.tempArray[1] = noone;
with(obj_entity)
{
    if(entityId == global.tempArray[0])
        global.tempArray[1] = id;
}
return global.tempArray[1];
