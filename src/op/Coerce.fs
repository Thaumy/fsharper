[<AutoOpen>]
module fsharper.op.Coerce

open System


let inline is<'b> a = typeof<'b>.IsInstanceOfType a

let inline coerce (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)

