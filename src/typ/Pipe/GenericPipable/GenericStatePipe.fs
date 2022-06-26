namespace fsharper.typ.Pipe.GenericPipable

open fsharper.op.Coerce
open fsharper.typ.Procedure

type GenericStatePipe<'I, 'O>(activate: 'I -> 'O, activated: 'I -> 'O) as self =

    [<DefaultValue>]
    val mutable func: 'I -> 'O

    do
        self.func <-
            fun arg ->
                self.func <- self.activated

                arg |> self.activate

    member val activate = activate with get, set
    member val activated = activated with get, set

    member self.fill = self.func

    member self.import(gp: GenericPipable<'t, 'I>) =
        GenericStatePipe<'t, 'O>(gp.fill .> activate, gp.fill .> activated)

    member self.export(gp: GenericPipable<'O, 't>) = gp.import self

    interface GenericPipable<'I, 'O> with
        member i.fill input = self.fill input
        member i.import gp = self.import gp
        member i.export gp = self.export gp

type GenericStatePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericStatePipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() =
        GenericStatePipe<'I, 'O>(coerce, coerce)
