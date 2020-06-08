[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Crypto?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Crypto/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Crypto.svg)](https://www.nuget.org/packages/ByteDev.Crypto)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/dcbdaad51dac43e9aad1736377992264)](https://www.codacy.com/manual/ByteDev/ByteDev.Crypto?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ByteDev/ByteDev.Crypto&amp;utm_campaign=Badge_Grade)

# ByteDev.Crypto

Provides simple cryptographic related classes for hashing/verifying data, encrypting/decrypting data and creating crypto random data in .NET.

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

The library's main classes:
- HashingService
- EncryptionService
- CryptoRandom

### Hashing

Use namespace `ByteDev.Crypto.Hashing`.

HashService class methods:
- Hash
- Verify
- CalcFileChecksum

Hash some clear text (returned as base 64 encoded string) and verify a guess is equal:

```csharp
IHashService service = new HashService(new Md5Algorithm(), EncodingType.Base64);

string hash = service.Hash(new ClearPhrase("Password1"));

bool isLoginSuccessful = service.Verify(new ClearPhrase("passwordGuess"), hash);
```

Calculate a checksum for a file (returned as hex encoded string):

```csharp
IHashService service = new HashService(new Md5Algorithm(), EncodingType.Hex);

string checksum = service.CalcFileChecksum(@"C:\somefile.txt");
```

### Encryption

Use namespace `ByteDev.Crypto.Encryption`.

EncryptionService class methods:
- Encrypt
- Decrypt
- EncryptProperties
- DecryptProperties

Create the `EncryptionService` class:

```csharp
IEncryptionAlgorithm algo = new RijndaelAlgorithm();

IEncryptionKeyIvFactory keyFactory = new EncryptionKeyIvFactory(algo);
EncryptionKeyIv keyIv = keyFactory.Create("Password1", Encoding.UTF8.GetBytes("someSalt"));

IEncryptionService service = new EncryptionService(algo, keyIv);
```

Encrypt a secret with a key and then decrypt it:

```csharp
byte[] cipher = service.Encrypt("mySecret");

string clearText = service.Decrypt(cipher);	 

// clearText == "mySecret"
```

Once a byte array cipher has been created the `ByteDev.Crypto.Encoding.Encoder` class can be used for any required encoding:

```csharp
byte[] cipher = service.Encrypt("mySecret", keyIv);

Encoder encoder = new Encoder(EncodingType.Hex);

string hex = encoder.Encode(cipher);
```

The `EncryptionService` class also supports encrypting/decrypting an object's public string properties that use `EncryptAttribute` through the `EncryptProperties` and `DecryptProperties` methods.

```csharp
public class MyInfo
{
    [Encrypt]
    public string Secrets { get; set; }
}

var info = new MyInfo { Secrets = "Some secrets" };

services.EncryptProperties(info, EncodingType.Hex);

// info.Secrets is now encrypted and encoded as hex

services.DecryptProperties(info, EncodingType.Hex);

// info.Secrets == "Some secrets"
```

### Random

Use namespace `ByteDev.Crypto.Random`.

CryptoRandom class methods:
- GenerateString
- GenerateArray

Generate a random string of a specified length using only the character set specified:

```csharp
const int length = 5;

using (var r = new CryptoRandom(CharacterSets.AlphaNumeric))
{
    string randomString = r.GenerateString(length);
}
```

You can also call `GenerateString` with a min and max length.
