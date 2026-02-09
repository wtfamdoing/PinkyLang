# PinkyLang (C# Interpreter)

This project is a simple educational interpreter for the **Pinky** scripting language, written in **C#**.

Link on the pinky lang: https://pinky-lang.org/

Pinky is an extremely minimal language that is commonly used by beginners when implementing their first compiler or interpreter.  
This project focuses on understanding the core concepts behind language implementation rather than performance or advanced features.

## Project Overview

The interpreter reads a source file written in the Pinky language, performs lexical analysis, and executes the code directly.  
At its current stage, the project works as an **interpreter**, not a compiler — no binary or intermediate code is generated.

The main goals of the project are:
- learning how a lexer works,
- understanding tokenization and basic parsing,
- executing instructions from a custom scripting language,
- gaining hands-on experience with language design and implementation.

## How It Works

1. Create a file with the `.pinky` extension.
2. Write Pinky source code inside the file.
3. Run it with the Makefile or ```dotnet run file.pinky```
4. The file is read, parsed, and executed immediately.

## Example

```pinky
print "Hello, world!"
