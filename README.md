# Neolution.Utilities

This is a collection of utilities that we have created to help with our development process. We have decided to open source these utilities in the hopes that they will be useful to others.

## Usage

Add the NuGet package to your project and reference the desired classes in your source code.

## Additional Packages

This library includes specialized packages that extend the core utilities for specific frameworks and third-party libraries. These packages are deliberately separated from the base `Neolution.Utilities` package to maintain its framework-agnostic and dependency-free nature, ensuring it can be used in any .NET project without forcing unwanted dependencies.

## Contributing

If you would like to contribute to this project, please submit a pull request.

### Namespace Organization

To ensure clear and unambiguous contribution guidelines, organize utilities into the following namespaces based on their structure:

- **`Neolution.Utilities.Extensions`** - for all extension methods
- **`Neolution.Utilities.Abstractions`** - for interfaces, base classes, and abstractions
- **`Neolution.Utilities.Helpers`** - for static utility classes

### File Naming Guidelines

- **Extensions**: Always `{Type}Extensions.cs` (e.g., `StringExtensions.cs`)
- **Abstractions**: Use descriptive names (e.g., `IValidator.cs`, `RetryPolicy.cs`)
- **Helpers**: Use clear, descriptive names that indicate the class purpose

### Cross-Namespace Dependencies

Extensions can reference Helpers and Abstractions when it makes sense for code reuse and maintainability.

### Extension Method Policy

Create utility methods as extension methods by default for discoverability and ease of use.

**Exceptions - Use static helper classes for:**

- All CLR primitives (`bool`, `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `nint`, `nuint`, `char`, `double`, `float`)
- Selected System .NET types: `decimal`, `object`, and `Enum`

**Exception to the exception:** Custom string representation methods (e.g., `ToCurrencyString()`, `ToHexString()`) are permitted as extension methods on the above types, using the `ToXyzString()` naming convention where `Xyz` describes the output format.

### Test Coverage

Add comprehensive unit tests that cover various usage scenarios, edge cases, and parameter combinations. Strive for high code coverage to ensure reliability.

## Releases

Due to the nature of this project as a loosely connected collection of utilities, it's important to be strict about following [SemVer](https://semver.org/) to communicate possible breaking changes to the users of this library via version number.

Equally as important is to be precise about the changes made in each release and maintain the [CHANGELOG.md](CHANGELOG.md) according to the [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) format so users of this library can see easily where changes were made.
