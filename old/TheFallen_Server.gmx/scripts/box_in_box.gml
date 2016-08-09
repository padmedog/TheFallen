///box_in_box(sx1, sy1, sz1, sx2, sy2, sz2, dx1, dy1, dz1, dx2, dy2, dz2);
var x11, y11, z11, x21, y21, z21, x12, y12, z12, x22, y22, z22;
x11 = argument0;
y11 = argument1;
z11 = argument2;
x21 = argument3;
y21 = argument4;
z21 = argument5;
x12 = argument6;
y12 = argument7;
z12 = argument8;
x22 = argument9;
y22 = argument10;
z22 = argument11;
if(rectangle_in_rectangle(x11,y11,x21,y21,x12,y12,x22,y22))
{
    if(rectangle_in_rectangle(x11,z11,x21,z21,x12,z12,x22,z22))
    {
        if(rectangle_in_rectangle(y11,z11,y21,z21,y12,z12,y22,z22))
        {
            return true;
        }
    }
}
return false;
