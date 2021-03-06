namespace fsharper.typ.Pipe.GenericPipable

open fsharper.typ
open fsharper.typ.Pipe.GenericPipable

type GenericCachePipe<'I, 'O>(func: 'I -> Option'<'O>) as self =

    member val dataPipe = GenericPipe<'I, 'O>() with get, set

    new() = GenericCachePipe(fun _ -> None)

    member self.fill input =
        match func input with
        | Some output -> output
        | _ -> self.dataPipe.fill input

    member self.import(gp: GenericPipable<'t, 'I>) =
        GenericCachePipe<'t, 'O>(gp.fill .> func)

    member self.export(gp: GenericPipable<'O, 't>) : GenericCachePipe<'I, 't> = downcast gp.import self

    interface GenericPipable<'I, 'O> with
        member i.fill input = self.fill input
        member i.import gp = self.import gp
        member i.export gp = self.export gp

type GenericCachePipe<'I, 'O> with

    //Semigroup
    member ma.mappend(mb: GenericPipe<'O, 't>) = ma.export mb

    //Monoid
    static member mempty() = GenericPipe<'I, 'O>()
