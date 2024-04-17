#!/bin/bash
#Nom ou addresse du serveur postgres
HOST=localhost
#port d'accès du serveur de postgres
PORT=5432
#Utilisateur ayant accès à la BD
USER=postgres
#Mot de passe de l'utilisateur
PASSWORD=TPI
#Nom de la base de donnée
DATABASE=unihockey
PSQL_SCRIPT=script.psql

PGPASSWORD=$PASSWORD psql -h $HOST -p $PORT -U $USER $DATABASE -f $PSQL_SCRIPT
