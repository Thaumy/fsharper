[<AutoOpen>]
module fsharper.op.Casting

open System

let inline cast (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)
