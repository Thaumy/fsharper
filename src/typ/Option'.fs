[<AutoOpen>]
module fsharper.typ.Option'

open fsharper.op.Reflect

//TODO 此实现受限于RFC FS-1043

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

    static member inline unit x = Option'<_>.pure x

type Option'<'a> with
    //Boxing
    static member inline wrap x = Option'<_>.pure x

    member inline self.unwrap() =
        match self with
        | Some x -> x
        | _ -> failwith "Try to unwrap None"

    member inline self.unwrapOr f =
        match self with
        | Some x -> x
        | _ -> f ()

    member inline self.unwrapOrPanic e = self.unwrapOr (fun () -> raise e)

    member inline self.ifCanUnwrap f =
        match self with
        | Some x -> f x
        | _ -> ()

    member inline self.ifCanUnwrapOr(trueDo, falseDo) =
        match self with
        | Some x -> trueDo x
        | _ -> falseDo ()

    static member inline fromNullable x =
        match x with
        | null -> None
        | _ -> Some x

    static member inline fromCommaOk x =
        match x with
        | v, true -> Some v
        | _ -> None

    static member inline fromOkComma x =
        match x with
        | true, v -> Some v
        | _ -> None

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

type Option' = Option'<obj>

open System.Runtime.CompilerServices

[<Extension>]
type ext =
    [<Extension>]
    static member inline intoOption'((ok, v)) = Option'.fromOkComma (ok, v)

    [<Extension>]
    static member inline intoOption'((v, ok)) = Option'.fromCommaOk (v, ok)

open System.Collections.Generic

type IEnumerator<'T> with
    member self.find p =
        let rec loop _ =
            if self.MoveNext() then
                if p self.Current then
                    Some self.Current
                else
                    loop ()
            else
                None

        loop ()

type IEnumerable<'T> with
    member inline self.find p = self.GetEnumerator().find p
