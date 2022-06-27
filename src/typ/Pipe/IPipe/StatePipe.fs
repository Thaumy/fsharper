namespace fsharper.typ.Pipe.IPipe

open fsharper.typ.Procedure

type StatePipe<'T>() as self =

    member val activate = id with get, set
    member val activated = id with get, set

    [<DefaultValue>]
    val mutable private func: 'T -> 'T

    do
        self.func <-
            fun arg ->
                self.func <- self.activated
                self.activate arg

    member self.fill = self.func

    member self.import(p: IPipe<'T>) =
        StatePipe<'T>(activate = (p.fill .> self.activate), activated = (p.fill .> self.activated))

    member self.export(p: IPipe<'T>) : StatePipe<'T> = downcast p.import self

    interface IPipe<'T> with
        member i.fill input = self.fill input
        member i.import p = p.export i
        member i.export p = p.import i

type StatePipe<'T> with

    //Semigroup
    member ma.mappend(mb: StatePipe<'T>) = ma.export mb

    //Monoid
    static member mempty() = StatePipe<'T>()
