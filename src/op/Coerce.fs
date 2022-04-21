[<AutoOpen>]
module fsharper.op.Coerce

open System

let inline is<'b> a = typeof<'b>.IsInstanceOfType a

//when 'a = 'b, coerce is eq to id
let inline coerce (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)

type Object with

    member self.is<'b>() = is<'b> self

    member self.coerce() = self |> coerce
