# Release Notes

## 5.1.0 - ?

Breaking changes:
- (None)

New features:
- Added Base32 encoding support.

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
