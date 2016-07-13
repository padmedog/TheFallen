//ds_grid_save_ini(filename (needs .ini),ds_grid);
var _filename, _grid, _w, _h;
_filename = argument0;
_grid     = argument1;
_w        = ds_grid_width( _grid);
_h        = ds_grid_height(_grid);
ini_open(_filename);
ini_write_real("data","width",_w);
ini_write_real("data","height",_h);
for(i = 0; i < _w; i += 1 )
{
    for(j = 0; j < _h; j += 1 )
    {
        ini_write_real("grid",string(i) + "-" + string(j),ds_grid_get(_grid,i,j));
    }
}
return ini_close();
