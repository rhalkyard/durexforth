\ Immediate versions of if/else/then to
\ allow conditional compilation.
\ Supports nesting.

header [if] ( -- ) immediate

: [then] ( -- ) ; immediate 

: [else] ( -- )
\ consume and discard following words
\ until a matching [else] or [then] is
\ encountered
1 begin
    begin parse-name dup while
        find-name case
        [ parse-name [if] find-name ] 
        literal of 
            1+
        endof
        [ latest ] literal of
            1-
            dup if 1+ then
        endof
        [ parse-name [then] find-name ] 
        literal of
            1- 
        endof
        endcase
        ?dup 0= if exit then
    repeat
    source nip drop 0= if refill then
    source nip drop 0= until
drop
; immediate

define [if] ( flag -- )
0= if postpone [else] then
; \ immediate flag set in header
