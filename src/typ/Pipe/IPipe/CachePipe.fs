namespace fsharper.typ.Pipe.IPipe

open fsharper.typ
open fsharper.typ.Pipe.IPipe

type CachePipe<'T>(func: 'T -> Option'<'T>) as self =

    member val dataPipe = Pipe<'T>() with get, set

    new() = CachePipe(fun _ -> None)

    member self.fill input =
        match func input with
        | Some output -> output
        | _ -> self.dataPipe.fill input

    member self.import(p: IPipe<'T>) = CachePipe<'T>(p.fill .> func)

    member self.export(p: IPipe<'T>) : CachePipe<'T> = downcast p.import self

    interface IPipe<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export self
        member i.export p = p.import self

type CachePipe<'T> with

    //Semigroup
    member ma.mappend(mb: Pipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = Pipe<'T>()
