namespace fsharper.typ.Pipe.GenericPipable

open fsharper.op.Coerce
open fsharper.typ.Procedure

type GenericPipe<'I, 'O>(func: 'I -> 'O) as self =

    member val func = func with get, set

    member self.invoke = self.func

    member self.import(gp: GenericPipable<'t, 'I>) = GenericPipe<'t, 'O>(gp.invoke .> func)

    member self.export(gp: GenericPipable<'O, 't>) = gp.import self

    interface GenericPipable<'I, 'O> with
        member i.invoke input = self.invoke input
        member i.import gp = self.import gp
        member i.export gp = self.export gp

type GenericPipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>(coerce)
