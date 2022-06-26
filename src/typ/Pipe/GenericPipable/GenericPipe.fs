namespace fsharper.typ.Pipe.GenericPipable

open fsharper.op.Coerce
open fsharper.typ.Pipe.GenericPipable
open fsharper.typ.Procedure

type GenericPipe<'I, 'O>(func: 'I -> 'O) as self =

    new() = GenericPipe<'I, 'O>(coerce)

    member self.fill = func

    member self.import(gp: GenericPipable<'t, 'I>) = GenericPipe<'t, 'O>(gp.fill .> func)

    member self.export(gp: GenericPipable<'O, 't>) : GenericPipe<'I, 't> = downcast gp.import self

    interface GenericPipable<'I, 'O> with
        member i.fill input = self.fill input
        member i.import gp = self.import gp
        member i.export gp = self.export gp

type GenericPipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()
