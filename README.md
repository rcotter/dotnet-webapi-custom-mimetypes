# dotnet-webapi-custom-mimetypes
A .Net WebAPI example using accepting and producing custom vnd mime types

Demonstrates what it seemingly takes to use custom mime type versioning
with .Net Web APIs and Swagger.

Happily, JSON serialization is automatically detected for an Accept mime type like `application/vnd.custom+json;version=1`.

The examples swaggers presents take an enormous amount of work. Also, 
examples and corresponding mime types are not tied meaning it is not clear that both must
match.

![img.png](img.png)

...but it all works ok. There are likely refinements I'm not seeing.
