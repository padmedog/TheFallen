/* menu_secondary(id);
0 = start menu
1 = quit menu
2 = option menu //incomplete
*/
var _id;
_id = argument0;
with(par_secondarymenu) instance_destroy();
switch(_id)
{
    case 0: //start
        instance_create(1040,16,obj_ipbox);
        instance_create(1040,48,obj_portbox);
        instance_create(1040,80,obj_goback);
        instance_create(1168,80,obj_connectbutton);
        break;
    case 1: //quit
        instance_create(1472,464,obj_gobackmenu);
        instance_create(1472,528,obj_leavegame);
        break;
    case 2: //options
        //insert option creation here
        break;
    default:
        break;
}
