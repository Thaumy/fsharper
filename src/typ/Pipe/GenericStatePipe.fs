namespace fsharper.typ.Pipe

open fsharper.op
open fsharper.typ

/// 泛用状态管道
type GenericStatePipe<'I, 'O>(activate: 'I -> 'O, activated: 'I -> 'O) as self =

    new() = GenericStatePipe(coerce, coerce)
    new(activate) = GenericStatePipe(activate, coerce)

    [<DefaultValue>]
    val mutable private func: 'I -> 'O

    do
        self.func <-
            fun arg ->
                self.func <- activated
                activate arg

    member self.fill = self.func

    member self.import(igp: IGenericPipe<'t, 'I>) : IGenericPipe<_, _> = GenericPipe(igp.fill .> self.fill)

    member self.export(igp: IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type GenericStatePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericStatePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericStatePipe<'I, 'O>()

type StatePipe<'T> = GenericStatePipe<'T, 'T>
