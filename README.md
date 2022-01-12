[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Crypto?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Crypto/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Crypto.svg)](https://www.nuget.org/packages/ByteDev.Crypto)

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

## Usage

The library is broken into three main namespaces:
- Hashing
- Encryption
- Random

---

### Hashing

Use namespace `ByteDev.Crypto.Hashing`. 

This namespace contains two main classes: `HashService` and `FileChecksumService`.

#### `HashService`

`HashService` class methods:
- Hash
- Verify

```csharp
IHashService service = new HashService(new Md5Algorithm(), EncodingType.Base64);

// Hash a phrase
string base64Hash = service.Hash(new ClearPhrase("Password1"));

// Verify a phrase against a hash
bool isSuccessful = service.Verify(new ClearPhrase("Pasword123456"), base64Hash);
```

#### `FileChecksumService`

`FileChecksumService` class methods:
- Calculate
- Verify
- Matches

```csharp
IFileChecksumService service = new FileChecksumService(new Md5Algorithm(), EncodingType.Hex);

// Calculate file checksum (hash)
string hexChecksum = service.Calculate(@"C:\myFile.txt");

// Verify existing checksum matches file's
bool isSuccessful = service.Verify(@"C:\myFile.txt", hexChecksum);

// Search directory for files with a particular checksum
IList<string> matches = service.Matches(@"C:\Temp", hexChecksum); 
```

---

### Encryption

Use namespace `ByteDev.Crypto.Encryption`.

`EncryptionService` class methods:
- Encrypt
- Decrypt
- EncryptProperties
- DecryptProperties

Initializing `EncryptionService`:

```csharp
IEncryptionAlgorithm algo = new RijndaelAlgorithm();

IEncryptionKeyIvFactory keyFactory = new EncryptionKeyIvFactory(algo);
EncryptionKeyIv keyIv = keyFactory.Create("Password1", Encoding.UTF8.GetBytes("someSalt"));

IEncryptionService service = new EncryptionService(algo, keyIv);
```

#### `Encrypt` & `Decrypt`

Encrypt a secret with a key and then decrypt it:

```csharp
byte[] cipher = service.Encrypt("mySecret");

string clearText = service.Decrypt(cipher);	 

// clearText == "mySecret"
```

Once a byte array cipher has been created the `ByteDev.Encoding.Encoder` class can be used for any required encoding:

```csharp
byte[] cipher = service.Encrypt("mySecret", keyIv);

Encoder encoder = new Encoder(EncodingType.Hex);

string hex = encoder.Encode(cipher);
```

#### `EncryptProperties` & `DecryptProperties`

The `EncryptionService` class also supports encrypting/decrypting an object's public string properties that use `EncryptAttribute` through the `EncryptProperties` and `DecryptProperties` methods.

```csharp
public class MyInfo
{
    [Encrypt]
    public string Secrets { get; set; }
}

var info = new MyInfo { Secrets = "Some secrets" };

service.EncryptProperties(info, EncodingType.Hex);

// info.Secrets is now encrypted and encoded as hex

service.DecryptProperties(info, EncodingType.Hex);

// info.Secrets == "Some secrets"
```

---

### Random

Use namespace `ByteDev.Crypto.Random`.

`CryptoRandomString` class methods:
- GenerateString
- GenerateArray

#### `GenerateString`

Generate a random string of a specified length using only the character set specified (you can also call `GenerateString` with a min and max length):

```csharp
const int length = 5;

using (var r = new CryptoRandomString(CharacterSets.AlphaNumeric))
{
    string randomString = r.GenerateString(length);
}
```

#### `GenerateArray`

Generate a char array of random characters of a specified length using only the character set specified (you can also call `GenerateArray` with a min and max length):

```csharp
const int length = 10;

using (var r = new CryptoRandomString(CharacterSets.Digits))
{
    char[] randomChars = r.GenerateArray(length);
}
```

`CryptoRandomNumber` class methods:
- GenerateInt32

#### `GenerateInt32`

Generate a random number.

```csharp
using (var r = new CryptoRandomNumber())
{
    int randomInt = r.GenerateInt32();
}
```
