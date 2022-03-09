[<AutoOpen>]
module fsharper.moreType.Dual


type Dual<'a> = Dual of getDual: 'a

let getDual (Dual getDual) = getDual
