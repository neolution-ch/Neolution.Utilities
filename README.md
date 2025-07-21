# Neolution.Utilities

This is a collection of utilities that we have created to help with our development process. We have decided to open source these utilities in the hopes that they will be useful to others.

## Usage

Add the NuGet package to your project and reference the desired classes in your source code.

## Contributing

If you would like to contribute to this project, please submit a pull request.

### Namespace Organization

Use a namespace that reflects the utility's purpose. Choose names that are specific enough to be meaningful but generic enough to accommodate similar utilities in the future.

### Extension Method Restrictions

To prevent API pollution and naming conflicts, use static helper classes instead of extension methods for the following types:

- All CLR primitives (`bool`, `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `nint`, `nuint`, `char`, `double`, `float`)
- Selected System .NET types: `string`, `decimal`, `object`, and `Enum`

**Exception:** Extension methods that provide custom string representations are permitted, provided they follow the `ToXyzString()` naming convention, where `Xyz` clearly indicates the format or purpose of the output (e.g., `ToCurrencyString()`, `ToHexString()`). This ensures consistency and clarity in API design across the library.

### Test Coverage

Add comprehensive unit tests that cover various usage scenarios, edge cases, and parameter combinations. Strive for high code coverage to ensure reliability.

## Releases

Due to the nature of this project as a loosely connected collection of utilities, it's important to be strict about following [SemVer](https://semver.org/) to communicate possible breaking changes to the users of this library via version number.

Equally as important is to be precise about the changes made in each release and maintain the [CHANGELOG.md](CHANGELOG.md) according to the [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) format so users of this library can see easily where changes were made.
