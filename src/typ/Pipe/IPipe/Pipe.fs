namespace fsharper.typ.Pipe.IPipe

open fsharper.typ.Pipe.IGenericPipe
open fsharper.typ.Procedure

type Pipe<'T>(func) as self =

    new() = Pipe<'T>(id)

    member self.fill = func

    member self.import(p: IPipe<'T>) = Pipe<'T>(p.fill .> self.fill)

    member self.export(p: IPipe<'T>) : Pipe<'T> = downcast p.import self

    interface IPipe<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export self
        member i.export p = p.import i

type Pipe<'T> with

    //Semigroup
    member ma.mappend(mb: Pipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = Pipe<'T>()
