[<AutoOpen>]
module fsharper.moreType.Endo


type Endo<'a> = Endo of appEndo: ('a -> 'a)

let appEndo (Endo appEndo) = appEndo
