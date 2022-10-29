[<AutoOpen>]
module fsharper.typ.Proc

open System
open System.Runtime.CompilerServices

[<AutoOpen>]
module fn =

    let inline loop f =
        while true do
            f ()

    let inline flip f a b = f b a

    /// aka const
    let inline always x _ = x

    let inline apply x f = f x

    let inline effect f x =
        f x |> ignore
        x

    //aka function composition (>>)
    let inline (.>) a b =
        Microsoft.FSharp.Core.Operators.op_ComposeRight a b

    let inline (..>) a b = fun x -> a x .> b

    //aka function composition (<<)
    let inline (<.) a b = b .> a

    let inline (<..) a b = b ..> a

    [<Extension>]
    type ext() =
        [<Extension>]
        static member inline effect(x, f) = x |> effect f

        [<Extension>]
        static member inline apply(x, f) = f x
