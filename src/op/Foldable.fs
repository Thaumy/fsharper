module fsharper.op.Foldable

let inline foldMap f t =
    (^t: (member foldMap : (^a -> ^m) -> ^m) t, f)

let inline foldr f acc t =
    (^t: (member foldr : (^a -> ^acc -> ^acc) * ^acc -> ^acc) t, f, acc)

let inline foldl f acc t =
    (^t: (member foldl : (^acc -> ^a -> ^acc) * ^acc -> ^acc) t, f, acc)

//TODO

(*type Foldable<'a> =
    abstract member foldMap : ('a -> 'acc) -> 'acc
    abstract member foldr : ('a -> 'acc -> 'acc) -> 'acc -> 'acc*)

(*let inline private runFoldMap f t =
    (^t: (member foldMap : (^a -> ^m) -> ^m) t, f)

let inline private runFoldr f acc t =
    (^t: (member foldr : (^a -> ^b -> ^b) * ^b -> ^b) t, f, acc)

let inline fold t = runFoldMap id t

let inline foldMap f (t: ^t) = runFoldr (mappend << f) mempty t

let inline foldr f (acc: ^acc) (t: ^t) = appEndo (runFoldMap (Endo << f) t) acc

let inline foldl f (acc: ^acc) (t: ^t) =
    let inline flip f a b = f b a
    appEndo (getDual (runFoldMap (Dual << Endo << flip f) t)) acc*)

(*
let inline private mappend ma mb =
    (^m: (member mappend : ^m -> ^m) ma, mb)

let inline private mempty< ^m when ^m: (static member mempty : unit -> ^m)> =
    (^m: (static member mempty : unit -> ^m) ())


let inline foldMap< ^a, ^acc, ^t when ^t :> Foldable< ^a > and ^acc: (static member mempty : unit -> ^acc) and ^acc: (member mappend :
    ^acc -> ^acc)>
    (f: ^a -> ^acc)
    (t: ^t)
    =
    t.foldr (mappend << f) mempty< ^acc>

let inline foldr< ^a, ^acc, ^t when ^t :> Foldable< ^a >> (f: ^a -> ^acc -> ^acc) (z: ^acc) (t: ^t) =
    appEndo (t.foldMap (Endo << f)) z
*)
