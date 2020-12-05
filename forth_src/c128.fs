\ Useful words for the Commodore 128

: fast ( - ) \ Put C128 into 2MHz mode
$d030 c@
%00000001 or
$d030 c! ;

: slow ( - ) \ Put C128 into 1MHz mode
$d030 c@
%11111110 and
$d030 c! ;

: fast? ( - is-fast ) \ get current processor speed
$d030 c@
%00000001 and ;
