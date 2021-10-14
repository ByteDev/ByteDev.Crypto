# Release Notes

## 8.0.0 - 14 October 2021

Breaking changes:
- Renamed `CryptoRandom` to `CryptoRandomString`.

New features:
- Added `CryptoRandomNumber`.
- Added `CharacterSets.UpperCaseHex`.
- Added `CharacterSets.LowerCaseHex`.

Bug fixes / internal changes:
- (None)

## 7.0.0 - 31 May 2021

Breaking changes:
- Removed `ClearPhrase.Value` property (use `ToString()` instead).

New features:
- Added `ClearPhrase.Encoding` property (default UTF-8).

Bug fixes / internal changes:
- Fixed `FileChecksumService` to handle upper or lower case Hex encoded hashes as the same thing.
- Fixed `HashService` to handle upper or lower case Hex encoded hashes as the same thing.

## 6.1.0 - 24 May 2021

Breaking changes:
- (None)

New features:
- Added `FileChecksumService.Matches`.

Bug fixes / internal changes:
- (None)

## 6.0.0 - 20 January 2021

Breaking changes:
- Moved `HashService.CalcFileChecksum` to `FileChecksumService.Calculate`

New features:
- Added `FileChecksumService.Verify`.
- Added `ObjectExtensions.IsSensitive`.

Bug fixes / internal changes:
- Fixed `CryptoRandom.GenerateArray` to handle a negative length.
- Fixed `CryptoRandom.GenerateArray` so `minLength` param can never be less than zero.

## 5.1.1 - 18 January 2021

Breaking changes:
- (None)

New features:
- (None)

Bug fixes / internal changes:
- Fixed bug in `EncryptionService.EncryptProperties` and `DecryptProperties` methods to handle properties with null or empty.

## 5.1.0 - 18 December 2020

Breaking changes:
- (None)

New features:
- Added Base32 encoding support.
- Added `CalcFileChecksum` overload that takes a buffer size.

Bug fixes / internal changes:
- (None)

## 5.0.2 - 02 July 2020

Breaking changes:
- (None)

New features:
- (None)

Bug fixes:
- Added license and project URL as part of package.

## 5.0.1 - 02 July 2020

Breaking changes:
- (None)

New features:
- (None)

Bug fixes:
- Removed encoding classes; now using `ByteDev.Encoding` package.

## 5.0.0 - 08 June 2020

Breaking changes:
- Renamed `HashEncoding` to `EncodingType`.

New features:
- Added `EncryptionService.EncryptProperties`.
- Added `EncryptionService.DecryptProperties`.
- Added `CharacterSets.AlphaNumeric`.

Bug fixes:
- (None)

## 4.0.0 - 29 April 2020

Breaking changes:
- Renamed `HashPhrase` to `ClearPhrase`.

New features:
- `HashService` can now take a `HashEncoding` on constructor.

Bug fixes:
- (None)

## 3.3.0 - 25 April 2020

Breaking changes:
- (None)

New features:
- Added `Md5Algorithm`.
- Added `HashService.CalcFileChecksum`.

Bug fixes / internal changes:
- (None)

## 3.2.0 - 28 March 2020

Breaking changes:
- (None)

New features:
- Added `CryptoRandom.GenerateArray` overload.

Bug fixes / internal changes:
- (None)

## 3.1.0 - 25 March 2020

Breaking changes:
- (None)

New features:
- Added `CharacterSets.AsciiSpecial`.
- Added `CryptoRandom.GenerateString` overload.

Bug fixes / internal changes:
- (None)

## 3.0.3 - 28 February 2020

Breaking changes:
- (None)

New features:
- (None)

Bug fixes / internal changes:
- Fixed .NET Standard package dependency.
