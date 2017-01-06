# Shuffled Number Generator
A command line utility that generates a list of 10,000 numbers in random order.
Each number in the list is unique and is between 1 and 10,000 (inclusive). The
results are written to the standard output.

## Building

### Local
To download the source files for this project, run the following in the
directory where you wish to download the files:
```bash
git clone git@github.com:jhbertra/shuffled-number-generator.git
```
#### Building in Visual Studio
Simply open ShuffledNumberGenerator.sln, found in the root directory of the
project, in Visual Studio.  It can then be built from the main menu with
*Build > Build Solution* and can be run by clicking the start button, or by
executing `ShuffledNumberGenerator\bin\Debug\ShuffledNumberGenerator.exe` in a
command prompt from the root directory of the project.  This is the recommended
way to run the project, as the shell will not exit when the process terminates.

Tests can be run from the main menu with *Test > Run > All Tests*, or using
ReSharper with *ReSharper > Unit Tests > Run All Unit Tests From Solution*

### Remote
The project is under Continuous Integration and Delivery via TravisCI. The build
can be accessed remotely from http://

## Commentary of Solution
The goals I had in mind when writing this solution were:

- Keep the code simple.
- Approach the project from a holistic perspective, as if it were a professional
deliverable.
- Find the most efficient algorithm to solve the problem.

### Notes on Solution Architecture and Production Code
I chose to implement this solution in a purely procedural manner, rather than in
an object-oriented one. I believe that a solution should never be over-
architected, and that there is a strong temptation to let the creative aspect
of object-oriented design to run wild when programming in a language such as C#.
I would much rather that my peers find my code concise and easy to comprehend
than that they be impressed by my abstract, existentialist reasoning skills.

This is why this solution contains only one production source file, and
instantiates no classes (except for an instance of `Math.Random`).  In my first
draft, I used an instance of `System.Collections.Generic.List<T>` for storage
of the integers, and added a `SwapElements` extension method for use by the
shuffling function.  But even this seemed overly complex.  The primary
advantages of using a `List<int>` instead of a plain `int[]` were not leveraged.
The size of the list was known at compile time, and so did not need to
dynamically grow, none of the functionality of the `List<T>` class was used,
and the `SwapElements` method was more straightforward to implement as function,
rather than an extension method.

In the end, the problem was a simple, self-contained one, which warranted a
correspondingly simple and self-contained solution.

### Notes on Non-Functional Aspects of Deliverable
I took somewhat of an opposite approach when it came to the non-functional
aspects of the project.  I decided to include ample documentation, include unit
and acceptance tests, host the project on GitHub, build the project on a CI
server, and automatically deploy the builds to remote storage.  I did so to
demonstrate my approach regarding these important aspects of the development
cycle.

### Notes on Algorithm Choice
The immediate inclination when reading "write a program that generates a list of
10000 random numbers" was obviously

```
for i in 10000
    print random()
```
But given that the numbers had to be contained in a specific interval and never
repeat, this quickly became problematic, as the solution wouldn't merely be
inefficient, it would be nondeterministic.

This quickly lead to a different approach, in which the list was first created
in a non-random order such a way that all the required numbers were guaranteed
to exist uniquely, then were shuffled to obtain random permutations.  Some quick
literature review revealed the Fisher-Yates shuffle algorithm, as improved by
Richard Durstenfeld was the best candidate for creating random permutations of
finite sets, given its O(n) time and space complexity.  This is the algorithm
used in the final solution.

### Notes on Testing
I decided to include both unit tests and acceptance tests in this project. When
it came to writing unit tests, the first problem was "What constitutes a unit?"
In most object-oriented programs, classes are these units. There are many
advantages to this approach. The structure of the test files closely reflects
the structure of the production files, test subjects can be created with mock
dependencies all from within a single setup function, mock state expectations
can be validated in the teardown functions, and the logic tested in a single
file is logically cohesive.

However, problems begin to occur when this approach is applied to procedural
code. Most object-oriented software contains at least some procedural code.
It is quite common to find "classes" that are purely collections of static
functionality.

The advantages of the class-as-a-unit approach quickly break down in these
cases.  The filenames usually become vague. For instance, this solution only
defines one class, named "Application" that contains all the logic, which would
lead to a test file name of "ApplicationTests." Supremely uninformative. Static
classes also do not usually share state and dependencies between functions.  
This makes it almost impossible to make effective use of setup and teardown
functions to setup shared state, because there is none.

In cases such as these, it is far more appropriate to treat functions as the
units of code to test.  For example, this solution's Application class contains
a function called `ShuffleArray`, therefore the test fixture that tests this
unit is named `ShuffleArrayTests`.  It is immediately clear what unit of
functionality this fixture tests by contrast to `ApplicationTests`. Furthermore,
all the dependencies and state of `ShuffleArray` can be created and validated
using the setup and teardown functions of the test fixture.

A curious problem arises when mixing these approaches.  How can the integrity
of file naming and structure conventions be preserved if some files test
classes, while others test functions?  I think the answer lies in nesting test
fixtures hierarchically, a feature which is supported by most testing
frameworks.  This would allow the convention of one file of test code for every
file of source code to hold, but would also allow pieces of logically distinct
functionality to be treated as the individual units of code.

Acceptance tests were written basically like unit tests which simply execute the
top-level function and validate the output.  This is the reason why the Main
function does not directly run the program logic, but delegates it to another
function.  Doing so allowed the output stream to be injected, so that output
could be observed by test code, and not end up uselessly dumped to the console.
