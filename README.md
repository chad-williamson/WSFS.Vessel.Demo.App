# WSFS.Vessel.Demo.App

My demo consists of 4 services, as follows:

WSFS.Vessel.Info - the REST service for the vessel "basic" info.

WSFS.Vessel.Info.Worker - a worker service that calls the WSFS API to gather vessel info, which then calls the WSFS.Vessel.Info service to save this to sql.

WSFS.Vessel.Location - the REST service for the vessel location info.

WSFS.Vessel.Location.Worker - a worker service that calls the WSFS API to gather vessel location info, which then calls the WSFS.Vessel.Location service to save in memory.


I feel like this exercise took me longer than it should have. Wasted some time refactoring a bit. Started off with a shared library with model and dbcontext classes. I was referencing them in all 4 services to gather, deserialize, save, etc. But I know the whole point of the microservice ideology is to have them as decoupled and autonomous as possible. So I went rethought the design and decided to have the workers simply call WSFS and then send the REST services the raw json string. This way the REST services could be the single location for the API and dbcontext models. This may not be what you were looking for, but I felt it was the best implementation.

Admittedly this is probably my most extensive exposure to microservice architecture. Everything I've previously worked on or created was quite monolithic in nature. I can see how getting in the habit of this design mentality would have saved me time.

I had intentions of writing a unit test or two, and even implementing a Blazor front end with mapping and a basic interface, but time did not allow as I had already spent between 6 and 7 hours on this, and felt exceeding that would not be in the spirit of the exercise. I don't see much code/logic that could benefit from a unit test here honestly. Even though I've written several unit tests before, I do struggle with the best way to implement them at times. I see coworkers writing 20 lines of mocking code to test one line of actual code. In those cases I wonder if the ROI is worth it. That being said, I know they have value and this is an area of growth for me that I need to improve on.

I did test all the services on my local machine and everything was working as expected.

