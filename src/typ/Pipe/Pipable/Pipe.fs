namespace fsharper.typ.Pipe.Pipable

open fsharper.typ.Pipe.GenericPipable
open fsharper.typ.Procedure

type Pipe<'T>(func) as self =

    new() = Pipe<'T>(id)

    member self.fill = func

    member self.import(p: Pipable<'T>) = Pipe<'T>(p.fill .> self.fill)

    member self.export(p: Pipable<'T>) : Pipe<'T> = downcast p.import self

    interface Pipable<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export self
        member i.export p = p.import i

type Pipe<'T> with

    //Semigroup
    member ma.mappend(mb: Pipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = Pipe<'T>()
