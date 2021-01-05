#load "FsAdvent2020/Day3.fs"

open System.IO
open FsAdvent2020

let entries = "Day3Data.txt" |> File.ReadAllLines
let treesHit slope =
    entries
    |> TobogganTrajectory.countTrees slope
    |> Seq.where id
    |> Seq.length

[
    1, 1
    3, 1
    5, 1
    7, 1
    1, 2
]
|> List.map (treesHit)
|> List.map (fun i -> printfn "Number of trees hit: %i" i; i)
|> List.map int64
|> List.fold (*) 1L
|> printfn "Product of trees hit: %i" // 3316272960
