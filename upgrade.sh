#!/bin/bash
for i in `ls -d ubta.*` 
do
    # check if directory exists
    echo "Checking $i"
    if [ -d $i ]; then
        echo "Upgrading $i"
        upgrade-assistant upgrade -o Inplace -f LTS $i/${i}.csproj 
    fi
done
