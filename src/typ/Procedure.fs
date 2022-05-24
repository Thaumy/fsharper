[<AutoOpen>]
module fsharper.typ.Procedure

[<AutoOpen>]
module ext =
    open System

    type Object with

        member self.``let`` f = f self

        member self.also f =
            self.``let`` f
            self

[<AutoOpen>]
module fn =

    let inline loop f =
        while true do
            f ()

    let inline flip f a b = f b a

    /// aka const
    let inline always x _ = x

    //aka function composition (>>)
    let inline (.>) a b =
        Microsoft.FSharp.Core.Operators.op_ComposeRight a b

    let inline (<.) a b = fun x -> a x b
