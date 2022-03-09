[<AutoOpen>]
module fsharper.types.Endo


type Endo<'a> = Endo of appEndo: ('a -> 'a)

let appEndo (Endo appEndo) = appEndo
