# KeySpace (cs-keyspace)

[![Travis CI](https://img.shields.io/travis/tabrath/cs-keyspace.svg?style=flat-square&branch=master)](https://travis-ci.org/tabrath/cs-keyspace)
[![AppVeyor](https://img.shields.io/appveyor/ci/tabrath/cs-keyspace/master.svg?style=flat-square)](https://ci.appveyor.com/project/tabrath/cs-keyspace)
[![NuGet](https://img.shields.io/nuget/v/KeySpace.svg?style=flat-square)](https://www.nuget.org/packages/KeySpace)
[![NuGet](https://img.shields.io/nuget/dt/KeySpace.svg?style=flat-square)](https://www.nuget.org/packages/KeySpace)
[![Codecov](https://img.shields.io/codecov/c/github/tabrath/cs-keyspace/master.svg?style=flat-square)](https://codecov.io/gh/tabrath/cs-keyspace)
[![Libraries.io](https://img.shields.io/librariesio/github/tabrath/cs-keyspace.svg?style=flat-square)](https://libraries.io/github/tabrath/cs-keyspace)

> Implementation/port of [whyrusleeping/go-keyspace](https://github.com/whyrusleeping/go-keyspace) in C# .NET Standard 1.6.

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Maintainers](#maintainers)
- [Contribute](#contribute)
- [License](#license)

## Install

    PM> Install-Package KeySpace

--

    dotnet add package KeySpace

## Usage

``` cs
var keyA = XORKeySpace.Instance.Key(bytes);
var keyB = XORKeySpace.Instance.Key(bytes);
var distance = keyA.Distance(keyB);
```

## Maintainers

Captain: [@tabrath](https://github.com/tabrath).

## Contribute

Contributions welcome. Please check out [the issues](https://github.com/tabrath/cs-keyspace/issues).

## License

[MIT](LICENSE) © 2017 Trond Bråthen
