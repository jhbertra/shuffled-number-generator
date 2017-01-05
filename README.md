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

I chose to implement this solution in a purely procedural manner, rather than in
an object-oriented one. I believe that a solution should never be over-
architected, and that there is a  strong temptation to let the creative aspect
of object-oriented design to run wild  programming.  I would much rather that my
peers find my code concise and easy to comprehend than that they be impressed
by my abstract, existentialist reasoning skills.  This is why this solution
contains only one production source file, and does not instantiate any objects
with names that include terms like "Runner," "Shuffler," or "Factory."  The only
exception to this approach was to extend the `System.Collections.Generic.List`
class with a `swapElements` method.  I reasoned that this was a reasonable use
of object-oriented design, as it related only to an operation being performed on
a data-structure, rather than to abstract application architecture or dependency
management, which I find OOP is used and abused to achieve more often than it
should be.  In the end, the problem was a simple, self-contained one, which
warranted a similarly simple and self-contained solution.

I took somewhat of an opposite approach when it came to the non-functional
aspects of the project.  That is because in this case, the functionality and
complexity of the project wasn't the determining factor in how much
documentation should be provided, how thorough the unit and acceptance testing
should be, whether or not the project should be built and tested on a CI server,
or if it should be automatically deployed to remote storage.  In this case, I
simply  wanted to demonstrate my approach regarding these important aspects of
the development cycle in this case.

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
