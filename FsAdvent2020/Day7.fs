module FsAdvent2020.HandyHaversacks

open System
open System.Collections.Generic

module BagRules =
    let parseBagDescription (description:string) =
        match description.Split null |> Seq.take 3 |> List.ofSeq with
        | [ quantity; adjective; color ] ->
            Int32.Parse quantity, (sprintf "%s %s" adjective color)
        | _ -> failwithf "Unable to parse bag description: '%s'" description
        
    let parse (rule:string) =
        match rule.Split ("bags contain", StringSplitOptions.TrimEntries) with
        | [|color;contents|] when contents.StartsWith "no" ->
            color, Seq.empty
        | [|color;contents|] ->
            match contents.Split (",", StringSplitOptions.TrimEntries) with
            | bagDescriptions ->
                let bags = bagDescriptions |> Seq.map parseBagDescription
                color, bags
        | _ -> failwithf "Unable to parse bag contents: '%s'" rule

    /// Parses bag rules to build map of bag color to contained quantity of colors of bags
    let parseRules(rules:string seq) =
        rules |> Seq.map parse |> Map.ofSeq

    let colorsThatCanContain (color:string) (rules:Map<string, seq<int * string>>) : int =
        let rec canContain (color:string) (contents:seq<int * string>) =
            match contents |> List.ofSeq with
            | [] -> false
            | head::tail ->
                if (snd head) = color then true
                else
                    let nested = rules.[snd head]
                    if canContain color nested then true
                    else canContain color tail
        let mutable count = 0
        for kvp in rules do
            if canContain color kvp.Value then count <- count + 1
        count
    
    let numberOfBagsWithinABagColor (color:string) (rules:Map<string, seq<int * string>>) : int =
        // If a bag contains no bags, just count that bag
        // If it contains bags, then count that bag + the bags it contains
        let rec contains (quantity:int) (bagColor:string) =
            match rules.[bagColor] |> List.ofSeq with
            | [] -> quantity // Just the quantity of this bag, no contents
            | contents -> // Quantity of bags in this bag * quantity of this bag
                quantity +
                    (contents
                    |> List.map (fun (count, color) -> (quantity * (contains count color)))
                    |> List.sum)
        let thisBagAndContents = contains 1 color
        thisBagAndContents - 1 // Don't count itself
