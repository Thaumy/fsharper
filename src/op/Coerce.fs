[<AutoOpen>]
module fsharper.op.Coerce

open System

let is<'b> a = typeof<'b>.IsInstanceOfType a

//when 'a = 'b, coerce is eq to id
let inline coerce (a: 'a) : 'b =
    downcast Convert.ChangeType(a, typeof<'b>)

let inline cast (a: 'a) : 'b = downcast (a :> obj)

type Object with

    member self.is<'b>() = is<'b> self
    member self.coerce() = self |> coerce
    member self.cast() = self |> cast
    member self.obj() = self
