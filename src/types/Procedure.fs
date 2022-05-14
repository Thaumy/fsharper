[<AutoOpen>]
module fsharper.types.Procedure

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

    let inline flip f a b = f b a

    /// aka const
    let inline konst x _ = x

    let inline (|>) a b = a |> b

    //aka function composition (>>)
    let inline (.|>) a b =
        Microsoft.FSharp.Core.Operators.op_ComposeRight a b
