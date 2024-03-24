
# AsyncLinqR

![CI](https://github.com/pascal-libaud/AsyncLinqR/workflows/Continuous%20Integration/badge.svg) [![NuGet](https://img.shields.io/nuget/v/AsyncLinqR.svg)](https://www.nuget.org/packages/AsyncLinqR/)

## English version

[Version française juste en dessous](#version-fran%C3%A7aise)

## Introduction

The **AsyncLinqR** library is an extension to LINQ for .NET that provides asynchronous versions of existing LINQ methods.
These methods allow you to perform data query operations asynchronously.

## Features

- Asynchronous versions of popular LINQ methods such as `Where`, `Select`, `First`, `FirstOrDefault` etc.
- Seamless support for asynchronous operations through the use of `async` and `await`.
- Support for asynchronous data types such as `Task<T>` and `IAsyncEnumerable<T>`.

## Examples of use

### Filter a collection asynchronously:

```csharp
using AsyncLinqR;

var result = await myCollection.WhereAsync(async x => await PredicateAsync(x)).ToListAsync();
```

Map a collection asynchronously :
```csharp

using AsyncLinqR;

var result = await myCollection.SelectAsync(async x => await SomeOperationAsync(x)).ToListAsync();
```

## Installation

You can install the library via NuGet Package Manager or via the package management console:
```package-manager
Install-Package AsyncLinqR
```

Contributions

Contributions and feedback are welcome! You can submit an Issue or a Pull Request on the project page.

## License

This project is licensed under the MIT license. For more information, please consult the LICENSE file.


## Version française

## Introduction

La bibliothèque **AsyncLinqR** est une extension de LINQ pour .NET qui fournit des versions asynchrones des méthodes de LINQ existantes.
Ces méthodes vous permettent d'effectuer des opérations de requête de données de manière asynchrone.

## Fonctionnalités

- Versions asynchrones des méthodes LINQ populaires telles que `Where`, `Select`, `First`, `FirstOrDefault` etc.
- Prise en charge transparente des opérations asynchrones grâce à l'utilisation de `async` et `await`.
- Compatibilité avec les types de données asynchrones tels que `Task<T>` et `IAsyncEnumerable<T>`.

## Exemples d'utilisation

### Filtrer une collection de manière asynchrone :

```csharp
using AsyncLinqR;

var result = await myCollection.WhereAsync(async x => await PredicateAsync(x)).ToListAsync();
```

Mapper une collection de manière asynchrone :
```csharp

using AsyncLinqR;

var result = await myCollection.SelectAsync(async x => await SomeOperationAsync(x)).ToListAsync();
```

## Installation

Vous pouvez installer la bibliothèque via NuGet Package Manager ou via la console de gestion de package :
```package-manager
Install-Package AsyncLinqR
```

Contributions

Les contributions, feedbacks, sont les bienvenues ! Vous pouvez soumettre une Issue ou une Pull Request sur la page du projet.

## Licence

Ce projet est sous licence MIT. Pour plus d'informations, veuillez consulter le fichier LICENSE.
