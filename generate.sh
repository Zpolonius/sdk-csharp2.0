#!/usr/bin/env bash

wget --no-check-certificate "http://cigateway.mars.pensio.com/APIResponse.xsd" -OAPIResponse.xsd
xsd APIResponse.xsd /c /o:AltaPayApi/AltaPayApi/Generated /namespace:AltaPay.Service.Dto
rm APIResponse.xsd
