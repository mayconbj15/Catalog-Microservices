FROM mongo
# Will be set through Environment Files
ARG DATABASE_URI
ARG USERNAME
ARG PASSWORD

ENV DATABASE_URI ${DATABASE_URI}
ENV USERNAME ${USERNAME}
ENV PASSWORD ${PASSWORD}

COPY products.json /products.json

#CMD mongoimport --username ${USERNAME} --password ${PASSWORD} --uri ${DATABASE_URI} --collection products --drop --file /products.json
CMD mongoimport --uri ${DATABASE_URI} --db=CatalogDb --collection Products --drop --file /products.json