[<AutoOpen>]
module fsharper.types.Endo


type Endo<'a> = Endo of appEndo: ('a -> 'a)

type Endo<'a> with
    //Semigroup
    member self.mappend(Endo b) =
        match self with
        | Endo a -> b >> a |> Endo
    //Monoid
    static member mempty: Endo<'a> = Endo id

//let a = Endo id

let appEndo (Endo appEndo) = appEndo
