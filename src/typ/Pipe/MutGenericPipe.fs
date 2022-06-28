namespace fsharper.typ.Pipe

open fsharper.op.Coerce
open fsharper.typ.Procedure

/// 可变泛用管道
type MutGenericPipe<'I, 'O>() as self =

    member val fill: 'I -> 'O = coerce with get, set

    member self.import(igp: #IGenericPipe<'t, 'I>) : IGenericPipe<_, _> =
        MutGenericPipe(fill = (igp.fill .> self.fill))

    member self.export(igp: #IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type MutGenericPipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: MutGenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = MutGenericPipe<'I, 'O>()

type MutPipe<'T> = MutGenericPipe<'T, 'T>
