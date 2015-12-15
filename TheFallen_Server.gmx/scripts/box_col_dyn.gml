var prec_ = argument0;
var swd_ = sprite_width/2;
var sht_ = sprite_height/2;
while(box_in_col(x-swd_+xspeed,y-sht_,z,x+swd_+xspeed,y+sht_,z+zheight))
{
    xspeed -= sign(xspeed)/prec_;
}
while(box_in_col(x-swd_,y-sht_+yspeed,z,x+swd_,y+sht_+yspeed,z+zheight))
{
    yspeed -= sign(yspeed)/prec_;
}
while(box_in_col(x-swd_,y-sht_,z+zspeed,x+swd_,y+sht_,z+zheight+zspeed))
{
    zspeed -= sign(zspeed)/prec_;
}
x += xspeed;
y += yspeed;
z += zspeed;
