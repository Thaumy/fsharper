namespace fsharper.typ.Pipe.IGenericPipe

open fsharper.op.Coerce
open fsharper.typ.Pipe.IGenericPipe
open fsharper.typ.Procedure

type GenericStatePipe<'I, 'O>() as self =

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

    member self.import(gp: IGenericPipe<'t, 'I>) =
        GenericStatePipe<'t, 'O>(activate = (gp.fill .> self.activate), activated = (gp.fill .> self.activated))

    member self.export(gp: IGenericPipe<'O, 't>) : GenericStatePipe<'I, 't> = downcast gp.import self

    interface IGenericPipe<'I, 'O> with
        member i.fill input = self.fill input
        member i.import gp = self.import gp
        member i.export gp = self.export gp

type GenericStatePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericStatePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericStatePipe<'I, 'O>()
