[<AutoOpen>]
module fsharper.alias.uint

open System

type u8 = Byte
type u16 = UInt16
type u32 = UInt32
type u64 = UInt64

let inline u8 value = byte value
let inline u16 value = uint16 value
let inline u32 value = uint32 value
let inline u64 value = uint64 value
