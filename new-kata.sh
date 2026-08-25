#!/usr/bin/env bash

set -euo pipefail
IFS=$'\n\t'
# Note that `set +e` is the syntax to disables variable strictness. This is
# particularly helpful if you need to source a script that violates any of these
# `set`s.

# Assert working directory is correct
if [ ! -f ./CodingKata.slnx ]
then
  echo "Must execute this script with the working directory that contains CodingKata.slnx"
  exit 1
fi

# Read kata kata_name
kata_name=${1:-}
method_name=${2:-}
if [[ -z "$kata_name" || -z "$method_name"  ]]; then
    echo "usage: $0 KATA_NAME METHOD_NAME"
    exit 1
fi

# Compute new challenge dir kata_name (nnn-NAME) corresponding namespace (_nnn_NAME)
last_num=$(ls -1 CodingKata.Console/Challenges/ | cut -d'-' -f1 | sort -rn | head -1)
new_num=$((last_num+1))
printf -v new_num_padded "%03d" $new_num
new_name="${new_num_padded}-${kata_name}"
new_namespace="_${new_num_padded}_${kata_name}"

# Make Kata.cs
source_file_path=./CodingKata.Console/Challenges/$new_name/Kata.cs
mkdir "$(dirname "$source_file_path")"
source_file_content="
using System;
using System.Linq;

namespace CodingKata.Console.Challenges.${new_namespace};

/// <summary>
/// TODO: summary
/// <example>
/// TODO: example
/// </example>
/// </summary>
/// <remarks>
/// TODO: kyu level
/// </remarks>
public class Kata
{
    /// <inheritdoc cref=\"Kata\"/>
    public static string ${method_name}(string str) // TODO: adjust input/output types as needed
    {
        throw new NotImplementedException(); // TODO: this
    }
}
"
echo "$source_file_content" > "$source_file_path"

# Make KataTests.cs
test_file_path=./CodingKata.Tests/Console/Challenges/$new_name/KataTests.cs
mkdir "$(dirname "$test_file_path")"
test_file_content="
using CodingKata.Console.Challenges.${new_namespace};

namespace CodingKata.Tests.Console.Challenges.${new_namespace};

public class KataTests
{
    [Fact]
    public static void GivenTest0()
    {
        Assert.Fail(); // TODO: this
    }
}
"
echo "$test_file_content" > "$test_file_path"

echo "Created stubbed Kata and test file:"
git status
