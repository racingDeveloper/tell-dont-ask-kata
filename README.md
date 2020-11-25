# Tell Don't Ask Kata
A legacy refactor kata, focused on the violation of the [tell don't ask](https://wiki.c2.com/?TellDontAsk) principle and the [anemic domain model](https://martinfowler.com/bliki/AnemicDomainModel.html).

## Instructions
Here you find a simple order flow application. It's able to create orders, do some calculation (totals and taxes), and manage them (approval/reject and shipment).

The old development team did not find the time to build a proper domain model but instead preferred to use a procedural style, building this anemic domain model.
Fortunately, they did at least take the time to write unit tests for the code.

Your new CTO, after many bugs caused by this application, asked you to refactor this code to make it more maintainable and reliable.

## What to focus on
As the title of the kata says, of course, the tell don't ask principle.
You should be able to remove all the setters moving the behavior into the domain objects.

But don't stop there.

If you can remove some test cases because they don't make sense anymore (eg: you cannot compile the code to do the wrong thing) feel free to do it!

## Credit
The Kata was originally authored by [@racingDeveloper](https://twitter.com/racingDeveloper)\
Typescript and Python3 versions by [mapu77](https://github.com/mapu77/tell-dont-ask-kata)\
PHP version by [Archel](https://github.com/Archel/tell-don-t-ask-kata-php)\
CSharp version by [raullorca](https://github.com/raullorca/TellDontAskKata)