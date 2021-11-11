
# SMTV Save Utility

A simple save decryption and encryption utility for Shin Megami Tensei V.

## Requirements

- .NET Runtime 5.0+
- SMTV Save File(s)

## Usage

- Locate and dump your SMTV saves (`SysSave`, `GameSaveXX`, `AutoSaveXX`).

- To decrypt or encrypt a save:

  `smtv.saveutil.exe -i </path/to/save>`

  By default, the program outputs decrypted or encrypted save files with a `_dec` or `_enc` suffix respectively.

- To output to a specific path:

  `smtv.saveutil.exe -i </path/to/save_in> -o </path/to/save_out>`

## Example

```powershell
> smtv.saveutil.exe -i GameSave02

Decrypting...
Writing to .\GameSave02_dec...
Done

> smtv.saveutil.exe -i GameSave02_dec -o GameSave02

Encrypting...
Writing to .\GameSave02...
Done
```
