[<AutoOpen>]
module fsharper.op.Foldable

open System.Collections.Generic

//TODO 此实现受限于RFC FS-1043

let inline foldMap f t =
    (^t: (member foldMap: (^a -> ^m) -> ^m) t, f)

let inline foldr f acc t =
    (^t: (member foldr: (^a -> ^acc -> ^acc) * ^acc -> ^acc) t, f, acc)

let inline foldl f acc t =
    (^t: (member foldl: (^acc -> ^a -> ^acc) * ^acc -> ^acc) t, f, acc)

type IEnumerator<'T> with

    member self.foldr f acc =
        if self.MoveNext() then
            f self.Current (self.foldr f acc)
        else
            acc

    member inline self.foldl f acc =
        let f' = fun x g -> fun acc' -> g (f acc' x)
        self.foldr f' id acc

    member inline self.foldl1 f =
        if self.MoveNext() then
            self.foldl f self.Current
        else
            failwith $"empty IEnumerator<{typeof<'T>}>"

    member inline self.foldr1 f =
        let rec go prev =
            if self.MoveNext() then
                f prev (go self.Current)
            else
                prev

        if self.MoveNext() then
            go self.Current
        else
            failwith $"empty IEnumerator<{typeof<'T>}>"

type IEnumerable<'T> with

    member inline self.foldr f acc = self.GetEnumerator().foldr f acc
    member inline self.foldl f acc = self.GetEnumerator().foldl f acc

    member inline self.foldr1 f = self.GetEnumerator().foldr1 f
    member inline self.foldl1 f = self.GetEnumerator().foldl1 f

    member inline self.foldMap f =
        let f' = fun x -> mappend (f x)
        self.foldr f' mempty

type IEnumerable<'T> with

    member inline self.foldr f acc =
        let rec loop f acc (en: IEnumerator<'T>) =
            if en.MoveNext() then
                f en.Current (loop f acc en)
            else
                acc

        loop f acc (self.GetEnumerator())

    member inline self.foldl f acc =
        let f' = fun x g -> fun acc' -> g (f acc' x)
        self.foldr f' id acc

    member inline self.foldMap f =
        let f' = fun x -> mappend (f x)
        self.foldr f' mempty

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
