///box_in_col(x1, y1, z1, x2, y2, z2);
var x1, y1, z1, x2, y2, z2, _res;
x1 = argument0;
y1 = argument1;
z1 = argument2;
x2 = argument3;
y2 = argument4;
z2 = argument5;
tempArray[0] = false;
tempArray[1] = x1;
tempArray[2] = y1;
tempArray[3] = z1;
tempArray[4] = x2;
tempArray[5] = y2;
tempArray[6] = z2;
with(obj_boxcollider)
{
    if(box_in_box(other.tempArray[1],other.tempArray[2],other.tempArray[3]
        ,other.tempArray[4],other.tempArray[5],other.tempArray[6]
        ,x,y,z,x+sprite_width,y+sprite_height,z+zheight))
    {
        other.tempArray[0] = true;
    }
}
return tempArray[0];
