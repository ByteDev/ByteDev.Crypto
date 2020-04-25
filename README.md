[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Crypto?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Crypto/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Crypto.svg)](https://www.nuget.org/packages/ByteDev.Crypto)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/dcbdaad51dac43e9aad1736377992264)](https://www.codacy.com/manual/ByteDev/ByteDev.Crypto?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ByteDev/ByteDev.Crypto&amp;utm_campaign=Badge_Grade)

# ByteDev.Crypto

Provides simple cryptographic related classes for hashing/verifying data, encrypting/decrypting data and creating crypto ramdon data in .NET.

## Installation

ByteDev.Crypto has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Crypto is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Crypto`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Crypto/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Crypto/blob/master/docs/RELEASE-NOTES.md).

## Code

The repo can be cloned from git bash:

`git clone https://github.com/ByteDev/ByteDev.Crypto`

## Usage

### Hashing

Hash some clear text (returned as base 64 string) and verify a guess is equal.

```csharp
IHashService service = new HashService();

string hash = service.Hash(new HashPhrase("Password1"));

bool isLoginSuccessful = service.Verify(new HashPhrase("passwordGuess"), hash);
```

Calculate a checksum (returned as base 64 string) for a file.

```csharp
IHashService service = new HashService(new Md5Algorithm());

string checksum = service.CalcFileChecksum(@"C:\somefile.txt");
```

### Encryption

Encrypt a secret with a key and then decrypt it.

```csharp
var algo = new RijndaelAlgorithm();

var keyFactory = new EncryptionKeyIvFactory(algo);
var keyIv = keyFactory.Create("Password1", Encoding.UTF8.GetBytes("someSalt"));

IEncryptionService service = new EncryptionService(algo);

byte[] cipher = service.Encrypt("mySecret", keyIv);

string clearText = service.Decrypt(cipher, keyIv);	// clearText == "mySecret"
```

### Random

Generate a random string of a specified length using only the character set specified.

```csharp
const int length = 5;

using (var r = new CryptoRandom(CharacterSets.Digits))
{
    string randomString = r.GenerateString(length);
}
```
