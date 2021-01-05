module Day3Tests

open System
open Xunit
open FsAdvent2020

let map =
    """..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#"""

[<Fact>]
let ``Num trees hit`` () =
    let treesHit =
        map.Split null
        |> TobogganTrajectory.countTrees (3,1)
        |> Seq.where id
        |> Seq.length
    Assert.Equal(7, treesHit)


[<Fact>]
let ``Num trees hit 1,2 slope`` () =
    let treesHit =
        map.Split null
        |> TobogganTrajectory.countTrees (1,2)
        |> Seq.where id
        |> Seq.length
    Assert.Equal(2, treesHit)

