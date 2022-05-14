[<AutoOpen>]
module fsharper.types.Option'

open fsharper.types.Object

/// 尝试拆箱None错误
exception TryToUnwrapNone


type Option'<'a> =
    | Some of 'a
    | None

type Option'<'a> with
    //Functor
    member inline self.fmap f =
        match self with
        | Some a -> f a |> Some
        | None -> None

    //Applicative
    static member inline ap(ma: Option'<'x -> 'y>, mb: Option'<'x>) =
        match ma, mb with
        | None, _ -> None
        | Some f, _ -> mb.fmap f

    static member inline ``pure`` x = Some x

    //Monad
    member inline self.bind f =
        match self with
        | None -> None
        | Some x -> f x

    static member inline unit x = Option'<_>.``pure`` x

type Option'<'a> with
    //Boxing
    static member inline wrap x = Option'<_>.``pure`` x

    member inline self.unwrap() =
        match self with
        | Some x -> x
        | _ -> raise TryToUnwrapNone

    member inline self.unwrapOr f =
        match self with
        | Some x -> x
        | _ -> f ()

    member inline self.whenCanUnwrap f =
        match self with
        | Some x -> f x
        | _ -> ()

    member inline self.debug() =
        match self with
        | Some x ->
            //下一级调试信息
            let msg: string =
                try
                    $"""({(x.tryInvoke "debug")})"""
                with
                | _ -> x.ToString()

            $"Some {msg}"
        | None -> "None"
