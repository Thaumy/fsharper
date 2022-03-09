[<AutoOpen>]
module fsharper.types.Dual


type Dual<'a> = Dual of getDual: 'a

let getDual (Dual getDual) = getDual
