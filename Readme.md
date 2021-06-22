Altapay - API C_Sharp
=====================

C_Sharp is a Client library that is used as a bridge between customer .Net solutions and Altapay gateway.

Note: To build this project upgrade to .NetFramework 4.6.1 or greater. If the target framework version is below 4.6.1. .NetStandard 2.0 SDK is unable to refer

### Build package

To build/package the C_Sharp you also need a number of build-tools.

Details about installing Mono can be found at http://www.mono-project.com/download/.

Below can be seen what had to be done in order to have a complete mono instalation on Ubuntu 16.04.

    $ sudo apt install gnupg ca-certificates
    $ sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
        echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
    $ sudo apt update

  The package mono-devel should be installed to compile code.
    $ sudo apt install mono-devel

  then (the complete installation of the library - for avoiding the "assembly not found" cases)
     
    $ sudo apt install mono-complete
    
 and finally:   
    
    $ sudo apt install ant

### 1.0.0

- Supports API changes from 20210324

## License

See [LICENSE](LICENSE)