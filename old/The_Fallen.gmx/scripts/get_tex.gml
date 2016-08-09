//get_tex(servertex id);
var _id, _value;
_id    = argument0;
_value = -1;
switch(_id)
{
  /*  case -1:
        
        break;  */
    case 0:
        return global.texture_whitefloor;
        break;
    case 1:
        return global.texture_grass;
        break;
}
return -1;
