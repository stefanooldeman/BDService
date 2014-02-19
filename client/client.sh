#!/bin/bash

printf "added product to stock\n"
curl -sX POST http://127.0.0.1:8080/v1/products -H "Content-Type: application/json; encode=utf-8;" -d '{"Title":"TV", "Price": 100, "Amount":10}' && printf "\n"
curl -sX POST http://127.0.0.1:8080/v1/products -H "Content-Type: application/json; encode=utf-8;" -d '{"Title":"Phone", "Price": 49, "Amount":5}' && printf "\n"
curl -sX POST http://127.0.0.1:8080/v1/products -H "Content-Type: application/json; encode=utf-8;" -d '[{"Title":"Button", "Price": 2, "Amount":40}, {"Title":"Banana", "Price": 0.10, "Amount":20}]' && printf "\n"
curl -sX GET http://127.0.0.1:8080/v1/products?format=json && printf "\n"


printf "created a user\n"
curl -sX POST http://127.0.0.1:8080/v1/users -H "Content-Type: application/json; encode=utf-8;" -d  '{"Username": "stefano"}' && printf "\n"

printf "add a product to cart\n"
curl -sX POST http://127.0.0.1:8080/v1/user/stefano/cart?Password=Bd-123 \
    -H "Content-Type: application/json; encode=utf-8;" \
    -d '{"Products": [{"Id": 10, "Amount": 1}]}' && printf "\n"
#post user/stefano/cart?Password=Bd-123 '{"Products": [{"Id": 11, "Amount": 1}]}'


printf "get all cart items for this user\n"
curl -sX GET http://127.0.0.1:8080/v1/user/stefano/cart/1.json?password=Bd-123
printf "\n"


