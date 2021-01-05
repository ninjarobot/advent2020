module Day6Tests

open Xunit
open FsAdvent2020.CustomCustoms

[<Fact>]
let ``Count distinct answers`` () =
    let data = """ab
ac
"""
    let num = numDistinctAnswers data
    Assert.Equal(3, num)

[<Fact>]
let ``Count agreed answers`` () =
    let data = """ab
ac
"""
    let num = numEveryoneAnswers data
    Assert.Equal(1, num)

let testData = """
abc

a
b
c

ab
ac

a
a
a
a

b
"""

[<Fact>]
let ``Sum distinct per group`` () =
    let sum = testData |> sumDistinctPerGroup
    Assert.Equal(11, sum)

[<Fact>]
let ``Sum agreed per group`` () =
    let sum = testData |> sumEveryoneInGroup
    Assert.Equal(6, sum)
