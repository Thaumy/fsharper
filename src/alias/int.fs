[<AutoOpen>]
module fsharper.alias.int

open System

type i8 = SByte
type i16 = Int16
type i32 = Int32
type i64 = Int64

let inline i8 value = sbyte value
let inline i16 value = int16 value
let inline i32 value = int32 value
let inline i64 value = int64 value
