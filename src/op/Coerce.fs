[<AutoOpen>]
module fsharper.op.Coerce

open System


let inline is<'b> a = typeof<'b>.IsInstanceOfType a

//when 'a = 'b, coerce is eq to id
let inline coerce (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)
