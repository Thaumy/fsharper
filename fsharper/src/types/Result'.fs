[<AutoOpen>]
module fsharper.types.Result'

open System
open fsharper.typeExt


type Result'<'a, 'e> =
    | Ok of 'a
    | Err of 'e

    //Functor
    member inline self.fmap f =
        match self with
        | Ok x -> f x |> Ok
        | Err e -> Err e

    //Applicative
    static member inline ap(ma: Result'<'a -> 'b, 'e>, mb: Result'<'a, 'e>) =
        match ma, mb with
        | Err e, _ -> Err e
        | Ok f, _ -> mb.fmap f

    static member inline ``pure`` x = Ok x

    //Monad
    member self.bind f =
        match self with
        | Err e -> Err e
        | Ok x -> f x

    static member inline unit x = Result'<_, _>.``pure`` x

type Result'<'a, 'e> with
    //Boxing
    static member inline warp x = Result'<_, _>.``pure`` x

    member inline self.unwarp() =
        match self with
        | Ok x -> x
        | Err e -> e.ToString() |> Exception |> raise

    member inline self.unwarpOr f =
        match self with
        | Ok x -> x
        | _ -> f ()

    member inline self.debug() =
        match self with
        | Ok x ->
            //下一级调试信息
            let msg: string =
                try
                    $"""({(x.tryInvoke "debug")})"""
                with
                | _ -> x.ToString()

            $"Ok {msg}"
        | Err e ->
            //下一级调试信息
            let msg: string =
                try
                    $"""({(e.tryInvoke "debug")})"""
                with
                | _ -> e.ToString()

            $"Err {msg}"
