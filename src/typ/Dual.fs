module fsharper.typ.Dual

//TODO exp feature

type Dual<'a> = Dual of getDual: 'a

let inline mappend ma mb =
    (^m: (member mappend : ^m -> ^m) ma, mb)

let inline mempty< ^m when ^m: (static member mempty : unit -> ^m)> =
    (^m: (static member mempty : unit -> ^m) ())


type Dual<'a> with
    //Semigroup
    static member inline mappend(ma: Dual< ^x >, mb: Dual< ^x >) =
        match ma, mb with
        | Dual a, Dual b -> Dual(mappend b a)

    //Monoid
    static member inline mempty() = Dual mempty< ^m>


let getDual (Dual getDual) = getDual
