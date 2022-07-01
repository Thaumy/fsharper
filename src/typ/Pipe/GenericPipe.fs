namespace fsharper.typ.Pipe

open fsharper.op.Coerce
open fsharper.typ.Procedure

/// 泛用管道
type GenericPipe<'I, 'O>(fill: 'I -> 'O) as self =

    new() = GenericPipe coerce

    member self.fill = fill

    member self.import(igp: #IGenericPipe<'t, 'I>) : IGenericPipe<_, _> = GenericPipe(igp.fill .> self.fill)

    member self.export(igp: #IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type GenericPipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()

type Pipe<'T> = GenericPipe<'T, 'T>
