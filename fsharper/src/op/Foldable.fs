[<AutoOpen>]
module fsharper.op.Foldable

open fsharper.types.Dual
open fsharper.types.Endo


let inline private runFoldMap f t =
    (^t: (member foldMap : (^a -> ^m) -> ^m) t, f)

let inline private runFoldr f acc t =
    (^t: (member foldr : (^a -> ^b -> ^b) -> ^b -> ^b) t, f, acc)

let inline fold t = runFoldMap id t

let inline foldMap f t = runFoldr (mappend << f) mempty t

let inline foldr f (acc: ^acc) (t: ^t) = appEndo (runFoldMap (Endo << f) t) acc

let inline foldl f (acc: ^acc) (t: ^t) =
    let inline flip f a b = f b a
    appEndo (getDual (runFoldMap (Dual << Endo << flip f) t)) acc
