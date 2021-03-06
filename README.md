# Monkey Coder
A monkey tries to code

## Application at glance
 - Artificial intelligence
 - Algorithm generator
 - Trial and error approach
 - Solution guessing

## How it works
We assume that there is some code...

Let's say a C# code - to be more specific. What we know about it? In the worst case - absolutely nothing. However, there is a wide array of statements that we could put about this code to increase our knowledge about it and thereby to shorten our way to figuring it out! In this project, we will start our way from the end, knowing absolutely everything about the algorithm and we will gradually decrease our knowledge about it, introducing wider and wider holes that we will attempt to fill up in the same time using as wise approaches as we will able to invent.

The ultimate objective is to develop an application that will be able to generate any algorithm based on known associations between inputs and outputs as well as knowledge of its own functions and their capabilities.

## Milestones
Each milestone will be achieved when application is able to fix an algorithm in defined degree of degradation. Each of which is defined as a user story:
 - As a user, I know the algorithm's implementation, input and output. However, I was given several integer values and I don't know how to initialize integer variables or fields withitn the algorithm with these variables to achieve expected output from provided input. ![Implemented][Y]
 - As a user, I don't know the algorithm's implementation. However, I know all functions and their arguments that are required and sufficient to implement the algorithm. The functions and their arguments are black boxes to me. Hence the only way to connect them is a random way. ![Implemented][Y]
 - As a user, I don't know the algorithm's implementation. Hover, I know all functions and their arguments that are required and sufficient to implement the algorithm. Some of the functions and their arguments are white boxes to me. Hence I have a chance to reduce the randomization when generating the code. ![Not implemented][X]

[X]: https://www.classroomcentral.org/wp-content/themes/classroomcentral/images/mark-x.png
[Y]: http://checkstocknow.co.uk/images/tick.gif

## Technology
Category|Tool
---|---
Language|C# 6.0
Framework|.NET Framework 4.6
IDE|Visual Studio Community 2015
VCS|GitHub
Unit Tests|NUnit
