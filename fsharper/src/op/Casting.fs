[<AutoOpen>]
module fsharper.op.Casting

open System


let inline is<'b> a = typeof<'b>.IsInstanceOfType a

let cast = fsharper.typeExt.Object.cast
