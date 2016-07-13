///box_col_dyn(precision);
var prec_, swd_, sht_, ox_, oy_, oz_;
prec_ = 1/argument0;
swd_  = sprite_width/2;
sht_  = sprite_height/2;
ox_   = false;
oy_   = false;
oz_   = false;
while(box_in_col(x-swd_+xspeed,y-sht_,z,x+swd_+xspeed,y+sht_,z+zheight))
{
    
    if(xspeed*sign(xspeed) >= prec_)
    {
        xspeed -= sign(xspeed)*prec_;
    }
    else
    {
        xspeed = 0;
        break;
    }
    ox_ = true;
}
while(box_in_col(x-swd_,y-sht_+yspeed,z,x+swd_,y+sht_+yspeed,z+zheight))
{
    if(yspeed*sign(yspeed) >= prec_)
    {
        yspeed -= sign(yspeed)*prec_;
    }
    else
    {
        yspeed = 0;
        break;
    }
    oy_ = true;
}
while(box_in_col(x-swd_,y-sht_,z+zspeed,x+swd_,y+sht_,z+zheight+zspeed))
{
    if(zspeed*sign(zspeed) >= prec_)
    {
        zspeed -= sign(zspeed)*prec_;
    }
    else
    {
        zspeed = 0;
        break;
    }
    oz_ = true;
}
if(!ox_ && !oy_) //xy
{
    while(box_in_col(x-swd_+xspeed,y-sht_+yspeed,z,x+swd_+xspeed,y+sht_+yspeed,z+zheight))
    {
        if(xspeed*sign(xspeed) >= prec_)
        {
            xspeed -= sign(xspeed)*prec_;
        }
        else
        {
            xspeed = 0;
            break;
        }
        if(yspeed*sign(yspeed) >= prec_)
        {
            yspeed -= sign(yspeed)*prec_;
        }
        else
        {
            yspeed = 0;
            break;
        }
        ox_ = true;
        oy_ = true;
    }
}
if(!oy_ && !oz_) //yz
{
    while(box_in_col(x-swd_,y-sht_+yspeed,z+zspeed,x+swd_,y+sht_+yspeed,z+zheight+zspeed))
    {
        if(yspeed*sign(yspeed) >= prec_)
        {
            yspeed -= sign(yspeed)*prec_;
        }
        else
        {
            yspeed = 0;
            break;
        }
        if(zspeed*sign(zspeed) >= prec_)
        {
            zspeed -= sign(zspeed)*prec_;
        }
        else
        {
            zspeed = 0;
            break;
        }
        oy_ = true;
        oz_ = true;
    }
}
if(!ox_ && !oz_) //zx
{
    while(box_in_col(x-swd_+xspeed,y-sht_,z+zspeed,x+swd_+xspeed,y+sht_,z+zheight+zspeed))
    {
        if(xspeed*sign(xspeed) >= prec_)
        {
            xspeed -= sign(xspeed)*prec_;
        }
        else
        {
            xspeed = 0;
            break;
        }
        if(zspeed*sign(zspeed) >= prec_)
        {
            zspeed -= sign(zspeed)*prec_;
        }
        else
        {
            zspeed = 0;
            break;
        }
        ox_ = true;
        oz_ = true;
    }
}
if(!ox_ && !oy_ && !oz_)
{
    while(box_in_col(x-swd_+xspeed,y-sht_+yspeed,z+zspeed,x+swd_+xspeed,y+sht_+yspeed,z+zheight+zspeed))
    {
        if(xspeed*sign(xspeed) >= prec_)
        {
            xspeed -= sign(xspeed)*prec_;
        }
        else
        {
            xspeed = 0;
            break;
        }
        if(yspeed*sign(yspeed) >= prec_)
        {
            yspeed -= sign(yspeed)*prec_;
        }
        else
        {
            yspeed = 0;
            break;
        }
        if(zspeed*sign(zspeed) >= prec_)
        {
            zspeed -= sign(zspeed)*prec_;
        }
        else
        {
            zspeed = 0;
            break;
        }
        ox_ = true;
        oy_ = true;
        oz_ = true;
    }
}
x += xspeed;
y += yspeed;
z += zspeed;
