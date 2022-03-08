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

let inline eq a b = (=) a b

let inline lt a b = (<) a b

let inline gt a b = (>) a b

let inline le a b = (<=) a b

let inline ge a b = (>=) a b
