namespace fsharper.typ.Pipe.Pipable

open fsharper.typ
open fsharper.typ.Pipe.Pipable

type CachePipe<'T>(func: 'T -> Option'<'T>) as self =

    member val dataPipe = Pipe<'T>() with get, set

    new() = CachePipe(fun _ -> None)

    member self.fill input =
        match func input with
        | Some output -> output
        | _ -> self.dataPipe.fill input

    member self.import(p: Pipable<'T>) = CachePipe<'T>(p.fill .> func)

    member self.export(p: Pipable<'T>) : CachePipe<'T> = downcast p.import self

    interface Pipable<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export self
        member i.export p = p.import self

type CachePipe<'T> with

    //Semigroup
    member ma.mappend(mb: Pipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = Pipe<'T>()
