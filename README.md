# Neolution.Utilities

This is a collection of utilities that we have created to help with our development process. We have decided to open source these utilities in the hopes that they will be useful to others.

## Usage
To use these utilities, simply add the Nuget package to your project and use the desired classes in your source code.

## Contributing
If you would like to contribute to this project, please submit a pull request.

### Guidelines

- Use a namespace that fits what the utility is doing. If you introduce a new namespace, choose a name that fits your utility but is still generic enough to hold similar utilities that can be added later alongside your addition.
- Try to create your utility as an extension method. Except it would extend a primitive type like Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, or Single.
- Add tests to your utilities and strive to cover many different usage scenarios and parametrization and high code coverage.

## Releases
Due to the nature of this project as a loosely connected collection of utilities, it's important to be strict about following [SemVer](https://semver.org/) to communicate possible breaking changes to the users of this library via version number.

Equally as important is to be precise about the changes made in each release and maintain the [CHANGELOG.md](CHANGELOG.md) according to the [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) format so users of this library can see easily where changes were made.
