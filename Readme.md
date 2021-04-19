# C_Sharp - Denmark
https://gitlab.com/altapay/aux/csharpsdk

C_Sharp is a Client library that is used as a bridge between customer .Net solutions and Altapay gateway.

## Installing C_Sharp

## Other pre-requisites

To build/package the C_Sharp you also need a number of build-tools.

Details about installing Mono can be found at http://www.mono-project.com/download/.

Below can be seen what had to be done in order to have a complete mono instalation on Ubuntu 16.04.

    $ sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
     echo "deb http://download.mono-project.com/repo/ubuntu xenial main" | sudo tee /etc/apt/sources.list.d/mono-official.list
    $ sudo apt-get update
  
  then (the complete installation of the library - for avoiding the "assembly not found" cases)
     
    $ sudo apt-get install mono-complete
    
 and finally:   
    
    $ sudo apt-get install ant


Start the build process by going to the repository directory from the terminal and run:

    $ ant
