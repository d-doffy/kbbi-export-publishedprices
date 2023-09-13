#!/bin/sh
dotnet publish -c Release --runtime linux-x64 --self-contained true -o publish 
cd publish
mv export.publish.prices genprices
zip -r ../genprices.zip *
cd ..
