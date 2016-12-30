# KeySpace (cs-keyspace)

[![Build Status](https://travis-ci.org/tabrath/cs-keyspace.svg?branch=master)](https://travis-ci.org/tabrath/cs-keyspace)
[![Build status](https://ci.appveyor.com/api/projects/status/wwt31xhbpmo2y11h?svg=true)](https://ci.appveyor.com/project/tabrath/cs-keyspace)
[![NuGet Badge](https://buildstats.info/nuget/KeySpace)](https://www.nuget.org/packages/KeySpace/)
[![codecov](https://codecov.io/gh/tabrath/cs-keyspace/branch/master/graph/badge.svg)](https://codecov.io/gh/tabrath/cs-keyspace)

C# implementation/port of [whyrusleeping/go-keyspace](https://github.com/whyrusleeping/go-keyspace).

## Install

    PM> Install-Package KeySpace

## Usage

``` cs
var keyA = XORKeySpace.Instance.Key(bytes);
var keyB = XORKeySpace.Instance.Key(bytes);
var distance = keyA.Distance(keyB);
```
