namespace fsharper.typ.Pipe.Pipable

open fsharper.typ.Pipe.GenericPipable

type Pipable<'T> =
    inherit GenericPipable<'T, 'T>
