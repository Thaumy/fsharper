namespace fsharper.typ.Pipe

open fsharper.typ
open fsharper.op.Coerce

/// 可变泛用缓存管道
type MutGenericCachePipe<'I, 'O>() as self =

    member val cache: 'I -> Option'<'O> = always None with get, set
    member val data: 'I -> 'O = coerce with get, set

    member self.fill input =
        match self.cache input with
        | Some output -> output
        | _ -> self.data input

    member self.import(igp: IGenericPipe<'t, 'I>) : IGenericPipe<_, _> = igp.export self //default impl

    member self.export(igp: IGenericPipe<'O, 't>) : IGenericPipe<_, _> =
        MutGenericPipe(fill = (self.fill .> igp.fill))

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

type MutGenericCachePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: MutGenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = MutGenericPipe<'I, 'O>()

//type MutGenericCachePipe = MutGenericCachePipe<obj, obj>
type MutCachePipe<'T> = MutGenericCachePipe<'T, 'T>
type MutCachePipe = MutCachePipe<obj>
