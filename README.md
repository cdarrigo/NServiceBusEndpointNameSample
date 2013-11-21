NServiceBusEndpointNameSample
=============================

A solution illustrating a problem with the Bus.InputAddress not respecting the value of DefineEndpointName when selfhosted.


I'm hosting the bus in a unit test, but the same behavior happens when I host it using a console app.

In the test, when the test class inits, it sets up the DI Container which will configure an instance of the bus and pass in the endpoint name to use.
The retries and timeout related queues are respecting this name, but the inputaddress queue is still using the name derived from the namespace.
You can run the test_bus_inputqueueName test to demonstrate this behavior, and the queues are visible in the Computer Management windows dialog.
