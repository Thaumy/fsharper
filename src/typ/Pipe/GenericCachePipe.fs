namespace fsharper.typ.Pipe

open fsharper.op
open fsharper.typ

/// 泛用缓存管道
type GenericCachePipe<'I, 'O>(cache, data) as self =

    new() = GenericCachePipe(always None, coerce)
    new(cache) = GenericCachePipe(cache, coerce)
    new(data) = GenericCachePipe(always None, data)

    member self.fill input =
        match cache input with
        | Some output -> output
        | _ -> data input

    member self.import(igp: IGenericPipe<'t, 'I>) : IGenericPipe<_, _> = GenericPipe(igp.fill .> self.fill)

    member self.export(igp: IGenericPipe<'O, 't>) : IGenericPipe<_, _> = igp.import self //default impl

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type GenericCachePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()

type CachePipe<'T> = GenericCachePipe<'T, 'T>
