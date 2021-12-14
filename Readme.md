AltaPay - API C_Sharp
=====================

C_Sharp is a Client library that is used as a bridge between customer .Net solutions and AltaPay gateway.


## How to Build

### On Windows using Visual Studio 

> Note: To build this project upgrade to .NetFramework 4.6.1 or greater. If the target framework version is below 4.6.1. .NetStandard 2.0 SDK is unable to refer

1- Clone the repository 

    $ git clone https://github.com/AltaPay/sdk-csharp2.0.git

2- Open Visual Studio 2019 and go to File → Open → Project/Solution.

3- Select the .sln file located in the Altapay directory.

4- Right-click on the solution file and click Build Solution to build the package.

![Download](docs/build-project.png)


### Using Vagrant

> To build using any operating system, install [VirtualBox](https://www.virtualbox.org/wiki/Downloads) and [Vagrant](https://www.vagrantup.com/downloads) for your operating system.
 
1- Clone the repository

    $ git clone https://github.com/AltaPay/sdk-csharp2.0.git

2- Start the build process by traversing to the repository directory from the terminal and run:

    $ ./start_build.sh

3-  Above bash script generates the zip file **AltaPay_build.zip** in the repository directory. 

## Changelog

See [Changelog](CHANGELOG.md) for all the release notes.

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

## Documentation

For more details please see [AltaPay docs](https://documentation.altapay.com/)
