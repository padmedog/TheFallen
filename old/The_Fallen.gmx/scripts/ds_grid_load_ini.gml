//ds_grid_load_ini(filename (needs .ini));
var _filename, _grid, _w, _h;
_filename = argument0;
ini_open(_filename);
_w        = ini_read_real("data","width",1);
_h        = ini_read_real("data","height",1);
_grid     = ds_grid_create(_w,_h);


for(i = 0; i < _w; i += 1 )
{
    for(j = 0; j < _h; j += 1 )
    {
        ds_grid_set(_grid,i,j,ini_read_real("grid",string(i) + "-" + string(j),0));
    }
}
ini_close();
