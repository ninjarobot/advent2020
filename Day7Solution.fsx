#load "FsAdvent2020/Day7.fs"

open System.IO
open FsAdvent2020.HandyHaversacks

"Day7Data.txt" |> File.ReadAllLines
|> BagRules.parseRules
|> BagRules.colorsThatCanContain "shiny gold"
|> printfn "Number of bags that contain a shiny gold bag: %i"

"Day7Data.txt" |> File.ReadAllLines
|> BagRules.parseRules
|> BagRules.numberOfBagsWithinABagColor "shiny gold"
|> printfn "Number of bags contained in a shiny gold bag: %i"
