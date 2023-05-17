[<AutoOpen>]
module fsharper.typ.Result'

open System
open fsharper.typ
open fsharper.op.Reflect

//TODO 此实现受限于RFC FS-1043

type Result'<'a, 'e> =
    | Ok of 'a
    | Err of 'e
    
type Result'<'t> = Result'<'t, exn>

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

    static member inline unit x = Result'<_, _>.pure x

type Result'<'a, 'e> with
    //Boxing
    static member inline wrap x = Result'<_, _>.pure x

    member inline self.unwrap() =
        match self with
        | Ok x -> x
        | Err e -> e.ToString() |> Exception |> raise

    member inline self.unwrapOr v =
        match self with
        | Ok x -> x
        | _ -> v

    member inline self.unwrapOrEval f =
        match self with
        | Ok x -> x
        | Err e -> f e

    member inline self.unwrapOrPanic e = self.unwrapOrEval (fun _ -> raise e)

    member inline self.orPure f =
        match self with
        | Err e -> Ok(f e)
        | _ -> self

    member inline self.orBind f =
        match self with
        | Err e -> f e
        | _ -> self

    member inline self.ifCanUnwrapOr(trueDo, falseDo) =
        match self with
        | Ok x -> trueDo x
        | Err e -> falseDo e

    static member inline fromNullable x =
        if (x :> obj = null) then
            NullReferenceException() :> exn |> Err
        else
            Ok x

    static member inline fromThrowable f =
        try
            f () |> Result'<_, _>.fromNullable
        with
        | e -> Err e

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
