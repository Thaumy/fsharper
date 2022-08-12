[<AutoOpen>]
module fsharper.alias.float

open System

type f32 = Single
type f64 = Double
type f128 = Decimal

let inline f32 value = float32 value
let inline f64 value = double value
let inline f128 value = decimal value
