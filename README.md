# NoChallenge - A tool to automatically defeat .NET crackmes based on string equality comparisons

This tool was designed to automatically solve basic crackmes using clear text string equality comparisons.
It works by hooking the `string.Equals` method in mscorlib and waiting for a special input. 
Once the special input has been entered it will output the correct key it was compared to.
Keep in mind, this tool might not work for every specific implementation.

## Usage
1. Drag & Drop the crackme executable onto NoChallenge.exe
2. Wait for it to tell you that the hook was installed
3. Enter '§' as key in the crackme
4. Wait for NoChallenge to give you the correct password

## Dependencies

- [Harmony](https://github.com/pardeike/Harmony)

