INTRODUCTION
------------
This is a password utilities library that can be used within .NET applications. for more details about its capabilities, please see this blog entry: http://sleeksoft.co.uk/public/techblog/articles/20130411_1.html

PROJECT GOALS
-------------
Demonstrate best practices in password generation and storage.
Be useful for any type of .NET application, including services, web apps, WinForms, etc.
Be well documented.
Be well tested.
Be performant.
No sudden breaking changes in the library interface.
Able to run on Mono (not yet tested).

PROJECT MATURITY
----------------
This is a immature project, currently only a few weeks old and with only one production implementation.

SUPPORTED FEATURES
------------------
Creation of password policies that specify password min/max length, allowed character sets, and a minimum number of characters from each set.
Creation of password hashing policies that specify the hash algorithm, storage format, work factor, and number of salt bytes.
Password generation using a cryptographically-secure pseudo-random number generator.
Password salting using a cryptographically-secure pseudo-random number generator.
Password hash generation from a range of hash algorithms, although currently only SHA1-160 and SHA2-256 (both with HMAC and PKDBF2) are implemented.
Password hash strengthening based on a specified work factor (number of hash iterations).
Password verification against a previously-stored password/salt hash.
Timing of password and hash generation.
Measurement of the entropy in machine-generated and human-generated passwords, where entropy is a proxy for password strength.
An interactive Windows Forms user interface that demonstrates most of the library's capabilities.

SUPPORTED VERSIONS 
------------------
Language: C# 4.0 and upwards. In fact everything in this library can be compiled in C# 2.0 except for a lambda expression, automatic properties, and some implicit typing (all C# 3.0). The C# 2.0 dependency is on generics.
Framework: Version 4.0 and upwards. 
Runtime: CLR version 4.0 and upwards. 
IDE: Visual Studio 2010 and upwards.

GETTING STARTED
---------------
Add a reference to the library in your project, either using Visual Studio or the command line.
Create a password policy by instantiating the PasswordPolicy class.
Create a hash policy by instantiating the HashPolicy class.
Create passwords by instantiating the PasswordGenerator class.
Salt and hash passwords by instantiating the HashGenerator class.

ROADMAP
-------
Implement SHA3 (Keccak).
Implement SCRYPT.
Implement BCRYPT.
Add manual test vectors to the test gui.
Add automatic test vectors to the test gui.
Add some unit tests.

SUPPORT
-------
You can email the primary author on mark.stephen.pearce@gmail.com. It's likely that I'll fix any significant bugs fairly quickly. 

LICENSE
-------
TL;DR: Very liberal. Basically, you can do whatever you want as long as you include the original copyright.

The MIT License (MIT)

Copyright (c) 2013 Mark Pearce

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
