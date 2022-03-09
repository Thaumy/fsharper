[<AutoOpen>]
module fsharper.moreType.Ord


type Ordering =
    | GT
    | EQ
    | LT

type SortOrdering =
    | ASC
    | DESC

let inline eq a b = (=) a b

let inline lt a b = (<) a b

let inline gt a b = (>) a b

let inline le a b = (<=) a b

let inline ge a b = (>=) a b


let cmp a b =
    match a, b with
    | _ when a > b -> GT
    | _ when a = b -> EQ
    | _ -> LT
