namespace fsharper.typ.Pipe

open fsharper.op.Coerce
open fsharper.typ.Procedure

/// 泛用管道
type GenericPipe<'I, 'O>(fill: 'I -> 'O) as self =
    let mut = MutGenericPipe(fill = fill)

    new() = GenericPipe(coerce)

    member self.fill = mut.fill
    member self.import x = mut.import x
    member self.export x = mut.export x

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

    member self.asMut() = mut

type GenericPipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()

//type GenericPipe = GenericPipe<obj, obj>
type Pipe<'T> = GenericPipe<'T, 'T>
type Pipe = Pipe<obj>
