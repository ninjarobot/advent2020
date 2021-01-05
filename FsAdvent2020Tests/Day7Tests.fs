module Day7Tests

open System
open Xunit
open FsAdvent2020.HandyHaversacks

let rules = [
    "light red bags contain 1 bright white bag, 2 muted yellow bags."
    "dark orange bags contain 3 bright white bags, 4 muted yellow bags."
    "bright white bags contain 1 shiny gold bag."
    "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags."
    "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags."
    "dark olive bags contain 3 faded blue bags, 4 dotted black bags."
    "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags."
    "faded blue bags contain no other bags."
    "dotted black bags contain no other bags."
    ]

[<Fact>]
let ``Parse a bag rule`` () =
    let rule = rules.Head
    let (color, contents) = BagRules.parse rule
    Assert.Equal("light red", color)
    Assert.Equal(1, (contents |> Seq.head) |> fst)
    Assert.Equal("bright white", (contents |> Seq.head) |> snd)
    Assert.Equal(2, (contents |> Seq.skip 1 |> Seq.head) |> fst)
    Assert.Equal("muted yellow", (contents |> Seq.skip 1 |> Seq.head) |> snd)

[<Fact>]
let ``Build bag rule database`` () =
    let rules = BagRules.parseRules rules
    let contains = rules.["faded blue"]
    Assert.Equal (0, contains |> Seq.length)

[<Fact>]
let ``Can contain shiny gold bag`` () =
    let rules = BagRules.parseRules rules
    let count = BagRules.colorsThatCanContain "shiny gold" rules
    Assert.Equal(4, count)

[<Fact>]
let ``Count of bags inside a shiny gold bag`` () =
    let data = """shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.""".Split(Environment.NewLine)
    let rules = BagRules.parseRules data
    let numberContained = BagRules.numberOfBagsWithinABagColor "shiny gold" rules
    Assert.Equal(126, numberContained)
