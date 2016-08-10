///bin_to_int(0,1...15);
var _final;
_final = 0;
for(i = 0; i < argument_count; i += 1 )
{
    _final += argument[i];
    _final = _final << 1;
}
return _final;
