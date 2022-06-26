namespace fsharper.typ.Pipe.Pipable

open fsharper.typ.Pipe.GenericPipable
open fsharper.typ.Procedure

type Pipe<'T> internal (beforeInvoked: 'T -> 'T) as self =

    member val func: 'T -> 'T = id with get, set

    new() = Pipe(id)

    member self.fill = beforeInvoked .> self.func

    member self.import(p: Pipable<'T>) = Pipe<'T>(p.fill, func = self.func)

    member self.export(p: Pipable<'T>) = p.import self

    interface Pipable<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export self
        member i.export p = p.import i

type Pipe<'T> with

    //Semigroup
    member ma.mappend(mb: Pipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = Pipe<'T>()
