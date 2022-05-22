[<AutoOpen>]
module fsharper.typ.Result'

open System
open fsharper.op.Reflection
open fsharper.typ

type Result'<'a, 'e> =
    | Ok of 'a
    | Err of 'e

type Result'<'a, 'e> with
    //Functor
    member inline self.fmap f =
        match self with
        | Ok x -> f x |> Ok
        | Err e -> Err e

    //Applicative
    static member inline ap(ma: Result'<'x -> 'y, 'e0>, mb: Result'<'x, 'e0>) =
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
    static member inline wrap x = Result'<_, _>.``pure`` x

    member inline self.unwrap() =
        match self with
        | Ok x -> x
        | Err e -> e.ToString() |> Exception |> raise

    member inline self.unwrapOr f =
        match self with
        | Ok x -> x
        | _ -> f ()

    member inline self.unwrapOrPanic e = self.unwrapOr (fun () -> raise e)

    member inline self.whenCanUnwrap f =
        match self with
        | Ok x -> f x
        | _ -> ()

    member inline self.whenCanUnwrapOr(trueDo, falseDo) =
        match self with
        | Ok x -> trueDo x
        | Err e -> falseDo e

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

type Result' = Result'<obj, exn>
