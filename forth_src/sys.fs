( Calls Basic/Kernal routines.
  Uses ar/xr/yr/sr for register I/O. )

native-c128? [if] 
$06 value ar $07 value xr 
$08 value yr $05 value sr

\ because JSRFAR always returns with 
\ BASIC + Kernal ROMs banked in, SYSFAR
\ must be located below the start of 
\ BASIC ROM ($4000) 
here $3fe0 > [if]
cr .( SYSFAR too high: ) here u. cr
[else]
\ 'far' SYS that calls 128 kernal JSRFAR
code sysfar ( addr bank -- )
\ JSRFAR uses $02-$05 ... better hope
\ we aren't that deep in the stack ...

lsb lda,x $02 sta, inx, \ set bank
\ set jump destination
\ yes it is supposed to be MSB-first
msb lda,x $03 sta, lsb lda,x $04 sta,
\ save X and MMUCR
\ JSRFAR does not save MMUCR and always 
\ returns in bank 15
txa, pha, $ff00 lda, pha,
$ff6e jsr, \ JSRFAR
pla, $ff00 sta, pla, tax, inx,
;code

\ default sys for calling ROM routines
: sys ( addr -- )
\ bank $f = RAM0 + kernal + BASIC
$f sysfar ;
[then]

[else]
$30c value ar $30d value xr
$30e value yr $30f value sr
code sys ( addr -- )
lsb lda,x $14 sta, msb lda,x $15 sta,
txa, pha,
$e130 jsr, \ perform [sys]
pla, tax, inx, ;code
[then]