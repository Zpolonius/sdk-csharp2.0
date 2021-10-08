#!/bin/bash
vagrant up
sshpass -p 'Passw0rd!' ssh -p 2222 IEUser@127.0.0.1 -t 'cd Desktop && ./build.bat'
sshpass -p 'Passw0rd!' scp -P 2222 IEUser@127.0.0.1:C:/Users/IEUser/Desktop/AltaPay_build.zip ./
