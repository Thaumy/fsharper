[<AutoOpen>]
module fsharper.op.Casting

open System

let inline is<'b> a = typeof<'b>.IsInstanceOfType a

let inline cast (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)
