# ByteDev.Crypto

Provides some simple cryptographic related classes for hashing/verifying and encrypting/decrypting strings in .NET.

## Code

ByteDev.Common has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

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
