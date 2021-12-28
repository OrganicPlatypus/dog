# dog

Domestic Organization Guru

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info against requirements
Important disclaimer: Certainly in production life-cycle I would have arrange a meeting, gather more pieces of information, probably conduct event storming session, however as this code serves only demonstrative purposes I made very autonomic decisions.

This project meant to serve following requirements:
  * allow notes sharing
  * lifetime of a note might be defined
  * access using paraphrase

d.o.g. meets the requirements above by implementing them as follows:
  * notes are in the form of list
  * list is shared among any participant having access to noting space
  * one can join the noting session both from the landing page or by a link provided by anyone within existing noting session
  * any note lifetime at first is a default value
  * the lifetime can be changed while noting in in the dedicated settings window
  * notes access names are stored hashed in database.

## Technologies
Project was created with:
* Api based on .Net version: 5.0
* Api tests based on .Net version: 6.0
* Frontend:
  * Node.js: 12.11
  * Angular: 12.2

* Frameworks:
  * redux
  * signalR
  * automapper
  * fluentValidation
  * serilog
  * xunit
  * moq
  * fluentAssertion
  * nswag


## Setup
##### Database
[Redis](https://redis.io/) was chosen as a database provider for this project due to following reasons:
* always wanted to give it a try (I am aware of variety of alternatives however I found key-value stores sufficient enough)
* does not require any additional infrastructure - is cost efficient.
* will be faster than heavy engines

Database setup:
```
docker run --name dog_redisDB -d -p 6379:6379 redis redis-server
```





To run this project, install it locally using npm:

```
$ cd ../lorem
$ npm install
$ npm start
```
