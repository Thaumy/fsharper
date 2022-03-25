namespace fsharper.types.Pipe.Pipable

open fsharper.types.Pipe.GenericPipable

type Pipable<'T> =
    inherit GenericPipable<'T, 'T>
