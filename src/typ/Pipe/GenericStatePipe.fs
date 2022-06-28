namespace fsharper.typ.Pipe

open fsharper.op.Coerce
open fsharper.typ.Procedure

/// 泛用状态管道
type GenericStatePipe<'I, 'O>(activate, activated) as self =
    let mut =
        MutGenericStatePipe(activate = activate, activated = activated)

    new() = GenericStatePipe(coerce, coerce)
    new(activate) = GenericStatePipe(activate, coerce)

    member self.fill = mut.fill
    member self.import x = mut.import x
    member self.export x = mut.export x

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import igp = self.import igp
        member i.export igp = igp.import self //default impl

    member self.asMut() = mut

type GenericStatePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericStatePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericStatePipe<'I, 'O>()

//type GenericStatePipe = GenericStatePipe<obj, obj>
type StatePipe<'T> = GenericStatePipe<'T, 'T>
type StatePipe = StatePipe<obj>
