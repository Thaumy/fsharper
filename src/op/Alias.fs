namespace fsharper.op.Alias

open System

[<AutoOpen>]
module uint =
    type u8 = Byte
    type u16 = UInt16
    type u32 = UInt32
    type u64 = UInt64
    let inline u8 value = byte value
    let inline u16 value = uint16 value
    let inline u32 value = uint32 value
    let inline u64 value = uint64 value

[<AutoOpen>]
module int =
    type i8 = SByte
    type i16 = Int16
    type i32 = Int32
    type i64 = Int64
    let inline i8 value = sbyte value
    let inline i16 value = int16 value
    let inline i32 value = int32 value
    let inline i64 value = int64 value

[<AutoOpen>]
module float =
    type f32 = Single
    type f64 = Double
    type f128 = Decimal
    let inline f32 value = float32 value
    let inline f64 value = double value
    let inline f128 value = decimal value
