///bin_to_int(0,1..15);
var _final = 0;
for(i = 0; i < argument_count; i++ )
{
    _final = (_final<<1)+argument[i];
}
return _final;
