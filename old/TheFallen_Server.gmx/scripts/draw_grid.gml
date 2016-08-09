///draw_grid(x,y,width,height,space x, space y);
var _x, _y, _w, _h, _xw, _yh;
_x = argument0;
_y = argument1;
_w = argument2+_x;
_h = argument3+_y;
_xw = argument4;
_yh = argument5;
for(i = _x; i < _w; i += _xw)
{
    draw_line(i,_y,i,_h);
}
for(i = _y; i < _h; i += _yh)
{
    draw_line(_x,i,_w,i);
}
