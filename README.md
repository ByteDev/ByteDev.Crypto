[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Crypto?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Crypto/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Crypto.svg)](https://www.nuget.org/packages/ByteDev.Crypto)

# ByteDev.Crypto

Provides simple cryptographic related classes for hashing/verifying and encrypting/decrypting data in .NET.

## Installation

ByteDev.Crypto has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Crypto is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Crypto`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Crypto/).

## Code

The repo can be cloned from git bash:

`git clone https://github.com/ByteDev/ByteDev.Crypto`

## Usage

### Hashing

Simple example of hasing some clear text ("Password123") and verifying a guess is equal.

```
var service = new HashService();

string hash = service.Hash("Password123");

bool isLoginSuccessful = service.Verify("passwordGuess", hash);
```

### Encryption

Simple example of encrypting a secret with a key and then decrypting it.

```
var algo = new RijndaelAlgorithm();

var keyFactory = new EncryptionKeyIvFactory(algo);
var keyIv = keyFactory.Create("Password1", Encoding.UTF8.GetBytes("someSalt"));

var service = new EncryptionService(algo);

byte[] cipher = service.Encrypt("mySecret", keyIv);

string clearText = service.Decrypt(cipher, keyIv);	// clearText = "mySecret"
```
