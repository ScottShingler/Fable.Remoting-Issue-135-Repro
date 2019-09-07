# Repro for Fable.Elmish Issue [#135](https://github.com/Zaid-Ajaj/Fable.Remoting/issues/135)

## Install pre-requisites

You'll need to install the following pre-requisites in order to build the application:

* The [.NET Core SDK](https://www.microsoft.com/net/download)
* [FAKE 5](https://fake.build/) installed as a [global tool](https://fake.build/fake-gettingstarted.html#Install-FAKE)
* The [Yarn](https://yarnpkg.com/lang/en/docs/install/) package manager (you an also use `npm` but the usage of `yarn` is encouraged).
* [Node LTS](https://nodejs.org/en/download/) installed for the front end components.
* If you're running on OSX or Linux, you'll also need to install [Mono](https://www.mono-project.com/docs/getting-started/install/).

## Run the application

To concurrently run the server and the client components in watch mode use the following command:

```bash
fake build -t Run
```

When the UI appears, click the button. This will invoke the `doSomething : int -> Async<Unit>` function with the value 42. Instead of sending a payload of 42 in the POST request, it will send `[null]`.