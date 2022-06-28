namespace fsharper.typ.Pipe

open fsharper.typ
open fsharper.op.Coerce

/// 泛用缓存管道
type GenericCachePipe<'I, 'O>(cache, data) as self =
    let mut =
        MutGenericCachePipe(cache = cache, data = data)

    new() = GenericCachePipe(always None, coerce)
    new(cache) = GenericCachePipe(cache, coerce)
    new(data) = GenericCachePipe(coerce, data)

    member self.fill = mut.fill
    member self.import x = mut.import x
    member self.export x = mut.export x

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl
        
    member self.asMut()=mut

type GenericCachePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()

type CachePipe<'T> = GenericCachePipe<'T, 'T>
