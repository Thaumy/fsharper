namespace fsharper.typ.Pipe

open fsharper.op
open fsharper.typ

/// 泛用远程管道
type GenericRemotePipe<'I, 'O>(fill: ('I -> 'O) ref) as self =

    new() = GenericRemotePipe(ref coerce)

    member self.fill = fill.Value

    member self.import(igp: #IGenericPipe<'t, 'I>) : IGenericPipe<_, _> = GenericPipe(igp.fill .> self.fill)

    member self.export(igp: #IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type GenericRemotePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericRemotePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericRemotePipe<'I, 'O>()

type RemotePipe<'T> = GenericRemotePipe<'T, 'T>
