
# dog

Domestic Organization Guru

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [IfRefreshed](#IfRefreshed)
* [Setup](#setup)
* [Acknowledgments](#Acknowledgments)

## General info and intention
This project meant to serve following requirements:
  * allow notes sharing
  * lifetime of a note might be defined
  * access using passphrase
  * no accounts required

## Technologies
Project was created with:
* Api based on .Net version: 5.0
* Api tests based on .Net version: 6.0
* Frontend:
  * Node.js: 12.11
  * Angular: 12.2

* Frameworks:
  * [redux](https://ngrx.io/)
  * [signalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0)
  * [toastr](https://www.npmjs.com/package/ngx-toastr)
  * [automapper](https://docs.automapper.org/en/stable/)
  * [fluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html)
  * [serilog](https://serilog.net/)
  * [xunit](https://xunit.net/)
  * [mock]
  * [nswag](https://github.com/RicoSuter/NSwag)

[Redis](https://redis.io/) was chosen as a database provider for this project due to following reasons:
  * always wanted to give it a try (I am aware of variety of alternatives however I found key-value stores sufficient enough for this task)
  * does not require any additional infrastructure - cost efficiency.
  * will be faster than heavy engines


## Setup

#### Frontend
To run frontend app, install it locally using npm:
```
$ cd domesticOrganizationGuru
$ npm install
$ npm serve -o
```

#### Database
To fire redisDb, if you would like to setup .net dev enviornment separately, you'll have to run redis docker first as connectionMultiplixer will throw
```
docker run --name dog_redisDB -d -p 6379:6379 redis redis-server
```

#### Backend
##### Warning: to run project Database image has to be running
```
$ cd cd .\dog\domesticOrganizationGuru.Api\domesticOrganizationGuru.Api\
$ dotnet run .\DomesticOrganizationGuru.Api.csproj
```

## IfRefreshed

##### What I would have done differently
* Introduce CQRS to decouple logic in a batter way, shift validation into a behavior pipeline.
* Reconsider Redis as a database, or at least pick better datatype representation of notes
* Dockerize the application properly not by parts
* Change notes type in frontend from BehaviorSubject to a hashset of a kind
* Change messages into not suggesting any information on an already existing data
* Have hardcoded values placed into appconfigs, consts, resxes
* I would have frontend part cleaned from any unused leftovers and tested properly
* Focus either on nswag or on direct implementation of api endpoints
* Squash commits while merging!


## Acknowledgments

My inspirations:
* [Loading animation](https://codepen.io/Sirop)
* [Colours and layout](https://codepen.io/rickyeckhardt)
