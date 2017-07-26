## Overview ##

In my concept of the issue there are devices/sensors which generate data and send them to the server. There are two main parts of the demo.
Firt there is a Web API application which serves as a hub for device messages (collects + provides statistics about messages). Second component
is a console application which simulates several sensors. Each sensor type has a different period of reading data and also generates diffrent
type of data (e.g. multiple values).



## Instructions ##

1. Open the solution file in Visual Studio and build the whole solution
2. Set the 'Monitor' web application as 'StartUp Project'.
3. Debug it 
4. Open compiled binaries of 'DeviceSimulater' and adjust the 'app.config' file
	a) make sure URL value match you localhost port
	b) you can change number of sensors
5. Run the 'DeviceSimulator'

The web application provides 3 type of information:

	http://localhost:53102/api/device					(list of all devices which have sent data)
	http://localhost:53102/api/device?deviceName=Pres_ykx5zh2wuog		(list of all messages per given device)
	http://localhost:53102/api/statistics					(number of messages per each device)

Note: The device name is generated on the fly so if you run the console app multiple times you will notice new devices appears on the list.
      Messages are sent to the server in 10 seconds interval - don't be suprised if the statistics change slowly.
      Each sensor provides data with different frequency (temperatue every 15 seconds vs vibration every 300 millisecons).



## Comments ##

There is definitelly space for further developement :-). The proper DAO layer can be created + real database to store the data + unit tests for business layer and MVC controllers.
Also the messages could be query based on time interval, create report from sensor values etc. 
Originally I started with WebAPI application (to keep it simple) but at the end I would use standard MVC to have better UI and control over URLs pattern.