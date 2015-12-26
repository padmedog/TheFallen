var prec_ = argument0;
var swd_ = sprite_width/2;
var sht_ = sprite_height/2;
while(box_in_col(x-swd_+xspeed,y-sht_,z,x+swd_+xspeed,y+sht_,z+zheight))
{
    if(xspeed*sign(xspeed) >= 1)
    {
        xspeed -= sign(xspeed)/prec_;
    }
    else
    {
        xspeed = 0;
        break;
    }
        
}
while(box_in_col(x-swd_,y-sht_+yspeed,z,x+swd_,y+sht_+yspeed,z+zheight))
{
    if(yspeed*sign(yspeed) >= 1)
    {
        yspeed -= sign(yspeed)/prec_;
    }
    else
    {
        yspeed = 0;
        break;
    }
}
while(box_in_col(x-swd_,y-sht_,z+zspeed,x+swd_,y+sht_,z+zheight+zspeed))
{
    if(zspeed*sign(zspeed) >= 1)
    {
        zspeed -= sign(zspeed)/prec_;
    }
    else
    {
        zspeed = 0;
        break;
    }
}
x += xspeed;
y += yspeed;
z += zspeed;
