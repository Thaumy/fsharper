namespace fsharper.typ.Pipe.IPipe

open fsharper.typ.Pipe.IGenericPipe

type IPipe<'T> =
    inherit IGenericPipe<'T, 'T>
