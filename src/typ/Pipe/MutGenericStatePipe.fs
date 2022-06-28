namespace fsharper.typ.Pipe

open fsharper.op.Coerce
open fsharper.typ.Procedure

/// 可变泛用状态管道
type MutGenericStatePipe<'I, 'O>() as self =

    member val activate = coerce with get, set
    member val activated = coerce with get, set

    [<DefaultValue>]
    val mutable private func: 'I -> 'O

    do
        self.func <-
            fun arg ->
                self.func <- self.activated
                self.activate arg

    member self.fill = self.func

    member self.import(igp: IGenericPipe<'t, 'I>) : IGenericPipe<_, _> =
        MutGenericStatePipe(activate = (igp.fill .> self.activate), activated = (igp.fill .> self.activated))

    member self.export(igp: IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type MutGenericStatePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: MutGenericStatePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = MutGenericStatePipe<'I, 'O>()

type MutStatePipe<'T> = MutGenericStatePipe<'T, 'T>
