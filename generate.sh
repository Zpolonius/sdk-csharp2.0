#!/usr/bin/env bash

wget --no-check-certificate "https://testgateway.pensio.com/APIResponse.xsd" -O APIResponse.xsd
xsd APIResponse.xsd /c /o:AltaPayApi/AltaPayApi/Generated /namespace:AltaPay.Service.Dto
rm APIResponse.xsd
