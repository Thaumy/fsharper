[<AutoOpen>]
module fsharper.moreType.Ord

type Ordering =
    | GT
    | EQ
    | LT


type SortOrdering =
    | ASC
    | DESC

let cmp a b =
    if a > b then GT
    else if a = b then EQ
    else LT

let inline eq a b = a = b
