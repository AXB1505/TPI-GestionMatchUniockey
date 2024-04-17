#!/bin/bash
#Nom ou addresse du serveur postgres
HOST=127.0.0.1
#port d'accès du serveur de postgres
PORT=5432
#Utilisateur ayant accès à la BD
USER=clef
#Mot de passe de l'utilisateur
PASSWORD=clefPASS
#Nom de la base de donnée
DATABASE=clefDB_Dev
PSQL_SCRIPT=scriptDev.psql

PGPASSWORD=$PASSWORD psql -h $HOST -p $PORT -U $USER $DATABASE -f $PSQL_SCRIPT
